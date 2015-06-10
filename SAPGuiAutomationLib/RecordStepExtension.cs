using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SAPGuiAutomationLib
{
    public static class RecordStepExtension
    {
        public static List<CodeStatement> GetCodeStatement(this RecordStep step,string dataClassParameter = null)
        {
            List<CodeStatement> codes = new List<CodeStatement>();
            CodeStatement actionCode = null;
            switch (step.Action)
            {
                case BindingFlags.SetProperty:
                    actionCode = getAssignDetailStatement(step, dataClassParameter);
                    break;
                case BindingFlags.InvokeMethod:
                    actionCode = getInvokeDetailStatement(step, dataClassParameter);
                    break;
                default:
                    break;
            }
            if (actionCode != null)
                codes.Add(actionCode);
            
            if(step.TakeScreenShot)
            {
                CodeExpressionStatement screenShotCode = new CodeExpressionStatement();
                screenShotCode.Expression = new CodeMethodInvokeExpression(
                    new CodeVariableReferenceExpression("SAPTestHelper.Current"), 
                    "TakeScreenShot", 
                    new CodePrimitiveExpression(step.StepId.ToString() + ".jpg"));
                codes.Add(screenShotCode);
            }
            return codes;
        }


        private static CodeAssignStatement getAssignDetailStatement(RecordStep step, string dataClass)
        {
            CodeAssignStatement asStatement = new CodeAssignStatement();

            asStatement.Left = new CodePropertyReferenceExpression(step.CompInfo.FindMethod, step.ActionName);

            if (dataClass == null)
                asStatement.Right = new CodePrimitiveExpression(step.ActionParams[0].Value);
            else
                asStatement.Right = step.ActionParams[0].GetVariableReference(dataClass);
            return asStatement;
        }

        private static CodeExpressionStatement getInvokeDetailStatement(RecordStep step, string dataClass)
        {
            CodeExpressionStatement statement = new CodeExpressionStatement();
            CodeExpression[] paras;
            if (step.ActionParams != null)
            {
                paras = new CodeExpression[step.ActionParams.Count];
                ///判断是否参数化函数代码
                if(dataClass == null)
                {
                    for (int i = 0; i < step.ActionParams.Count; i++)
                    {
                        paras[i] = new CodePrimitiveExpression(step.ActionParams[i].Value);
                    }
                }
                else
                {
                    for (int i = 0; i < step.ActionParams.Count; i++)
                    {
                        paras[i] = step.ActionParams[i].GetVariableReference(dataClass);
                    }
                }
                
            }
            else
            {
                paras = new CodeExpression[0];
            }

            CodeMethodInvokeExpression method = new CodeMethodInvokeExpression(step.CompInfo.FindMethod, step.ActionName, paras);
            statement.Expression = method;
            return statement;
        }
    }
}

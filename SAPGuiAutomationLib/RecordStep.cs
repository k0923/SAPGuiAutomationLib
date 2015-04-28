using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using SAPTestRunTime;

namespace SAPGuiAutomationLib
{
    public class RecordStep
    {
        public int StepId { get; set; }

        public BindingFlags Action { get; set; }

        public SapCompInfo CompInfo { get; set; }

        public string ActionName { get; set; }

        public object[] ActionParams { get; set; }

        public CodeStatement GetCodeStatement()
        {
            switch(Action)
            {
                case BindingFlags.SetProperty:
                    return getAssignStatement();
                case BindingFlags.InvokeMethod:
                    return getInvokeStatement();
                default:
                    return null;
            }
        }

        private CodeAssignStatement getAssignStatement()
        {
            CodeAssignStatement asStatement = new CodeAssignStatement();

            CodeMethodInvokeExpression genericMethod = new CodeMethodInvokeExpression(
                      new CodeMethodReferenceExpression(
                         new CodeVariableReferenceExpression("SAPTestHelper.Current"),
                             "GetElementById",
                                 new CodeTypeReference[] {
                                    new CodeTypeReference(CompInfo.Type),
                                       }), new CodePrimitiveExpression(CompInfo.Id));


            asStatement.Left = new CodePropertyReferenceExpression(genericMethod,ActionName);

            asStatement.Right = new CodePrimitiveExpression(ActionParams[0]);
            return asStatement;
        }

        private CodeExpressionStatement getInvokeStatement()
        {
            
            CodeExpressionStatement statement = new CodeExpressionStatement();
            CodeMethodInvokeExpression genericMethod = new CodeMethodInvokeExpression(
                      new CodeMethodReferenceExpression(
                         new CodeVariableReferenceExpression("SAPTestHelper.Current"),
                             "GetElementById",
                                 new CodeTypeReference[] {
                                    new CodeTypeReference(CompInfo.Type),
                                       }), new CodePrimitiveExpression(CompInfo.Id));
            CodeExpression[] paras = new CodeExpression[ActionParams.Count()];
            for(int i =0;i<ActionParams.Count();i++)
            {
                paras[i] = new CodePrimitiveExpression(ActionParams[i]);
            }
            CodeMethodInvokeExpression method = new CodeMethodInvokeExpression(genericMethod, ActionName,paras);
            statement.Expression = method;
            return statement;
        }
    }
}

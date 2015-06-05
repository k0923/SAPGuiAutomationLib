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
        public static CodeStatement GetCodeStatement(this RecordStep step)
        {
            switch (step.Action)
            {
                //case BindingFlags.SetProperty:
                //    return getAssignStatement(step);
                //case BindingFlags.InvokeMethod:
                //    return getInvokeStatement(step);
                //default:
                //    return null;
                case BindingFlags.SetProperty:
                    return getAssignDetailStatement(step);
                case BindingFlags.InvokeMethod:
                    return getInvokeDetailStatement(step);
                default:
                    return null;
            }
        }

        //public static CodeStatement GetCodeDetailStatement(this RecordStep step)
        //{
            
        //    switch (step.Action)
        //    {
        //        case BindingFlags.SetProperty:
        //            return getAssignDetailStatement(step);
        //        case BindingFlags.InvokeMethod:
        //            return getInvokeDetailStatement(step);
        //        default:
        //            return null;
        //    }
        //}

        //private static CodeAssignStatement getAssignStatement(RecordStep step)
        //{
        //    CodeAssignStatement asStatement = new CodeAssignStatement();

        //    CodeMethodInvokeExpression genericMethod = new CodeMethodInvokeExpression(
        //              new CodeMethodReferenceExpression(
        //                 new CodeVariableReferenceExpression("SAPTestHelper.Current"),
        //                     "GetElementById",
        //                         new CodeTypeReference[] {
        //                            new CodeTypeReference(step.CompInfo.Type),
        //                               }), new CodePrimitiveExpression(step.CompInfo.Id));

            


        //    asStatement.Left = new CodePropertyReferenceExpression(genericMethod, step.ActionName);

        //    asStatement.Right = new CodePrimitiveExpression(step.ActionParams[0].Value);
        //    return asStatement;
        //}

        //private static CodeExpressionStatement getInvokeStatement(RecordStep step)
        //{

        //    CodeExpressionStatement statement = new CodeExpressionStatement();
        //    CodeMethodInvokeExpression genericMethod = new CodeMethodInvokeExpression(
        //              new CodeMethodReferenceExpression(
        //                 new CodeVariableReferenceExpression("SAPTestHelper.Current"),
        //                     "GetElementById",
        //                         new CodeTypeReference[] {
        //                            new CodeTypeReference(step.CompInfo.Type),
        //                               }), new CodePrimitiveExpression(step.CompInfo.Id));
        //    CodeExpression[] paras;
        //    if (step.ActionParams != null)
        //    {
        //        paras = new CodeExpression[step.ActionParams.Count];
        //        for (int i = 0; i < step.ActionParams.Count; i++)
        //        {
        //            paras[i] = new CodePrimitiveExpression(step.ActionParams[i].Value);
        //        }
        //    }
        //    else
        //    {
        //        paras = new CodeExpression[0];
        //    }

        //    CodeMethodInvokeExpression method = new CodeMethodInvokeExpression(genericMethod, step.ActionName, paras);
        //    statement.Expression = method;
        //    return statement;
        //}

        private static CodeAssignStatement getAssignDetailStatement(RecordStep step)
        {
            CodeAssignStatement asStatement = new CodeAssignStatement();

            asStatement.Left = new CodePropertyReferenceExpression(step.CompInfo.FindMethod, step.ActionName);

            asStatement.Right = new CodePrimitiveExpression(step.ActionParams[0].Value);
            return asStatement;
        }

        private static CodeExpressionStatement getInvokeDetailStatement(RecordStep step)
        {
            CodeExpressionStatement statement = new CodeExpressionStatement();
            CodeExpression[] paras;
            if (step.ActionParams != null)
            {
                paras = new CodeExpression[step.ActionParams.Count];
                for (int i = 0; i < step.ActionParams.Count; i++)
                {
                    paras[i] = new CodePrimitiveExpression(step.ActionParams[i].Value);
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

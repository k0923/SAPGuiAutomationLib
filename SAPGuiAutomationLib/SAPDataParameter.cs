using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPGuiAutomationLib
{
    public class SAPDataParameter
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Comment { get; set; }
        public Type Type { get; set; }

        public object Value { get; set; }
    }

    public static class SAPDataParameterCodeExtension
    {
        public static CodeTypeMember CreatePropertyCode(this SAPDataParameter p)
        {
            CodeSnippetTypeMember snippet = new CodeSnippetTypeMember();
            
            
            if (p.Comment != null)
            {
                snippet.Comments.Add(new CodeCommentStatement(p.Comment, true));
            }

            snippet.Text = string.Format("public {0} {1} {{get;set;}}", p.Type.ToString(), p.Name);
            
            return snippet;
        }

        public static CodeVariableReferenceExpression GetVariableReference(this SAPDataParameter p,string dataClass)
        {
            return new CodeVariableReferenceExpression(string.Format("{0}.{1}", dataClass, p.Name));
        }
    }
}

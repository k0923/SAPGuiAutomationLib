using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using System.Reflection;
using SAPGuiAutomationLib;
using SAPFEWSELib;

namespace SAPLibTools
{
    public class CodeGenerateHelper
    {
        private CodeCompileUnit _unit;
        private CodeTypeDeclaration _targetClass;

        public CodeCompileUnit Unit { get { return _unit; } }

        public CodeTypeDeclaration TargetClass { get { return _targetClass; } }

        public CodeGenerateHelper()
        {
            _unit = new CodeCompileUnit();
            _targetClass = new CodeTypeDeclaration("SAPFEExtension");
            _targetClass.IsClass = true;
            _targetClass.TypeAttributes = TypeAttributes.Public;
           
            
            CodeNamespace ns = new CodeNamespace("SAPTestRunTime");
            ns.Types.Add(_targetClass);
            _unit.Namespaces.Add(ns);
        }

        public CodeMemberMethod[] GetFindByNameMethod()
        {

            
            CodeMemberMethod method = new CodeMemberMethod();
            CodeTypeParameter tpPara = new CodeTypeParameter("TReturn");
            tpPara.Constraints.Add(new CodeTypeReference(" class", CodeTypeReferenceOptions.GenericTypeParameter));
            method.TypeParameters.Add(tpPara);

            tpPara = new CodeTypeParameter("TInput");
            tpPara.Constraints.Add(new CodeTypeReference(" class", CodeTypeReferenceOptions.GenericTypeParameter));
            method.TypeParameters.Add(tpPara);



            method.Name = "findByNameTemplate";
            method.Attributes = MemberAttributes.Static | MemberAttributes.Private;
            method.ReturnType = new CodeTypeReference("TReturn");
            method.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference("TInput"), "Container"));
            method.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "Name"));

            CodeConditionStatement conditionalStatement = new CodeConditionStatement(
                // The condition to test. 

               new CodeSnippetExpression("Container is GuiVContainer"),

                // The statements to execute if the condition evaluates to true. 
           new CodeStatement[] { 
               new CodeSnippetStatement("var container = Container as GuiVContainer;")
              ,new CodeSnippetStatement("TReturn item = container.FindByName(Name, typeof(TReturn).Name) as TReturn;")
              ,new CodeSnippetStatement("return item;")},
                // The statements to execute if the condition evalues to false. 
           new CodeStatement[] { new CodeSnippetStatement("throw new Exception(\"Not Found FindByName method in type:\" + typeof(TInput).Name);") });


            method.Statements.Add(conditionalStatement);


            SAPAutomationHelper.Current.SetSAPApiAssembly();
            Assembly asm = SAPAutomationHelper.Current.SAPGuiApiAssembly;

            var interfaces = asm.GetTypes().Where(t => t.IsInterface && t.Name.StartsWith("Gui") && t.GetInterfaces()[0].GetMethod("FindByName") != null).ToArray();
          
            CodeMemberMethod[] methods = new CodeMemberMethod[interfaces.Count()+1];

            for (int i = 0; i < interfaces.Count(); i++)
            {
                
                methods[i] = new CodeMemberMethod();
                methods[i].Name = "FindByName";
                methods[i].Attributes = MemberAttributes.Public | MemberAttributes.Static;
                CodeParameterDeclarationExpression para1 = new CodeParameterDeclarationExpression(new CodeTypeReference("this "+interfaces[i].Name),"Container");
                methods[i].Parameters.Add(para1);
                CodeParameterDeclarationExpression para2 = new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)),"Name");
                methods[i].Parameters.Add(para2);
                methods[i].ReturnType = new CodeTypeReference("T");
                CodeTypeParameter param = new CodeTypeParameter("T");
                param.Constraints.Add(" class");
                methods[i].TypeParameters.Add(param);

                methods[i].Statements.Add(new CodeMethodReturnStatement(
                        new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(null, "findByNameTemplate", new CodeTypeReference("T"), new CodeTypeReference(interfaces[i].Name)
                                ), new CodeVariableReferenceExpression("Container"), new CodeVariableReferenceExpression("Name"))
                    ));
            }

            methods[methods.Length - 1] = method;

            return methods;

        }


    }
}

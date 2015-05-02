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


        public List<CodeSnippetTypeMember> GetVisualComponentProperties()
        {
            SAPAutomationHelper.Current.SetSAPApiAssembly();
            Assembly asm = SAPAutomationHelper.Current.SAPGuiApiAssembly;

            Type VCompoent = asm.GetType("SAPFEWSELib.GuiVComponent").GetInterfaces()[0];
           
            List<CodeSnippetTypeMember> props = new List<CodeSnippetTypeMember>();
            foreach (PropertyInfo pi in VCompoent.GetProperties().Where(p=>p.IsSpecialName == false))
            {
                CodeSnippetTypeMember p = new CodeSnippetTypeMember();
                
                string text = "public " + pi.PropertyType.Name + " " + pi.Name + "{get;set;}";
                p.Text = text;
                props.Add(p);
            }
            return props;
            
        }

        public CodeMemberMethod[] GetFindChildByConditionMethods()
        {
            string methodName = "FindChildByCondition";

            SAPAutomationHelper.Current.SetSAPApiAssembly();
            Assembly asm = SAPAutomationHelper.Current.SAPGuiApiAssembly;

            var interfaces = asm.GetTypes().Where(t => t.IsInterface && t.Name.StartsWith("Gui") && t.GetInterfaces()[0].GetProperty("Children") != null).ToArray();

            CodeMemberMethod[] methods = new CodeMemberMethod[interfaces.Count()];

            for (int i = 0; i < interfaces.Count(); i++)
            {

                methods[i] = new CodeMemberMethod();
                methods[i].Name = methodName;
                methods[i].Attributes = MemberAttributes.Public | MemberAttributes.Static;
                CodeParameterDeclarationExpression para1 = new CodeParameterDeclarationExpression(new CodeTypeReference("this " + interfaces[i].Name), interfaces[i].Name.Substring(3));
                methods[i].Parameters.Add(para1);
                CodeParameterDeclarationExpression para2 = new CodeParameterDeclarationExpression(new CodeTypeReference("Func<T,bool>"), "Condition");
                methods[i].Parameters.Add(para2);
                methods[i].ReturnType = new CodeTypeReference("T");
                CodeTypeParameter param = new CodeTypeParameter("T");
                param.Constraints.Add(" class");
                methods[i].TypeParameters.Add(param);

                methods[i].Statements.Add(new CodeMethodReturnStatement(
                        new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(null, "findChildByConditionTemplate", new CodeTypeReference("T")),
                            new CodeVariableReferenceExpression(interfaces[i].Name.Substring(3)+".Children"),
                            new CodeVariableReferenceExpression("Condition")
                    )));
            }
            return methods;
        }

        public CodeMemberMethod[] GetFindByNameExMethods()
        {
            string methodName = "FindByNameEx";

            SAPAutomationHelper.Current.SetSAPApiAssembly();
            Assembly asm = SAPAutomationHelper.Current.SAPGuiApiAssembly;

            var interfaces = asm.GetTypes().Where(t => t.IsInterface && t.Name.StartsWith("Gui") && t.GetInterfaces()[0].GetMethod(methodName) != null).ToArray();

            CodeMemberMethod[] methods = new CodeMemberMethod[interfaces.Count()];

            for (int i = 0; i < interfaces.Count(); i++)
            {

                methods[i] = new CodeMemberMethod();
                methods[i].Name = methodName;
                methods[i].Attributes = MemberAttributes.Public | MemberAttributes.Static;
                CodeParameterDeclarationExpression para1 = new CodeParameterDeclarationExpression(new CodeTypeReference("this " + interfaces[i].Name), interfaces[i].Name.Substring(3));
                methods[i].Parameters.Add(para1);
                CodeParameterDeclarationExpression para2 = new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "Name");
                methods[i].Parameters.Add(para2);
                CodeParameterDeclarationExpression para3 = new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(int)), "TypeId");
                methods[i].Parameters.Add(para3);
                methods[i].ReturnType = new CodeTypeReference("T");

                CodeTypeParameter param = new CodeTypeParameter("T");
                param.Constraints.Add(" class");
                methods[i].TypeParameters.Add(param);

                methods[i].Statements.Add(new CodeMethodReturnStatement(
                        new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(null, "findByNameExTemplate", new CodeTypeReference("T")),
                            new CodeVariableReferenceExpression("Name"),
                            new CodeVariableReferenceExpression("TypeId"),
                            new CodeArgumentReferenceExpression(interfaces[i].Name.Substring(3) + "." + methodName)


                    )));
            }



            return methods;
        }

        public CodeMemberMethod[] GetFindByIdMethods()
        {
            string methodName = "FindById";

            SAPAutomationHelper.Current.SetSAPApiAssembly();
            Assembly asm = SAPAutomationHelper.Current.SAPGuiApiAssembly;

            var interfaces = asm.GetTypes().Where(t => t.IsInterface && t.Name.StartsWith("Gui") && t.GetInterfaces()[0].GetMethod(methodName) != null).ToArray();

            CodeMemberMethod[] methods = new CodeMemberMethod[interfaces.Count()];

            for (int i = 0; i < interfaces.Count(); i++)
            {

                methods[i] = new CodeMemberMethod();
                methods[i].Name = methodName;
                methods[i].Attributes = MemberAttributes.Public | MemberAttributes.Static;
                CodeParameterDeclarationExpression para1 = new CodeParameterDeclarationExpression(new CodeTypeReference("this " + interfaces[i].Name), interfaces[i].Name.Substring(3));
                methods[i].Parameters.Add(para1);
                CodeParameterDeclarationExpression para2 = new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "Id");
                methods[i].Parameters.Add(para2);
                methods[i].ReturnType = new CodeTypeReference("T");
                CodeTypeParameter param = new CodeTypeParameter("T");
                param.Constraints.Add(" class");
                methods[i].TypeParameters.Add(param);

                methods[i].Statements.Add(new CodeMethodReturnStatement(
                        new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(null, "findByIdTemplate", new CodeTypeReference("T")),
                            new CodeVariableReferenceExpression("Id"),
                            new CodeArgumentReferenceExpression(interfaces[i].Name.Substring(3) + "." + methodName)


                    )));
            }



            return methods;
        }

        public CodeMemberMethod[] GetFindAllByNameExMethods()
        {
            string methodName = "FindAllByNameEx";

            SAPAutomationHelper.Current.SetSAPApiAssembly();
            Assembly asm = SAPAutomationHelper.Current.SAPGuiApiAssembly;

            var interfaces = asm.GetTypes().Where(t => t.IsInterface && t.Name.StartsWith("Gui") && t.GetInterfaces()[0].GetMethod(methodName) != null).ToArray();

            CodeMemberMethod[] methods = new CodeMemberMethod[interfaces.Count()];

            for (int i = 0; i < interfaces.Count(); i++)
            {

                methods[i] = new CodeMemberMethod();
                methods[i].Name = methodName;
                methods[i].Attributes = MemberAttributes.Public | MemberAttributes.Static;
                CodeParameterDeclarationExpression para1 = new CodeParameterDeclarationExpression(new CodeTypeReference("this " + interfaces[i].Name), interfaces[i].Name.Substring(3));
                methods[i].Parameters.Add(para1);
                CodeParameterDeclarationExpression para2 = new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "Name");
                methods[i].Parameters.Add(para2);
                CodeParameterDeclarationExpression para3 = new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(int)), "TypeId");
                methods[i].Parameters.Add(para3);
                methods[i].ReturnType = new CodeTypeReference("IEnumerable<T>");
                CodeTypeParameter param = new CodeTypeParameter("T");
                param.Constraints.Add(" class");
                methods[i].TypeParameters.Add(param);

                methods[i].Statements.Add(new CodeMethodReturnStatement(
                        new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(null, "findAllByNameExTemplate", new CodeTypeReference("T")),
                            new CodeVariableReferenceExpression("Name"),
                            new CodeVariableReferenceExpression("TypeId"),
                            new CodeArgumentReferenceExpression(interfaces[i].Name.Substring(3) + "." + methodName)


                    )));
            }
            return methods;
        }

        public CodeMemberMethod[] GetFindAllByNameMethods()
        {
            string methodName = "FindAllByName";

            SAPAutomationHelper.Current.SetSAPApiAssembly();
            Assembly asm = SAPAutomationHelper.Current.SAPGuiApiAssembly;

            var interfaces = asm.GetTypes().Where(t => t.IsInterface && t.Name.StartsWith("Gui") && t.GetInterfaces()[0].GetMethod(methodName) != null).ToArray();

            CodeMemberMethod[] methods = new CodeMemberMethod[interfaces.Count()];

            for (int i = 0; i < interfaces.Count(); i++)
            {

                methods[i] = new CodeMemberMethod();
                methods[i].Name = methodName;
                methods[i].Attributes = MemberAttributes.Public | MemberAttributes.Static;
                CodeParameterDeclarationExpression para1 = new CodeParameterDeclarationExpression(new CodeTypeReference("this " + interfaces[i].Name), interfaces[i].Name.Substring(3));
                methods[i].Parameters.Add(para1);
                CodeParameterDeclarationExpression para2 = new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "Name");
                methods[i].Parameters.Add(para2);
                methods[i].ReturnType = new CodeTypeReference("IEnumerable<T>");
                CodeTypeParameter param = new CodeTypeParameter("T");
                param.Constraints.Add(" class");
                methods[i].TypeParameters.Add(param);

                methods[i].Statements.Add(new CodeMethodReturnStatement(
                        new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(null, "findAllByNameTemplate", new CodeTypeReference("T")),
                            new CodeVariableReferenceExpression("Name"),
                            new CodeArgumentReferenceExpression(interfaces[i].Name.Substring(3) + "." + methodName)


                    )));
            }



            return methods;
        }

        public CodeMemberMethod[] GetFindByNameMethods()
        {
            //CodeMemberMethod method = new CodeMemberMethod();
            //CodeTypeParameter tpPara = new CodeTypeParameter("TReturn");
            //tpPara.Constraints.Add(new CodeTypeReference(" class", CodeTypeReferenceOptions.GenericTypeParameter));
            //method.TypeParameters.Add(tpPara);

            //tpPara = new CodeTypeParameter("TInput");
            //tpPara.Constraints.Add(new CodeTypeReference(" class", CodeTypeReferenceOptions.GenericTypeParameter));
            //method.TypeParameters.Add(tpPara);



           // method.Name = "findByNameTemplate";
           // method.Attributes = MemberAttributes.Static | MemberAttributes.Private;
           // method.ReturnType = new CodeTypeReference("TReturn");
           // method.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference("TInput"), "Container"));
           // method.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "Name"));

           // CodeConditionStatement conditionalStatement = new CodeConditionStatement(
           //     // The condition to test. 

           //    new CodeSnippetExpression("Container is GuiVContainer"),

           //     // The statements to execute if the condition evaluates to true. 
           //new CodeStatement[] { 
           //    new CodeSnippetStatement("var container = Container as GuiVContainer;")
           //   ,new CodeSnippetStatement("TReturn item = container.FindByName(Name, typeof(TReturn).Name) as TReturn;")
           //   ,new CodeSnippetStatement("return item;")},
           //     // The statements to execute if the condition evalues to false. 
           //new CodeStatement[] { new CodeSnippetStatement("throw new Exception(\"Not Found FindByName method in type:\" + typeof(TInput).Name);") });


           // method.Statements.Add(conditionalStatement);
            string methodName = "FindByName";

            SAPAutomationHelper.Current.SetSAPApiAssembly();
            Assembly asm = SAPAutomationHelper.Current.SAPGuiApiAssembly;

            var interfaces = asm.GetTypes().Where(t => t.IsInterface && t.Name.StartsWith("Gui") && t.GetInterfaces()[0].GetMethod(methodName) != null).ToArray();
          
            CodeMemberMethod[] methods = new CodeMemberMethod[interfaces.Count()];

            for (int i = 0; i < interfaces.Count(); i++)
            {
                
                methods[i] = new CodeMemberMethod();
                methods[i].Name = methodName;
                methods[i].Attributes = MemberAttributes.Public | MemberAttributes.Static;
                CodeParameterDeclarationExpression para1 = new CodeParameterDeclarationExpression(new CodeTypeReference("this " + interfaces[i].Name), interfaces[i].Name.Substring(3));
                methods[i].Parameters.Add(para1);
                CodeParameterDeclarationExpression para2 = new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)),"Name");
                methods[i].Parameters.Add(para2);
                methods[i].ReturnType = new CodeTypeReference("T");
                CodeTypeParameter param = new CodeTypeParameter("T");
                param.Constraints.Add(" class");
                methods[i].TypeParameters.Add(param);

                methods[i].Statements.Add(new CodeMethodReturnStatement(
                        new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(null, "findByNameTemplate", new CodeTypeReference("T")),
                            new CodeVariableReferenceExpression("Name"),
                            new CodeArgumentReferenceExpression(interfaces[i].Name.Substring(3) + "."+methodName)
                            
                            
                    )));
            }

            

            return methods;

        }


    }
}

using SAPAutomation.Framework;
using SAPAutomation.Framework.Attributes;
using SAPFEWSELib;
using System;
using System.CodeDom;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPAutomation;

namespace SAPGuiAutomationLib
{
    public static class SAPAutomationExtension
    {
        private static bool isMatch(GuiFrameWindow window,GuiVComponent comp, int X1, int Y1, int X2, int Y2)
        {
            int x1 = comp.ScreenLeft - window.ScreenLeft;
            int y1 = comp.ScreenTop - window.ScreenTop;
            int x2 = x1 + comp.Width;
            int y2 = y1 + comp.Height;
            if (x1 >= X1 && y1 >= Y1 && x2 <= X2 && y2 <= Y2)
                return true;
            return false;
        }

        public static IEnumerable<GuiVComponent> FindByRegion(this GuiFrameWindow Window,int X1,int Y1,int X2,int Y2)
        {
            return Window.FindDescendantsByProperty<GuiVComponent>(c => isMatch(Window,c, X1,Y1,X2,Y2));
        }

        public static CodeExpression FindByIdCode(this GuiComponent Component)
        {
            return getFindByIdCode(Component);
        }

        public static CodeExpression FindByIdCode(this SapCompInfo CompInfo)
        {
            GuiComponent comp = SAPAutomationHelper.Current.GetSAPComponentById<GuiComponent>(CompInfo.Id);
            return getFindByIdCode(comp);
        }

        public static CodeExpression FindByNameDetailCode(this GuiComponent Component)
        {
            return getFindByNameDetailCode(Component);
        }

        public static CodeExpression FindByNameDetailCode(this SapCompInfo CompInfo)
        {
            GuiComponent comp = SAPAutomationHelper.Current.GetSAPComponentById<GuiComponent>(CompInfo.Id);
            return getFindByNameDetailCode(comp);
        }

        public static CodeExpression FindByNameCode(this GuiComponent Component)
        {
            return getFindByNameCode(Component);
        }

        public static CodeExpression FindByNameCode(this SapCompInfo CompInfo)
        {
            GuiComponent comp = SAPAutomationHelper.Current.GetSAPComponentById<GuiComponent>(CompInfo.Id);
            return getFindByNameCode(comp);
        }

        private static CodeExpression getFindByIdCode(GuiComponent Component)
        {
            if (Component == null)
                return null;
            CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(
                   new CodeMethodReferenceExpression(
                       new CodeVariableReferenceExpression("SAPTestHelper.Current.SAPGuiSession")
                       , "FindById"
                       , new CodeTypeReference(Component.GetDetailType()))
                       , new CodePrimitiveExpression(formatId(Component.Id)));
            return expression;
        }

        private static string formatId(string id)
        {
            int index = id.IndexOf('w');
            return id.Substring(index, id.Length - index);
        }

        private static CodeExpression getFindByNameDetailCode(GuiComponent Component)
        {
            if (Component == null)
                return null;
            try
            {
                Stack<SapCompInfo> comps = new Stack<SapCompInfo>();
                do
                {
                    SapCompInfo ci = new SapCompInfo();
                    ci.Id = Component.Id;
                    ci.Name = Component.Name;
                    ci.Type = Component.GetDetailType();
                    comps.Push(ci);
                    Component = Component.Parent;
                }
                while ((Component is GuiSession) == false);

                SapCompInfo top = comps.Pop();
                CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(
                    new CodeMethodReferenceExpression(
                        new CodeVariableReferenceExpression("SAPTestHelper.Current.SAPGuiSession")
                        , "FindById"
                        , new CodeTypeReference(top.Type))
                        , new CodePrimitiveExpression(top.Name));


                while (comps.Count > 0)
                {
                    SapCompInfo temp = comps.Pop();
                    expression = new CodeMethodInvokeExpression(
                    new CodeMethodReferenceExpression(
                          expression
                          , "FindByName"
                          , new CodeTypeReference(temp.Type))
                          , new CodePrimitiveExpression(temp.Name));

                }
                return expression;
            }
            catch
            {
                return null;
            }
        }


        private static CodeExpression getFindByNameCode(GuiComponent Component)
        {
            if (Component == null)
                return null;
            try
            {
                string name = Component.Name;
                string type = Component.GetDetailType();

                while(!(Component.Parent is GuiSession))
                {
                    Component = Component.Parent;
                }

                string basicMethod = "SAPTestHelper.Current.";
                if (Component is GuiModalWindow)
                    basicMethod += "PopupWindow";
                else
                    basicMethod += "MainWindow";


                CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(
                    new CodeMethodReferenceExpression(
                        new CodeVariableReferenceExpression(basicMethod)
                        , "FindByName"
                        , new CodeTypeReference(type))
                        , new CodePrimitiveExpression(name));


                return expression;
            }
            catch
            {
                return null;
            }
        }



        public static string GetDetailType(this GuiComponent comp)
        {
            if (comp.Type == "GuiSplitterShell")
            {
                return "GuiSplit";
            }
            else if (comp is GuiShell)
            {
                string type = "Gui" + (comp as GuiShell).SubType;
                if (type == "GuiTextEdit")
                    type = "GuiTextedit";
                if (type == "GuiToolbar")
                    type = "GuiToolbarControl";
                return type;
            }
            else
            {
                return comp.Type;
            }
        }

        

        public static CodeTypeDeclaration CreateModule(this IEnumerable<RecordStep> steps,string className)
        {
            if(steps == null || steps.Count()==0)
                return null;
            CodeTypeDeclaration targetClass = new CodeTypeDeclaration(className);
            targetClass.IsClass = true;
            //targetClass.BaseTypes.Add(new CodeTypeReference(typeof(SAPModule)));
            CodeMemberMethod method = new CodeMemberMethod() { 
                Name = "RunAction",
                Attributes = MemberAttributes.Public | MemberAttributes.Static
            };
            
            string parameterName = null;
            foreach(var step in steps)
            {
                if(step.IsParameterize)
                {
                    parameterName = "Data";
                    method.Statements.AddRange(step.GetCodeStatement(parameterName).ToArray());
                    foreach(var p in step.ActionParams)
                    {
                        targetClass.Members.Add(p.CreatePropertyCode());
                    }
                }
                else
                {
                    method.Statements.AddRange(step.GetCodeStatement().ToArray());
                }
            }
            if (parameterName != null)
                method.Parameters.Add(new CodeParameterDeclarationExpression() { Type = new CodeTypeReference(className),Name=parameterName });
            targetClass.Members.Add(method);
            return targetClass;
        }

        public static CodeTypeDeclaration GetDataClass(string className, IEnumerable<SAPDataParameter> paras,SAPModuleAttribute attribute)
        {
            if (paras == null || paras.Count() == 0)
                return null;
            CodeTypeDeclaration dataClass = new CodeTypeDeclaration(className);
            dataClass.IsClass = true;
            dataClass.Attributes = MemberAttributes.Public;
            CodeAttributeDeclaration declare = new CodeAttributeDeclaration() { };
            declare.Name = "SAPModule";
            foreach(var at in typeof(SAPModuleAttribute).GetProperties().Where(p=>p.CanWrite))
            {
                declare.Arguments.Add(new CodeAttributeArgument() { 
                    Name = at.Name,
                    Value = new CodePrimitiveExpression(at.GetValue(attribute))
                });
            }
            dataClass.CustomAttributes.Add(declare);

            
            foreach (var p in paras)
            {
                dataClass.Members.Add(p.CreatePropertyCode());
            }
            return dataClass;
        }

    }
}

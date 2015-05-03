using SAPFEWSELib;
using SAPTestRunTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;

namespace SAPGuiAutomationLib
{
    public class SapCompInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public static class SapCompInfoExtension
    {
        private static string getDetailType(GuiComponent comp)
        {
            if (comp.Type == "GuiSplitterShell")
            {
                return "GuiSplit";
            }
            else if (comp is GuiShell)
            {
                return "Gui" + (comp as GuiShell).SubType;
            }
            else
            {
                return comp.Type;
            }
        }

        public static CodeExpression GetFindCode(this SapCompInfo CompInfo)
        {
            Stack<SapCompInfo> comps = new Stack<SapCompInfo>();
            GuiComponent comp = SAPTestHelper.Current.GetElementById(CompInfo.Id);
            do
            {
                SapCompInfo ci = new SapCompInfo();
                ci.Id = comp.Id;
                ci.Name = comp.Name;

                ci.Type = getDetailType(comp);
                comps.Push(ci);
                comp = comp.Parent;
            }
            while ((comp is GuiSession) == false);

            SapCompInfo top = comps.Pop();
            CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(
                new CodeMethodReferenceExpression(
                    new CodeVariableReferenceExpression("SAPTestHelper.Current.SAPGuiSession")
                    , "FindById"
                    , new CodeTypeReference(top.Type))
                    , new CodePrimitiveExpression(top.Name));

            
            while(comps.Count>0)
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
    }
}

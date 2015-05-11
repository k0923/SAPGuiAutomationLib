using SAPFEWSELib;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPGuiAutomationLib
{
    public static class SAPAutomationExtension
    {
       public static CodeExpression GetFindCode(this GuiComponent Component)
       {
           return getCodeExpression(Component);
       }

       public static CodeExpression GetFindCode(this SapCompInfo CompInfo)
       {
           GuiComponent comp = SAPAutomationHelper.Current.GetSAPComponentById<GuiComponent>(CompInfo.Id);
           return getCodeExpression(comp);
       }
       
       private static CodeExpression getCodeExpression(GuiComponent Component)
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
                   ci.Type = getDetailType(Component);
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
    }
}

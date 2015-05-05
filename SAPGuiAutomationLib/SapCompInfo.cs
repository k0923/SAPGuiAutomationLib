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

        public SapCompInfo() { }

        public void RunGetMethod()
        {
            generateMethod();
        }

        private void generateMethod()
        {
            GuiComponent comp = SAPTestHelper.Current.TryGetElementById(Id);
            

            if (comp == null)
            {
                _findMethod = null;
            }
            else
            {
                try
                {
                    Stack<SapCompInfo> comps = new Stack<SapCompInfo>();
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
                    _findMethod = expression;
                }
                catch
                {
                    _findMethod = null;
                }
                
            }
            
        }

        private CodeExpression _findMethod;

        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public CodeExpression FindMethod { get { return _findMethod; } }

        private string getDetailType(GuiComponent comp)
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

    //public static class SapCompInfoExtension
    //{
    //    private static string getDetailType(GuiComponent comp)
    //    {
    //        if (comp.Type == "GuiSplitterShell")
    //        {
    //            return "GuiSplit";
    //        }
    //        else if (comp is GuiShell)
    //        {
    //            return "Gui" + (comp as GuiShell).SubType;
    //        }
    //        else
    //        {
    //            return comp.Type;
    //        }
    //    }

    //    public static CodeExpression GetFindCode(this SapCompInfo CompInfo)
    //    {
    //        Stack<SapCompInfo> comps = new Stack<SapCompInfo>();

           

    //        GuiComponent comp = SAPTestHelper.Current.GetElementById(CompInfo.Id);
    //        do
    //        {
    //            SapCompInfo ci = new SapCompInfo();
    //            ci.Id = comp.Id;
    //            ci.Name = comp.Name;

    //            ci.Type = getDetailType(comp);
    //            comps.Push(ci);
    //            comp = comp.Parent;
    //        }
    //        while ((comp is GuiSession) == false);

    //        SapCompInfo top = comps.Pop();
    //        CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(
    //            new CodeMethodReferenceExpression(
    //                new CodeVariableReferenceExpression("SAPTestHelper.Current.SAPGuiSession")
    //                , "FindById"
    //                , new CodeTypeReference(top.Type))
    //                , new CodePrimitiveExpression(top.Name));

            
    //        while(comps.Count>0)
    //        {
    //            SapCompInfo temp = comps.Pop();
    //            expression = new CodeMethodInvokeExpression(
    //            new CodeMethodReferenceExpression(
    //                  expression
    //                  , "FindByName"
    //                  , new CodeTypeReference(temp.Type))
    //                  , new CodePrimitiveExpression(temp.Name));

    //        }
    //        return expression;
            
            
           
    //    }
    //}
}

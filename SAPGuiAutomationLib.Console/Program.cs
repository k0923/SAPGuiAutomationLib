using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPFEWSELib;
using System.Reflection;
using System.Reflection.Emit;
using SAPTestRunTime;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;

namespace SAPGuiAutomationLib.Con
{
    class Program
    {
        static void Main(string[] args)
        {
            
             SAPTestHelper.Current.SetSession();

             SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").
                 FindByName<GuiUserArea>("usr").FindChildByCondition<GuiTextField>(c => c.DefaultTooltip.Contains("Exchange Rate")).Text = "M";


            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").FindByName<GuiToolbar>("tbar[0]").FindByName<GuiOkCodeField>("okcd").Text = "SE16";

            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").FindByName<GuiToolbar>("tbar[0]").FindByName<GuiButton>("btn[0]").Press();


            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").FindByName<GuiUserArea>("usr").FindByName<GuiCTextField>("DATABROWSE-TABLENAME").Text = "TCURR";

            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").FindByName<GuiToolbar>("tbar[0]").FindByName<GuiButton>("btn[0]").Press();

            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").FindByName<GuiUserArea>("usr").FindByName<GuiCTextField>("I1-LOW").Text = "M";
            
            
           
            
            
            
            
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").FindByName<GuiUserArea>("usr").FindByName<GuiCTextField>("DATABROWSE-TABLENAME").Text = "abc";

            GuiComponent comp = SAPTestHelper.Current.GetElementById("/app/con[0]/ses[0]/wnd[0]/usr");



            SapCompInfo ci = new SapCompInfo();
            ci.Type = comp.Type;
            ci.Name = comp.Name;
            ci.Id = comp.Id;

            DisplayCode(ci.GetFindCode());
            Console.ReadLine();
            //SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").FindByName<GuiUserArea>("usr");






            //SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").FindByName<GuiUserArea>("usr")
           
            
            
            
            
            
            
            
            
            var shell = SAPTestHelper.Current.GetElementById<GuiContainerShell>("wnd[0]/usr/cntlIMAGE_CONTAINER/shellcont");
            var test = shell.FindByName("shell", "GuiSplitShell");
            //var area = SAPTestHelper.Current.SAPGuiSession.FindById("wnd[0]").FindByName<GuiUserArea>("usr");
           // Console.WriteLine(area.SubType);
            GuiTree tree = SAPTestHelper.Current.GetElementById<GuiTree>("wnd[0]/usr/cntlIMAGE_CONTAINER/shellcont/shell/shellcont[0]/shell");
            var area1 = SAPTestHelper.Current.GetElementById<GuiUserArea>("wnd[0]/usr").FindByName("shell", "GuiTree");
            
            
        }
        static void DisplayCode(CodeExpression Expression)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("c#");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "c";
            StringBuilder sb = new StringBuilder();
            
            using (TextWriter sourceWriter = new StringWriter(sb))
            {
                provider.GenerateCodeFromExpression(Expression, sourceWriter, options);
                
            }
            Console.WriteLine(sb.ToString());
        }
        static void SAPGuiSession_Hit(GuiSession Session, GuiComponent Component, string InnerObject)
        {
            
        }


        static void ShowProp(string typeName,object obj)
        {
            
            
        }
    }
}

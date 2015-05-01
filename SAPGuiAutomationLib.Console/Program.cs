using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPFEWSELib;
using System.Reflection;
using System.Reflection.Emit;
using SAPTestRunTime;

namespace SAPGuiAutomationLib.Con
{
    class Program
    {
        static void Main(string[] args)
        {

            
            SAPTestHelper.Current.SetSession();

            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").FindByName<GuiUserArea>("usr");

            var shell = SAPTestHelper.Current.GetElementById<GuiContainerShell>("wnd[0]/usr/cntlIMAGE_CONTAINER/shellcont");
            var test = shell.FindByName("shell", "GuiSplitShell");
            //var area = SAPTestHelper.Current.SAPGuiSession.FindById("wnd[0]").FindByName<GuiUserArea>("usr");
           // Console.WriteLine(area.SubType);
            GuiTree tree = SAPTestHelper.Current.GetElementById<GuiTree>("wnd[0]/usr/cntlIMAGE_CONTAINER/shellcont/shell/shellcont[0]/shell");
            var area1 = SAPTestHelper.Current.GetElementById<GuiUserArea>("wnd[0]/usr").FindByName("shell", "GuiTree");
            
            
        }

        static void SAPGuiSession_Hit(GuiSession Session, GuiComponent Component, string InnerObject)
        {
            
        }


        static void ShowProp(string typeName,object obj)
        {
            
            
        }
    }
}

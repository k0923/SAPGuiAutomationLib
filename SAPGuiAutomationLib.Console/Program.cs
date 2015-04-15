using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPFEWSELib;
using System.Reflection;

namespace SAPGuiAutomationLib.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            SAPTestHelper.Current.SetSession();
            var comp = SAPTestHelper.Current.GetElementById("wnd[0]");
            
            SAPTestHelper.Current.SetSAPApiAssembly(@"C:\Users\Young\Documents\visual studio 2013\Projects\SAPGuiAutomationLib\SAPGuiAutomationLib\obj\Debug\Interop.SAPFEWSELib.dll");
            var ms = SAPTestHelper.Current.GetSAPTypeInfoes<MethodInfo>(comp, (t) =>  t.GetMethods().Where(m=>m.IsSpecialName == false).ToList() );
            SAPTestHelper.Current.GetElementById<GuiMenubar>("wnd[0]/usr");
        }
    }
}

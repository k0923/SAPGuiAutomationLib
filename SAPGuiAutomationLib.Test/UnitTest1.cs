using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAPGuiAutomationLib;
using SAPFEWSELib;
using System.Reflection;

namespace SAPGuiAutomationLib.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SAPTestHelper.Current.SetSession();
            SAPTestHelper.Current.SetSAPApiAssembly(@"C:\Users\Young\Documents\visual studio 2013\Projects\SAPGuiAutomationLib\SAPGuiAutomationLib\obj\Debug\Interop.SAPFEWSELib.dll");
            SAPTestHelper.Current.GetElementById("/app/con[0]/ses[0]/wnd[0]/tbar[0]/okcd").HighLight();
           
        }

        [TestMethod]
        public void LoadSAPLibTest()
        {
            SAPTestHelper.Current.SetSAPApiAssembly();
            var ms = SAPTestHelper.Current.GetSAPTypeInfoes<MethodInfo>("GuiButton", t => t.GetMethods());
            
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAPGuiAutomationLib;
using SAPFEWSELib;
using System.Reflection;
using SAPAutomation;

namespace SAPGuiAutomationLib.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SAPTestHelper.Current.SetSession();
            var radio = SAPTestHelper.Current.MainWindow.FindByName<GuiTextField>("MKPF-BKTXT");
            string tp = radio.AccLabelCollection.Count.ToString();

           
        }

        [TestMethod]
        public void LoadSAPLibTest()
        {
           
            
        }
    }
}

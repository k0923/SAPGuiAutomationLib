using SAPAutomation;
using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPAutomation.Extension;
using SAPAutomation.Framework.Attributes;

namespace Demo
{
    public class GetCurrency
    {
        [TestData]
        /// From currency
        public System.String CurFrom { get; set; }
        /// To-currency
        [TestData]
        public System.String CurTo { get; set; }

        [TestData(FriendlyName="ScreenShot")]
        public string ScreenShotPath { get; set; }

        public static void RunAction(GetCurrency Data)
        {
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiOkCodeField>("wnd[0]/tbar[0]/okcd").Text = "/n";
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").SendVKey(0);
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiOkCodeField>("wnd[0]/tbar[0]/okcd").Text = "se16";
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").SendVKey(0);
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtDATABROWSE-TABLENAME").Text = "TCURR";
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").SendVKey(0);
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI1-LOW").Text = "M";
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI2-LOW").Text = Data.CurFrom;
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI3-LOW").Text = Data.CurTo;
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI3-LOW").SetFocus();
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI3-LOW").CaretPosition = 3;
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiButton>("wnd[0]/tbar[1]/btn[8]").Press();
            SAPTestHelper.Current.TakeScreenShot(Data.ScreenShotPath);
        }
    }
}

using SAPFEWSELib;
using SAPTestRunTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Test
{


    public class Exchange
    {

        private void RunAction(Exchange_Data Data)
        {
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").ResizeWorkingPane(216, 24, false);
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiOkCodeField>("wnd[0]/tbar[0]/okcd").Text = "/n";
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").SendVKey(0);
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiOkCodeField>("wnd[0]/tbar[0]/okcd").Text = "se16";
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").SendVKey(0);
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtDATABROWSE-TABLENAME").Text = "TCURR";
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").SendVKey(0);
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI1-LOW").Text = Data.Rate_Type;
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI2-LOW").Text = Data.CurFrom;
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI3-LOW").Text = Data.CurTo;
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI3-LOW").SetFocus();
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiCTextField>("wnd[0]/usr/ctxtI3-LOW").CaretPosition = 3;
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiButton>("wnd[0]/tbar[1]/btn[8]").Press();
        }
    }

    [SAPModule(ModuleName = "Exchange", ModuleVersion = null, Author = null, Email = null, ScreenNumber = null, TCode = null)]
    public class Exchange_Data
    {

        /// Exchange Rate Type
        public System.String Rate_Type { get; set; }
        /// From currency
        public System.String CurFrom { get; set; }
        /// To-currency
        public System.String CurTo { get; set; }
    }
}

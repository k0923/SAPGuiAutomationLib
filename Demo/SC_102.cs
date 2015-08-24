using SAPAutomation;
using SAPAutomation.Framework;
using SAPAutomation.Framework.Attributes;
using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Young.Data.Attributes;

namespace Demo
{
    [DataBinding("SC_102")]
    public class SC_102:SAPGuiScreen
    {
        [ColumnBinding("Order")]
        public string Order
        {
            get
            {
               return SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("VBAK-VBELN").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("VBAK-VBELN").Text = value;
            }
        }


        public void Sales()
        {
            SAPTestHelper.Current.MainWindow.FindByName<GuiButton>("btn[5]").Press();
            if (SAPTestHelper.Current.PopupWindow != null)
                SAPTestHelper.Current.PopupWindow.Close();
        }
    }
}

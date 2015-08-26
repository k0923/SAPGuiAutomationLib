using SAPAutomation;
using SAPAutomation.Framework;
using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Young.Data.Attributes;

namespace Demo
{
    public class EnterGoodsIssue_NewItem:SAPGuiScreen
    {
        [ColumnBinding]
        [SingleSampleData("30")]
        public string ReasonForMvmt
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("MSEG-GRUND").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("MSEG-GRUND").Text = value;
            }
        }

        [MethodBinding(Order=99)]
        public void Process()
        {
            this.SendKeys(SAPKeys.Enter);
        }
    }
}

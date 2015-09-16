using SAPAutomation;
using SAPAutomation.Framework;
using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Young.Data.Attributes;

namespace MB1A
{
    public class EnterGoodsIssue_NewItem:SAPGuiScreen,IFillData
    {
        [ColumnBinding]
        [SingleSampleData("30")]
        public string ReasonForMvmt { get; set; }

        public void FillData()
        {
            if (!string.IsNullOrEmpty(ReasonForMvmt))
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("MSEG-GRUND").Text = ReasonForMvmt;
        }
    }
}

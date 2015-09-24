using SAPAutomation;
using SAPFEWSELib;
using SAPTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Young.Data;
using Young.Data.Attributes;

namespace MB1A
{
    
    public class EnterGoodsIssue_Initial:SAPGuiScreen,IFillData
    {
       
        [ColumnBinding]
        [SingleSampleData("551")]
        public string MovementType { get; set; }

        [ColumnBinding]
        [SingleSampleData("5206")]
        public string Plant { get; set; }

        [ColumnBinding]
        [SingleSampleData("MVTF")]
        public string StorageLocation { get; set; }


        [ColumnBinding(Order =10,Directory =DataDirectory.Output)]
        public string Document()
        {
            var doc = Regex.Replace(StatusMessage, @"\D+", "");
            return doc;
        }

        public void FillData()
        {
            if (!string.IsNullOrEmpty(MovementType))
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("RM07M-BWARTWA").Text = MovementType;
            if (!string.IsNullOrEmpty(Plant))
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("RM07M-WERKS").Text = Plant;
            if (!string.IsNullOrEmpty(StorageLocation))
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("RM07M-LGORT").Text = StorageLocation;
        }

        public void NewItem()
        {
            SAPTestHelper.Current.MainWindow.FindByName<GuiButton>("btn[8]").Press();
        }
    }
}

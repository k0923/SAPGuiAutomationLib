using SAPAutomation;
using SAPAutomation.Framework;
using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Young.Data;
using Young.Data.Attributes;

namespace Demo
{
    
    public class EnterGoodsIssue_Initial:SAPGuiScreen
    {
        public EnterGoodsIssue_Initial()
        {
            this.StartTransaction("MB1A");
        }

        [ColumnBinding]
        [SingleSampleData("551")]
        public string MovementType
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("RM07M-BWARTWA").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("RM07M-BWARTWA").Text = value;
            }
        }

        [ColumnBinding]
        [SingleSampleData("5206")]
        public string Plant
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("RM07M-WERKS").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("RM07M-WERKS").Text = value;
            }
        }

        [ColumnBinding]
        [SingleSampleData("MVTF")]
        public string StorageLocation
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("RM07M-LGORT").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("RM07M-LGORT").Text = value;
            }
        }

        [MethodBinding(Order=5)]
        public EnterGoodsIssue_NewItems Process()
        {
            this.SendKeys(SAPKeys.Enter);
            return new EnterGoodsIssue_NewItems();
        }

        [ColumnBinding(Order =10,Directory =DataDirectory.Output)]
        public string Document
        {
            get
            {
                return Regex.Replace(StatusMessage,@"\D+","");
            }
        }
    }
}

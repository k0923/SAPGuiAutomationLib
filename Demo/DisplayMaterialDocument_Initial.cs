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
    public class DisplayMaterialDocument_Initial:SAPGuiScreen
    {
        public DisplayMaterialDocument_Initial()
        {
            this.StartTransaction("MB03");
        }

        [ColumnBinding("Document")]
        [SingleSampleData("38643064")]
        public string MaterialDoc
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("RM07M-MBLNR").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("RM07M-MBLNR").Text = value;
            }
        }

        [ColumnBinding]
        [SingleSampleData("2015")]
        public string MatDocYear
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiTextField>("RM07M-MJAHR").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiTextField>("RM07M-MJAHR").Text = value;
            }
        }

        [MethodBinding]
        public DisplayMaterialDocument_Overview Process()
        {
            this.SendKeys(SAPKeys.Enter);
            return new DisplayMaterialDocument_Overview();
        }
    }
}

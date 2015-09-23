using SAPAutomation;
using SAPFEWSELib;
using SAPTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Young.Data.Attributes;

namespace Demo.MB03
{
    public class DisplayMaterialDocument_Initial_Screen : SAPGuiScreen, IFillData
    {
        [ColumnBinding("Document")]
        [SingleSampleData("38643064")]
        public string MaterialDoc { get; set; }

        [ColumnBinding]
        [SingleSampleData("2015")]
        public string MatDocYear { get; set; }

        public void FillData()
        {
            ///如果MaterialDoc为空，就不填数据以节省开销
            if(!string.IsNullOrEmpty(MaterialDoc))
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("RM07M-MBLNR").Text = MaterialDoc;

            if(!string.IsNullOrEmpty(MatDocYear))
                SAPTestHelper.Current.MainWindow.FindByName<GuiTextField>("RM07M-MJAHR").Text = MatDocYear;
        }

        public void Overview()
        {
            SAPTestHelper.Current.MainWindow.FindByName<GuiButton>("btn[5]").Press();
        }

        public void Header()
        {
            SAPTestHelper.Current.MainWindow.FindByName<GuiButton>("btn[16]").Press();
        }

    }
}

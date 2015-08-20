using SAPAutomation;
using SAPAutomation.Framework;
using SAPAutomation.Framework.Attributes;
using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Demo
{
    [TableBinding("SC_4001")]
    public class SC_4001:SAPGuiScreen
    {
        [ColumnBinding]
        public string QuoteNum
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiTextField>("VBAK-ZZHPQUOTE").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiTextField>("VBAK-ZZHPQUOTE").Text = value;
            }
        }

        [ColumnBinding]
        public string WatsonServer
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("VBAK-ZZWATSERV").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("VBAK-ZZWATSERV").Text = value;
            }
        }
    }

    public class SC_4001_Sales:SC_4001
    {
        [ColumnBinding]
        public string Paymentcard1
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("CCDATA-CCINS").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("CCDATA-CCINS").Text = value;
            }
        }

        [ColumnBinding]
        public string Paymentcard2
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("CCDATA-CCNUM").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("CCDATA-CCNUM").Text = value;
            }
        }

        private DataRow[] rows;

        [MultiColumnBinding(new string[]{"Item","Material","Description","Hrt It No"},"Test")]
        public DataRow[] Rows
        {
            get
            {
                return rows;
            }
            set
            {
                var tableControl = SAPTestHelper.Current.MainWindow.FindByName<GuiTableControl>("SAPMV45ATCTRL_U_ERF_GUTLAST");
                int startRow = 1;
                foreach(var row in value)
                {
                    var temp = row["Item"].ToString();
                    tableControl.GetCell(startRow, 0).Text = row["Item"].ToString();
                    tableControl.GetCell(startRow, 1).Text = row["Material"].ToString();
                    tableControl.GetCell(startRow, 2).Text = row["Description"].ToString();
                    tableControl.GetCell(startRow, 9).Text = row["Hrt It No"].ToString();
                    startRow++;
                }
                rows = value;
            
            }
        }
    }
}

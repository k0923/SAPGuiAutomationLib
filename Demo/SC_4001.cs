using SAPAutomation;
using SAPAutomation.Framework;
using SAPAutomation.Framework.Attributes;
using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Young.Data.Attributes;

namespace Demo
{
    
    public class SC_4001:SAPGuiScreen
    {
        [ColumnBinding]
        [SingleSampleData(Value = "110077584")]
        public string SoldToParty
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("KUAGV-KUNNR").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("KUAGV-KUNNR").Text = value;
            }
        }

        [ColumnBinding]
        [SingleSampleData("500559544")]
        public string ShipToParty
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("KUWEV-KUNNR").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("KUWEV-KUNNR").Text = value;
            }
        }

        [ColumnBinding]
        [SingleSampleData("Test20150824145804")]
        public string PoNumber
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiTextField>("VBKD-BSTKD").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiTextField>("VBKD-BSTKD").Text = value;
            }
        }

        [ColumnBinding]
        [SingleSampleData("08/24/2015")]
        public string PoDate
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("VBKD-BSTDK").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("VBKD-BSTDK").Text = value;
            }
        }

        [ColumnBinding]
        [SingleSampleData("08/24/2015")]
        public string HPReceiveDate
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("VBAK-ZZHPRECDT").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("VBAK-ZZHPRECDT").Text = value;
            }
        }







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
        private DataRow[] rows;


        [ColumnBinding]
        [ComplexSampleData(Content = new string[] { "Material", "Target quantity" }, DataType = SampleDataType.Header)]
        [ComplexSampleData(Content = new string[] { "AF573A", "1" }, DataType = SampleDataType.Body)]
        public DataRow[] Rows
        {
            get
            {
                return rows;
            }
            set
            {
                
                var tableControl = SAPTestHelper.Current.MainWindow.FindByName<GuiTableControl>("SAPMV45ATCTRL_U_ERF_GUTLAST");
                int startRow = 0;
                foreach(var row in value)
                {
                    //tableControl.GetCell(startRow, 0).Text = row["Item"].ToString();
                    tableControl.GetCell(startRow, 1).Text = row["Material"].ToString();
                    tableControl.GetCell(startRow, 4).Text = row["Target quantity"].ToString();
                    //tableControl.GetCell(startRow, 2).Text = row["Description"].ToString();
                    //tableControl.GetCell(startRow, 9).Text = row["Hrt It No"].ToString();
                    startRow++;
                }
                rows = value;
            
            }
        }

       
        public void Process()
        {
            SAPTestHelper.Current.MainWindow.FindByName<GuiButton>("BT_HEAD").Press();
            if(SAPTestHelper.Current.PopupWindow!=null)
            {
                var btn = SAPTestHelper.Current.PopupWindow.FindDescendantsByProperty<GuiButton>(b => b.Tooltip.ToLower().Contains("continue")).FirstOrDefault();
                if (btn != null)
                {
                    btn.Press();
                    this.SendKeys(SAPKeys.Enter);
                     if(SAPTestHelper.Current.PopupWindow!=null)
                     {
                         SAPTestHelper.Current.PopupWindow.FindDescendantsByProperty<GuiButton>(b => b.Tooltip.ToLower().Contains("continue")).FirstOrDefault().Press();
                     }
                }
                    
            }
            //return new SC_4002();
            //this.SendKeys(SAPKeys.Enter);
        }
    }
}

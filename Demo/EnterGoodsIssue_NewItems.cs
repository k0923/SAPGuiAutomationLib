using SAPAutomation;
using SAPAutomation.Framework;
using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Young.Data.Attributes;

namespace Demo
{
    public class EnterGoodsIssue_NewItems:SAPGuiScreen
    {
        [ColumnBinding]
        [SingleSampleData("3556999999")]
        public string GLAccount
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("MSEGK-KONTO").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("MSEGK-KONTO").Text = value;
            }
        }

        [ColumnBinding]
        [SingleSampleData("SI00")]
        public string BusinessArea
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("COBL-GSBER").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("COBL-GSBER").Text = value;
            }
        }

        [ColumnBinding]
        [SingleSampleData("5200400200")]
        public string CostCenter
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("COBL-KOSTL").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("COBL-KOSTL").Text = value;
            }
        }

        [ColumnBinding(new string[] { "Material", "Quantity" })]
        [ComplexSampleData(Content = new string[] { "Material", "Quantity" }, DataType = SampleDataType.Header)]
        [ComplexSampleData(Content = new string[] { "JG091A     #ABA", "2" }, DataType = SampleDataType.Body)]
        public DataRow[] Materials
        {
            set
            {
                var container = SAPTestHelper.Current.MainWindow.FindByName<GuiSimpleContainer>(":SAPMM07M:0421");
                var materials = container.FindAllByName<GuiCTextField>("MSEG-MATNR");
                var quantities = container.FindAllByName<GuiTextField>("MSEG-ERFMG");
                int index = 0;
                foreach(DataRow dr in value)
                {
                    materials.ElementAt(index).Text = dr["Material"].ToString();
                    
                    quantities.ElementAt(index).Text = dr["Quantity"].ToString();
                }
            }
        }

        [MethodBinding(Order = 4)]
        public EnterGoodsIssue_NewItem Process()
        {
            this.SendKeys(SAPKeys.Enter);
            return new EnterGoodsIssue_NewItem();
        }

        [MethodBinding(Order =5)]
        public void Save()
        {
            this.SendKeys(SAPKeys.Ctrl_S);
        }
    }
}

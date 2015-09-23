using SAPAutomation;
using SAPFEWSELib;
using SAPTestFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Young.Data.Attributes;

namespace MB1A
{
    public class EnterGoodsIssue_NewItems:SAPGuiScreen,IFillData
    {
        [ColumnBinding]
        [SingleSampleData("3556999999")]
        public string GLAccount { get; set; }

        [ColumnBinding]
        [SingleSampleData("SI00")]
        public string BusinessArea { get; set; }

        [ColumnBinding]
        [SingleSampleData("5200400200")]
        public string CostCenter { get; set; }

        [ColumnBinding(new string[] { "Material", "Quantity" })]
        [ComplexSampleData(Content = new string[] { "Material", "Quantity" }, DataType = SampleDataType.Header)]
        [ComplexSampleData(Content = new string[] { "JG091A     #ABA", "2" }, DataType = SampleDataType.Body)]
        public DataRow[] Materials { get; set; }

        public void FillData()
        {
            if (!string.IsNullOrEmpty(GLAccount))
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("MSEGK-KONTO").Text = GLAccount;
            if (!string.IsNullOrEmpty(BusinessArea))
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("COBL-GSBER").Text = BusinessArea;
            if (!string.IsNullOrEmpty(CostCenter))
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("COBL-KOSTL").Text = CostCenter;
            if(Materials!=null)
            {
                var container = SAPTestHelper.Current.MainWindow.FindByName<GuiSimpleContainer>(":SAPMM07M:0421");
                var materials = container.FindAllByName<GuiCTextField>("MSEG-MATNR");
                var quantities = container.FindAllByName<GuiTextField>("MSEG-ERFMG");
                int index = 0;
                foreach (DataRow dr in Materials)
                {
                    materials.ElementAt(index).Text = dr["Material"].ToString();

                    quantities.ElementAt(index).Text = dr["Quantity"].ToString();
                }
            }
        }
    }
}

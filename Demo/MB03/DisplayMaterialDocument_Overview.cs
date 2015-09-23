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
    public class DisplayMaterialDocument_Overview:SAPGuiScreen,IFillData
    {
        public void AccountingDocuments()
        {
            SAPTestHelper.Current.MainWindow.FindByName<GuiButton>("btn[7]").Press();
        }

        public void FillData()
        {
            
        }

        
        public void SelectAccount()
        {
            var GridView = SAPTestHelper.Current.PopupWindow.FindByName<GuiGridView>("shell");
            int row = 0;
            for (int i = 0; i < GridView.RowCount; i++)
            {
                string value = GridView.GetCellValueByDisplayColumn(i, "type");
                if (value.IndexOf("Account") >= 0)
                {
                    row = i;
                }
            }
            GridView.DoubleClickCell(row, "Doc");
        }
    }
}

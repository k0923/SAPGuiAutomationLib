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
    public class DisplayDocument_Overview:SAPGuiScreen
    {
        [MethodBinding(Order = 0)]
        public void AccountingDocuments()
        {
            SAPTestHelper.Current.MainWindow.FindByName<GuiButton>("btn[7]").Press();
        }

        [MethodBinding(Order =1)]
        public void SelectAccount()
        {
            var GridView = SAPTestHelper.Current.PopupWindow.FindByName<GuiGridView>("shell");
            int row = 0;
            for(int i = 0;i<GridView.RowCount;i++)
            {
                if(GridView.GetCellValue(i,"Object type text").IndexOf("Accounting")>0)
                {
                    row = i;
                }
            }
            GridView.SelectedRows = row.ToString();
            GridView.DoubleClickCurrentCell();
            
        }
    }
}

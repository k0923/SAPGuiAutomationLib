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


namespace Demo.MB03
{
    public class DisplayDocument_Overview:SAPGuiScreen,IFillData
    {
        private string _iDoc;

        [MethodBinding(Order =0)]
        public void SelectRelationship()
        {
            var control = SAPTestHelper.Current.MainWindow.FindByName<GuiGOSShell>("shellcont[1]").FindByName<GuiToolbarControl>("shell");
            control.PressContextButton("%GOS_TOOLBOX");
            control.SelectContextMenuItem("%GOS_SRELATIONS");
        }

        [ColumnBinding("IDoc",Order = 1, Directory = DataDirectory.Output)]
        public string GetIDoc()
        {
            var grid = SAPTestHelper.Current.PopupWindow.FindDescendantsByProperty<GuiGridView>(r => true).First();
            for (int i = 0; i < grid.RowCount; i++)
            {
                if (grid.GetCellValueByDisplayColumn(i, "Document").IndexOf("IDoc") >= 0)
                {
                    string allDoc = grid.GetCellValueByDisplayColumn(i, "Desc");
                    string iDoc = Regex.Replace(allDoc, @"\D+", "");
                    iDoc = Regex.Replace(iDoc, @"^[0]+", "");
                    _iDoc = iDoc;
                }
            }
            return _iDoc;
        }

        public void FillData()
        {
            
        }

       
        
    }
}

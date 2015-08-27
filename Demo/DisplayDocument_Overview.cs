using SAPAutomation;
using SAPAutomation.Framework;
using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Young.Data.Attributes;


namespace Demo
{
    public class DisplayDocument_Overview:SAPGuiScreen
    {
        [MethodBinding(Order =0)]
        public void SelectRelationship()
        {

            var control = SAPTestHelper.Current.MainWindow.FindByName<GuiGOSShell>("shellcont[1]").FindByName<GuiToolbarControl>("shell");
            control.PressContextButton("%GOS_TOOLBOX");
            control.SelectContextMenuItem("%GOS_SRELATIONS");
        }

        [ColumnBinding(Order =1,Directory = Young.Data.DataDirectory.Output)]
        public string IDoc
        {
            get
            {
                var grid = SAPTestHelper.Current.PopupWindow.FindDescendantsByProperty<GuiGridView>(r => true).First();
                for (int i = 0; i < grid.RowCount;i++)
                {
                    if(grid.GetCellValueByDisplayColumn(i,"Document").IndexOf("IDoc")>=0)
                    {
                        string allDoc = grid.GetCellValueByDisplayColumn(i, "Desc");
                        string iDoc = Regex.Replace(allDoc, @"\D+", "");
                        iDoc = Regex.Replace(iDoc, @"^[0]+","");
                        return iDoc;
                    }
                }
                return "";
            }
        }
    }
}

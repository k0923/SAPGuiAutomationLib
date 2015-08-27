using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;
using SAPFEWSELib;
using SAPAutomation;
using SAPAutomation.Framework;
using Young.Data;
using System.Text.RegularExpressions;
using Young.Data.Attributes;

namespace Demo
{
    public class Script:DataDriven
    {
        [MethodBinding(Order = 1)]
        public EnterGoodsIssue_Initial EnterGoodsIssue()
        {
            return new EnterGoodsIssue_Initial();
        }

        [MethodBinding(Order =2)]
        public DisplayMaterialDocument_Initial DisplayMaterialDoc()
        {
            return new DisplayMaterialDocument_Initial();
        }
    }

    class Program
    {
        
        static void Main(string[] args)
        {
            DataDriven.Data = ExcelHelper.Current.Open(@"C:\Demo\1.xlsx").ReadAll();
            ExcelHelper.Current.Close();
            //DataDriven.GlobalBindingModeType.IsUsingSampleData = true;
            DataDriven.GlobalBindingModeType.RecusionMode = RecusionType.Recusion;
            DataDriven.NonSharedTables = new List<string>();
            DataDriven.CurrentId = 1;
            //DataDriven.GlobalBindingModeType.SettingMode = SettingType.PropertyOnly;

            SAPTestHelper.Current.SetSession();
            SAPTestHelper.Current.TurnScreenLog(ScreenLogLevel.All);
            Script s = new Script();
            s.DataBinding();

            SAPTestHelper.Current.SaveLog(@"1.xml");

            ExcelHelper.Current.Create(@"c:\Demo\2.xlsx");
            foreach(DataTable dataTable in DataDriven.Data.Tables)
            {
                ExcelHelper.Current.Write(dataTable);
            }
            ExcelHelper.Current.Close();
        }


        public static string GetCell(GuiGridView GridView, int row, string FriendlyColName)
        {
            string column = "";
            GridView.SelectAll();
            foreach (var colName in GridView.SelectedColumns)
            {
                string displayCol = GridView.GetDisplayedColumnTitle(colName);
                if (displayCol.IndexOf(FriendlyColName) >= 0)
                {
                    column = colName;
                    break;
                }

            }
            GridView.ClearSelection();
            return GridView.GetCellValue(row, column);
        }

        static void Script1()
        {
            SAPGuiScreen sc = new SAPGuiScreen();
            sc.StartTransaction("MB1A");
            sc = new EnterGoodsIssue_Initial();
            sc.DataBinding();
            sc.SendKeys(SAPKeys.Enter);
            sc = new EnterGoodsIssue_NewItems();
            sc.DataBinding();
            sc.SendKeys(SAPKeys.Enter);
            sc = new EnterGoodsIssue_NewItem();
            sc.DataBinding();
            sc.SendKeys(SAPKeys.Enter);
            sc.SendKeys(SAPKeys.Ctrl_S);

            

            
        }

        

     
    }
}

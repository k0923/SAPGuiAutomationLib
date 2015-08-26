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

namespace Demo
{
    class Program
    {
        
        static void Main(string[] args)
        {
            SAPTestHelper.Current.SetSession();
           
            //SAPTestHelper.Current.TurnScreenLog(ScreenLogLevel.All);
            DataDriven.Data = ExcelHelper.Current.Open(@"C:\Demo\1.xlsx").ReadAll();
            ExcelHelper.Current.Close();
            DataDriven.GlobalBindingModeType.IsUsingSampleData = true;
            DataDriven.GlobalBindingModeType.RecusionMode = RecusionType.Recusion;
            DataDriven.NonSharedTables = new List<string>();
            DataDriven.CurrentId = 1;
            //DataDriven.GlobalBindingModeType.SettingMode = SettingType.PropertyOnly;


            

            SAPGuiScreen sc = new DisplayMaterialDocument_Initial();
            //sc.StartTransaction("MB1A");
            sc.DataBinding();

            //sc = new EnterGoodsIssue_NewItems();
            //sc.DataBinding();

            //sc = new EnterGoodsIssue_NewItem();
            //sc.DataBinding();
            //SAPTestHelper.Current.SaveLog("1.xml");
            

            //SC_4002 s = new SC_4002();
            //s.DataBinding();

            //SAPTestHelper.Current.SetSession();

            //SC_102 sc = new SC_102();
            //sc.StartTransaction("VA02");
            //sc.DataBinding();

            

            
           
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

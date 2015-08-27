using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPFEWSELib;
using System.IO;
using SAPAutomation.Framework;

namespace SAPAutomation
{
    public static partial class SAPFEExtension
    {
        public static void HighLight(this GuiComponent comp)
        {
            GuiVComponent vComp = comp as GuiVComponent;
            if (vComp != null)
                vComp.Visualize(true);
        }

        public static GuiComponent ThrowNotFoundError(this GuiComponent comp,string message)
        {
            //if (comp == null)
                //throw new GuiNotFoundExpection(message);
            return comp;
        }

        public static T OfType<T>(this GuiComponent Component) where T:class
        {
            return Component as T;
        }
        
        public static string GetCellValue(this GuiGridView GridView,int col,int row)
        {
            int index = 0;
            string column = "";
            GridView.SelectAll();
            foreach(var colName in GridView.SelectedColumns)
            {
                if (index == col)
                {
                    column = colName;
                    break;
                }
                   
                index++;
            }
            GridView.ClearSelection();
            return GridView.GetCellValue(row, column);  
        }

        public static string GetCellValueByDisplayColumn(this GuiGridView GridView, int row, string FriendlyColName)
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

        public static void DoubleClickCell(this GuiGridView GridView,int row,string FriendlyColumn)
        {
            string column = "";
            GridView.SelectAll();
            foreach (var colName in GridView.SelectedColumns)
            {
                string displayCol = GridView.GetDisplayedColumnTitle(colName);
                if (displayCol.IndexOf(FriendlyColumn) >= 0)
                {
                    column = colName;
                    break;
                }

            }
            GridView.ClearSelection();
            GridView.DoubleClick(row, column);
        }

        public static T GetCell<T>(this GuiTableControl Table,int row,int col) where T:class
        {
            return Table.GetCell(row, col) as T;
        }

        /// <summary>
        /// If use property of Entries in GuiComboBox , it will throw an error
        /// </summary>
        /// <param name="Cb"></param>
        /// <returns>return a list of GuiComboBoxEntry</returns>
        public static dynamic GetEntries(this GuiComboBox Cb)
        {
            dynamic comb = Cb;
            return comb.Entries;
        }

        public static void SendKey(this GuiFrameWindow Window,SAPKeys key)
        {
            Window.SendVKey((int)key);
        }

        public static void SendKey(this GuiMainWindow Window,SAPKeys key)
        {
            Window.SendVKey((int)key);
        }

        public static void SendKey(this GuiModalWindow Window,SAPKeys key)
        {
            Window.SendVKey((int)key);
        }

        public static void FindByName(this GuiSimpleContainer Container,int Row,int Column)
        {
            string id = Container.Id;
            //foreach(GuiComponent child in Container.Children)
            //{
            //    string tempId = child.Id.Replace(id, "");
            //    if(tempId)
            //}
        }

        public static GuiContextMenu Select(this GuiContextMenu ContextMenu,string Menu)
        {
            
            if(ContextMenu.Children.Count > 0)
            {
                for(int i = 0;i<ContextMenu.Children.Count;i++)
                {
                    GuiContextMenu subMenu = ContextMenu.Children.ElementAt(i) as GuiContextMenu;
                    if(subMenu.Text.IndexOf(Menu,StringComparison.OrdinalIgnoreCase)>=0)
                    {
                        subMenu.Select();
                        return subMenu;
                    }
                }
            }
            return null;
        }

    }
}

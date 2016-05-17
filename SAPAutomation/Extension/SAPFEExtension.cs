using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPFEWSELib;
using System.IO;

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

        public static void SetBatchValues(this GuiTableControl table, List<string> Values, Action<int> process = null)
        {
            List<List<Tuple<int, string>>> datas = Values.Select(s => new List<Tuple<int, string>>() { new Tuple<int, string>(1, s) }).ToList();
            SAPFEExtension.SetBatchValues(table, datas, process);
        }

        public static void SetBatchValues(this GuiTableControl table,List<List<Tuple<int,string>>> Values,Action<int> process=null) 
        {
            var id = table.Id;
            var pageSize = table.VerticalScrollbar.PageSize;
            int row = 0;
            int range = pageSize - 1;

            for (int i = 0; i < Values.Count; i++)
            {

                if (row != 0 && row % range == 0)
                {
                    table.VerticalScrollbar.Position += pageSize;
                    table = SAPTestHelper.Current.SAPGuiSession.FindById<GuiTableControl>(id);

                    //table = SAPTestHelper.Current.PopupWindow.FindByName<GuiTableControl>("SAPLALDBSINGLE");
                }

                if (i < range)
                {
                    for(int j=0;j<Values[i].Count;j++)
                    {
                        var rowData = Values[i];
                        table.GetCell(row, rowData[j].Item1).Text = rowData[j].Item2;
                    }
                }
                    
                else
                {
                    for (int j = 0; j < Values[i].Count; j++)
                    {
                        var rowData = Values[i];
                        table.GetCell(row % range + 1, rowData[j].Item1).Text = rowData[j].Item2;
                    }
                   
                }


                row++;

                if (process != null)
                    process(i+1);

            }
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

        /// <summary>
        /// Select Tree Node Path Name, split the path name by "->"
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ChooseNode(this GuiTree tree, string path)
        {
            var paths = path.Split(new string[] { "->" }, StringSplitOptions.None);
            var initialLevel = 0;
            var myKey = "";
            foreach (var key in tree.GetAllNodeKeys())
            {
                var level = tree.GetHierarchyLevel(key);
                if (level == initialLevel)
                {
                    var node = tree.GetNodeTextByKey(key);
                    if (node.ToLower().Trim().Contains(paths[initialLevel].ToLower().Trim()))
                    {
                        initialLevel++;
                        if (initialLevel == paths.Count())
                        {
                            myKey = key;
                            break;
                        }
                    }
                }
            }
            if (myKey != "")
            {
                List<string> keyList = new List<string>();
                var parentKey = tree.GetParent(myKey);
                while (parentKey.Trim() != "")
                {
                    keyList.Add(parentKey);
                    parentKey = tree.GetParent(parentKey);
                }
                var count = keyList.Count();
                if (count > 0)
                {
                    for (int i = count - 1; i >= 0; i--)
                    {
                        tree.ExpandNode(keyList[i]);
                    }

                }
                tree.SelectNode(myKey);
            }

            return myKey;

        }

        public static void ExpandAll(this GuiTree tree) {
            var root = tree.TopNode;
            var rootPath = tree.GetNodePathByKey(root);
            expandNode(tree, rootPath);
        }

        private static void expandNode(GuiTree tree, string path) {
            string key = tree.GetNodeKeyByPath(path);
            
            if (tree.IsFolderExpandable(key) && tree.IsFolderExpanded(key) == false) {
                tree.ExpandNode(key);
            }
            var childCount = tree.GetNodeChildrenCount(key);

            if (childCount > 0) {
                var tempPath = path;
                for (int i = 1; i <= childCount; i++) {
                    path += @"\" + i.ToString();
                    expandNode(tree, path);
                    path = tempPath;
                }
            }
        }

    }
}

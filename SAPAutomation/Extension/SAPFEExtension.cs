using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPFEWSELib;
using System.IO;
using SAPAutomation.Framework.SAPException;

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
            if (comp == null)
                throw new GuiNotFoundExpection(message);
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
                    column = colName;
                index++;
            }
            
            return GridView.GetCellValue(row, column);
                
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace SAPAutomation.Data
{
    public static class DataTableExtension
    {
        public static void ExportToExcel(this DataTable dataTable,string File)
        {
            
        }

        public static void ExportToTxt(this DataTable dataTable,string File,char splitChar = '\t')
        {

        }

        public static DataTable ReadFromExcel(this DataTable dataTable,string File,string sheetName)
        {
            return null;
        }

        public static DataTable ReadFromTxt(this DataTable dataTable,string File,char splitChar = '\t')
        {
            return null;
        }
    }
}

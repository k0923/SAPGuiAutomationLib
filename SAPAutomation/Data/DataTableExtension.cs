using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
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
        public static void ExportToExcel(this DataTable dataTable,string File,string sheetName)
        {
            createExcelFile(File, dataTable, sheetName);
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

        private static void createExcelFile(string filePath, DataTable dt, string tableName = "")
        {
            SpreadsheetDocument doc = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook);
            WorkbookPart wbPart = doc.AddWorkbookPart();
            wbPart.Workbook = new Workbook();

            WorksheetPart wsPart = wbPart.AddNewPart<WorksheetPart>();
            wsPart.Worksheet = new Worksheet(new SheetData());

            Sheets sheets = doc.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

            Sheet sheet = new Sheet()
            {
                Id = doc.WorkbookPart.GetIdOfPart(wsPart),
                SheetId = 1,
                Name = tableName == "" ? dt.TableName : tableName
            };

            sheets.Append(sheet);


            SheetData sheetData = wsPart.Worksheet.GetFirstChild<SheetData>();

            Row headRow = new Row();
            headRow.RowIndex = 1;


            for(int i=0;i<dt.Columns.Count;i++)
            {
                Cell c = new Cell();
                c.DataType = new EnumValue<CellValues>(CellValues.String);
                c.CellReference = getColumnName(i + 1) + "1";
                c.CellValue = new CellValue(dt.Columns[i].ColumnName);
                headRow.AppendChild(c);
            }
            sheetData.AppendChild(headRow);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Row r = new Row();
                r.RowIndex = (UInt32)i + 2;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Cell c = new Cell();
                    c.DataType = new EnumValue<CellValues>(getCellType(dt.Columns[j].DataType));
                    c.CellReference = getColumnName(j + 1) + r.RowIndex.ToString();
                    c.CellValue = new CellValue(dt.Rows[i][j].ToString());
                    r.Append(c);
                }
                sheetData.Append(r);
            }

            wbPart.Workbook.Save();
            doc.Close();
        }


        private static string getColumnName(int columnIndex)
        {
            int dividend = columnIndex;
            string columnName = String.Empty;
            int modifier;

            while (dividend > 0)
            {
                modifier = (dividend - 1) % 26;
                columnName =Convert.ToChar(65 + modifier).ToString() + columnName;
                dividend = (int)((dividend - modifier) / 26);
            }

            return columnName;
        }

        private static CellValues getCellType(Type dataType)
        {
            if (dataType == typeof(string))
            {
                return CellValues.String;
            }
            else if(dataType == typeof(DateTime))
            {
                return CellValues.Date;
            }
            else
            {
                return CellValues.Number;
            }
        }

        public static void CreateSpreadsheetWorkbook(string filepath)
        {
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.
                Create(filepath, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Add Sheets to the Workbook.
            Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "mySheet"
            };
            sheets.Append(sheet);

            workbookpart.Workbook.Save();

            // Close the document.
            spreadsheetDocument.Close();
        }
    }
}

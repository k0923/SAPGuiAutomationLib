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
        public static void ExportToExcel(this DataTable dataTable, string File, string sheetName)
        {
            createExcelFile(File, dataTable, sheetName);
        }

        public static void ExportToTxt(this DataTable dataTable, string File, char splitChar = '\t')
        {

        }

        public static void ReadFromExcel(this DataTable dataTable, string File, string sheetName)
        {
            dataTable.Clear();
            dataTable.Columns.Clear();
            dataTable.TableName = sheetName;
            getDataFromExcel(dataTable, File, sheetName);
        }

        public static void ReadFromTxt(this DataTable dataTable, string File, char splitChar = '\t')
        {

        }

        #region EXPORT EXCEL
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


            for (int i = 0; i < dt.Columns.Count; i++)
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
                    if(c.DataType.Value==CellValues.Boolean)
                    {
                        string value = bool.Parse(dt.Rows[i][j].ToString()) ? "1" : "0";
                        c.CellValue = new CellValue(value);
                    }
                    else
                    {
                        c.CellValue = new CellValue(dt.Rows[i][j].ToString());
                    }
                    
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
                columnName = Convert.ToChar(65 + modifier).ToString() + columnName;
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
            else if (dataType == typeof(DateTime))
            {
                return CellValues.Date;
            }
            else if (dataType == typeof(Boolean))
            {
                return CellValues.Boolean;
            }
            else
            {
                return CellValues.Number;
            }
        }

        #endregion

        #region Read Excel
        private static DataTable getDataFromExcel(DataTable dt, string filePath, string sheetName)
        {
            var dataList = getDatas(filePath, sheetName);
            int count = dataList.First().Count;
            foreach (var list in dataList)
            {
                if (list.Count != count)
                    throw new Exception("Can't create table using the exist datas,please check the data in excel");
            }
            return createTable(dt, dataList, sheetName);
        }

        private static DataTable createTable(DataTable dt, List<List<string>> datas, string tableName)
        {

            var header = datas.First();
            foreach (var str in header)
            {
                DataColumn dc = new DataColumn(str);
                dt.Columns.Add(dc);
            }
            for (int i = 1; i < datas.Count; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    dr[j] = datas[i][j];
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private static List<List<string>> getDatas(string filePath, string sheetName)
        {
            List<List<string>> dataList = new List<List<string>>();
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(filePath, false))
            {
                WorkbookPart wbPart = doc.WorkbookPart;
                Sheet sheet = wbPart.Workbook.Descendants<Sheet>().Where(s => s.Name == sheetName).FirstOrDefault();
                if (sheet == null)
                {
                    throw new ArgumentException("sheetName");
                }
                WorksheetPart wsPart = wbPart.GetPartById(sheet.Id) as WorksheetPart;
                foreach (var row in wsPart.Worksheet.Descendants<Row>())
                {
                    List<string> datas = new List<string>();
                    foreach (var cell in row.Descendants<Cell>())
                    {
                        string value = getCellValue(cell, wbPart);
                        datas.Add(value);
                    }
                    dataList.Add(datas);
                }
            }
            return dataList;
        }

        private static string getCellValue(Cell cell, WorkbookPart wb)
        {
            string value = cell.InnerText;
            if (cell.DataType != null)
                switch (cell.DataType.Value)
                {
                    case CellValues.SharedString:
                        var stringTable = wb.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                        if (stringTable != null)
                        {
                            value = stringTable.SharedStringTable.ElementAt(int.Parse(value)).InnerText;
                        }
                        break;
                    case CellValues.Boolean:
                        switch (value)
                        {
                            case "0":
                                value = "FALSE";
                                break;
                            default:
                                value = "TRUE";
                                break;
                        }
                        break;
                }
            return value;
        }
        #endregion
    }
}

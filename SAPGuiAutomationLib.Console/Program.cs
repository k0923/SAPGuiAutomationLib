using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using System.Reflection.Emit;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Threading;
using System.Data;



using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data.SqlClient;
using SAPFEWSELib;
using SAPAutomation;
using SAPGUIComboBoxLib;
using SAPAutomation.Framework;
//using Young.DAL;

namespace SAPGuiAutomationLib.Con
{
    
    class Program
    {
        public static bool test(out int a)
        {
            a = 10;
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                a = 11;
            }
        }

        static void Main(string[] args)
        {
            SAPTestHelper.Current.SetSession();
            var fields = SAPTestHelper.Current.SAPGuiSession.FindDescendantsByProperty<GuiCTextField>(c => true);
            Console.WriteLine(fields.Count());
            //GetCurrency gc = new GetCurrency();
            //Type t = gc.GetType();
            //MethodInfo mi = t.GetMethod("GetCurrencyRate");
            //object obj = mi.Invoke(gc, null);
            Global.DataSet = Young.Data.ExcelHelper.Current.Open(@"E:\test.xlsx").ReadAll();
            Global.CurrentId = 1;

            //SAPTestHelper.Current.SetSession();
            //SAPTestHelper.Current.TurnScreenLog(ScreenLogLevel.All);
            GetCurrency gc = new GetCurrency();
            gc.DataBinding();

            SC_230 sc230 = new SC_230();
            sc230.StartTransaction("SE16");
            sc230.TableName = "TCURR";
            sc230.SendKeys(SAPKeys.Enter);

            SC_1000 sc1000 = new SC_1000();
            sc1000.RateType = "M";
            sc1000.FromCurrency = "USD";
            sc1000.ToCurrency = "USD";
            sc1000.Execute();
            var datas = SAPTestHelper.Current.ScreenDatas;

            SAPTestHelper.Current.End();

            
            //SAPTestHelper.Current.SetSession();
            //SAPTestHelper.Current.OnRequestBlock += Current_OnRequestBlock;
            //SAPTestHelper.Current.TurnScreenLog(ScreenLogLevel.All);
            //SAPTestHelper.Current.SAPGuiSession.StartTransaction("se16");
            //SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("DATABROWSE-TABLENAME").Text = "TCURR";
            //SAPTestHelper.Current.MainWindow.SendKey(SAPKeys.Enter);
            //SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("I1-LOW").Text = "M";
            
            //Run("USD", "USD");
            

            //int count = SAPTestHelper.count;
            //int sc = SAPTestHelper.sCount;

            //SAPTestHelper.Current.End();
            //System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(List<ScreenData>));
            //using (FileStream fs = new FileStream("1.xml",FileMode.Create))
            //{
            //    xs.Serialize(fs, SAPTestHelper.Current.ScreenDatas);
            //}


        }

        static void Current_OnRequestBlock()
        {
            Run("USD", "CNY");
        }

        static void Run(string curFrom,string curTo)
        {
            SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("I2-LOW").Text = curFrom;
            SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("I3-LOW").Text = curTo;
            SAPTestHelper.Current.MainWindow.SendKey(SAPKeys.F8);
        }

        static void Run1()
        {
            SAPTestHelper.Current.SAPGuiSession.EndTransaction();
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

            foreach (DataColumn dc in dt.Columns)
            {
                Cell c = new Cell();
                c.DataType = new EnumValue<CellValues>(CellValues.SharedString);
                c.CellValue = new CellValue(dc.ColumnName);
                headRow.Append(c);
            }
            sheetData.Append(headRow);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Row r = new Row();
                r.RowIndex = uint.Parse(i.ToString()) + 2;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Cell c = new Cell();
                    c.DataType = new EnumValue<CellValues>(getCellType(dt.Columns[j].DataType));
                    c.CellValue = new CellValue(dt.Rows[i][j].ToString());
                    r.Append(c);
                }
                sheetData.Append(r);
            }

            wbPart.Workbook.Save();
            doc.Close();
        }

        private static CellValues getCellType(Type dataType)
        {
            if (dataType == typeof(string))
            {
                return CellValues.SharedString;
            }
            else if (dataType == typeof(DateTime))
            {
                return CellValues.Date;
            }
            else
            {
                return CellValues.Number;
            }
        }

        public static string GetCellValue(string fileName,string sheetName,string addressName)
        {
            string value = null;

            // Open the spreadsheet document for read-only access.
            using (SpreadsheetDocument document =
                SpreadsheetDocument.Open(fileName, false))
            {
                // Retrieve a reference to the workbook part.
                WorkbookPart wbPart = document.WorkbookPart;

                // Find the sheet with the supplied name, and then use that 
                // Sheet object to retrieve a reference to the first worksheet.
                Sheet theSheet = wbPart.Workbook.Descendants<Sheet>().
                  Where(s => s.Name == sheetName).FirstOrDefault();

                // Throw an exception if there is no sheet.
                if (theSheet == null)
                {
                    throw new ArgumentException("sheetName");
                }

                // Retrieve a reference to the worksheet part.
                WorksheetPart wsPart =
                    (WorksheetPart)(wbPart.GetPartById(theSheet.Id));

                // Use its Worksheet property to get a reference to the cell 
                // whose address matches the address you supplied.
                Cell theCell = wsPart.Worksheet.Descendants<Cell>().
                  Where(c => c.CellReference == addressName).FirstOrDefault();

                // If the cell does not exist, return an empty string.
                if (theCell != null)
                {
                    value = theCell.InnerText;

                    // If the cell represents an integer number, you are done. 
                    // For dates, this code returns the serialized value that 
                    // represents the date. The code handles strings and 
                    // Booleans individually. For shared strings, the code 
                    // looks up the corresponding value in the shared string 
                    // table. For Booleans, the code converts the value into 
                    // the words TRUE or FALSE.
                    if (theCell.DataType != null)
                    {
                        switch (theCell.DataType.Value)
                        {
                            case CellValues.SharedString:

                                // For shared strings, look up the value in the
                                // shared strings table.
                                var stringTable =
                                    wbPart.GetPartsOfType<SharedStringTablePart>()
                                    .FirstOrDefault();

                                // If the shared string table is missing, something 
                                // is wrong. Return the index that is in
                                // the cell. Otherwise, look up the correct text in 
                                // the table.
                                if (stringTable != null)
                                {
                                    value =
                                        stringTable.SharedStringTable
                                        .ElementAt(int.Parse(value)).InnerText;
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
                    }
                }
            }
            return value;
        }

        private string getReference(int row,int col)
        {
            return "A";
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


            SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
            for (uint i = 1; i < 100;i++ )
            {
                Row r = new Row() { RowIndex = i };
                for(uint j = 1;j<10;j++)
                {
                    Cell c = new Cell();
                    CellFormat format = new CellFormat();
                    
                    format.FillId = 2;
                    c.DataType = new EnumValue<CellValues>(CellValues.SharedString);
                    
                    c.CellValue = new CellValue(string.Format("r:{0},c:{1}", i, j));
                    c.Append(format);
                    r.Append(c);
                }
                sheetData.Append(r);
            }

            workbookpart.Workbook.Save();

            // Close the document.
            spreadsheetDocument.Close();
        }

        static string GetCode<T>(T item, Func<CodeDomProvider, Action<T, TextWriter, CodeGeneratorOptions>> action)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("c#");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "c";
            StringBuilder sb = new StringBuilder();

            using (TextWriter sourceWriter = new StringWriter(sb))
            {
                action(provider)(item, sourceWriter, options);
            }
            return sb.ToString();
        }

       


       


        static void ShowProp(string typeName,object obj)
        {
            
            
        }

        static void DataClassTest()
        {
            //List<SAPDataParameter> ps = new List<SAPDataParameter>();
            //ps.Add(new SAPDataParameter() { Name = "Type", Type = typeof(int), Comment = "Type of the currency." });
            //ps.Add(new SAPDataParameter() { Name = "CurrencyFrom", Type = typeof(string), Comment = "From Currency." });
            //ps.Add(new SAPDataParameter() { Name = "CurrencyTo", Type = typeof(string), Comment = "To Currency." });
            //ps.Add(new SAPDataParameter() { Name = "ValidDate", Type = typeof(string), Comment = "Valid Date." });

            //SAPModuleAttribute attribute = new SAPModuleAttribute();
            //attribute.ModuleName = "Get Currency";
            //attribute.ModuleVersion = "1.0.0.0";
            //attribute.ScreenNumber = "1000";
            //attribute.TCode = "SE16";
            //attribute.Author = "Zhou Yang";
            //attribute.Email = "yang.zhou4@hp.com";
           

            //var tp = SAPAutomationExtension.GetDataClass("Screen_GetCurrency", ps,attribute);
            //string code = CodeHelper.GetCode<CodeTypeMember>(tp, p => p.GenerateCodeFromMember).ToString();
           
          

        }

        static void DynamicEmit()
        {
            //ModuleBuilder builder = new ModuleBuilder();
            //TypeBuilder tb = builder.DefineType("Student", TypeAttributes.Class);
            //PropertyBuilder pb = tb.DefineProperty("Name", PropertyAttributes.None, typeof(string), null);
            
        }
    }


    public class CompareObj:IEquatable<CompareObj>
    {
        public string DataA { get; set; }

        public int DataB { get; set; }

        public bool Equals(CompareObj other)
        {
            if (DataA == other.DataA && DataB == other.DataB)
                return true;
            return false;
        }

        public void test()
        {

        }
    }

    public class RefTest
    {
        private bool _isSet;
        public RefTest(ref bool IsSet)
        {
            _isSet = IsSet;
        }

        public void Set()
        {
            _isSet = true;
        }
    }

    public class Screen_101:SAPGuiScreen
    {

    }
    class CodeHelper
    {
        private static CodeDomProvider provider = CodeDomProvider.CreateProvider("c#");
        public static StringBuilder GetCode<T>(T item, Func<CodeDomProvider, Action<T, TextWriter, CodeGeneratorOptions>> action)
        {
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            StringBuilder sb = new StringBuilder();

            using (TextWriter sourceWriter = new StringWriter(sb))
            {
                action(provider)(item, sourceWriter, options);
            }
            return sb;
        }

        public static bool IsValidVariable(string VariableName)
        {
            return provider.IsValidIdentifier(VariableName);
        }

    }
}

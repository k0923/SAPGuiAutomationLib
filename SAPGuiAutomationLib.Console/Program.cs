using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPFEWSELib;
using System.Reflection;
using System.Reflection.Emit;
using SAPTestRunTime;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Threading;
using System.Data;
//using Young.DAL;

namespace SAPGuiAutomationLib.Con
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isSet = false;
            RefTest refT = new RefTest(ref isSet);
            refT.Set();
            Console.WriteLine(isSet);

            DataClassTest();
            DataTable dt = new DataTable();
            dt.Columns.Add("A");
            dt.Columns.Add("B");
            dt.Columns.Add("C");
            dt.Columns.Add("D");

            var colString = dt.Columns.OfType<DataColumn>().Count();

            string testVar = "";
            Console.WriteLine(CodeHelper.IsValidVariable(testVar));

            DataClassTest();

            SAPTestHelper.Current.SetSession();
            SAPTestHelper.Current.SAPGuiSession.ActiveWindow.Restore();


            //SAPLogon logon = new SAPLogon();
            //logon.StartProcess();
            //logon.OpenConnection("g1u3171c.austin.hp.com");
            //logon.Login("21688419", "2wsx#edc", "100", "EN");

            //SAPTestHelper.Current.SetSession(logon);

            //Thread.Sleep(15000);
            //SAPTestHelper.Current.SAPGuiSession.ActiveWindow.CompBitmap
            SAPTestHelper.Current.TakeScreenShot(@"C:\screenshot\1.jpg");
            
            //IDBProvider prodiver = new ORACLEDB("User Id=ASIAPACIFIC_LILZHANG;Password=newpassword_2015;Data Source=gvu1266.atlanta.hp.com:1525/TKRKI;");

            //DataAccess da = new DataAccess(prodiver);


            //string sql = "select procd.invoice_id, procd.invoice_date, procd.buyer_entity_code,procd.seller_entity_code, procd.order_number, procs.status, procs.status_date,procs.note from PROC_LBOPC_A_DETAIL procd, PROC_LBOPC_A_DETAIL_STATUS procs where procd.PROC_DETAIL_ID = procs.FK_PROC_A_DETAIL_ID and procd.invoice_id in ('H4H2157512')";

            //System.Data.DataSet ds = da.GetData(sql, System.Data.CommandType.Text, null);


           


            //SAPTestHelper.Current.SetSession();
            //GuiComponent c = SAPTestHelper.Current.SAPGuiSession.ActiveWindow.FindByName("us","GuiUserArea");

            //GuiComponent comp = SAPTestHelper.Current.GetElementById("/app/con[0]/ses[0]/wnd[0]/usr");



            //SapCompInfo ci = new SapCompInfo();
            //ci.Id = comp.Id;
       
           
            
            
       
            
        }
        //static void DisplayCode(CodeExpression Expression)
        //{
        //    CodeDomProvider provider = CodeDomProvider.CreateProvider("c#");
        //    CodeGeneratorOptions options = new CodeGeneratorOptions();
        //    options.BracingStyle = "c";
        //    StringBuilder sb = new StringBuilder();
            
        //    using (TextWriter sourceWriter = new StringWriter(sb))
        //    {
        //        provider.GenerateCodeFromExpression(Expression, sourceWriter, options);
                
        //    }
        //    Console.WriteLine(sb.ToString());
        //}

        

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

       


        static void SAPGuiSession_Hit(GuiSession Session, GuiComponent Component, string InnerObject)
        {
            
        }


        static void ShowProp(string typeName,object obj)
        {
            
            
        }

        static void DataClassTest()
        {
            List<SAPDataParameter> ps = new List<SAPDataParameter>();
            ps.Add(new SAPDataParameter() { Name = "Type", Type = typeof(int), Comment = "Type of the currency." });
            ps.Add(new SAPDataParameter() { Name = "CurrencyFrom", Type = typeof(string), Comment = "From Currency." });
            ps.Add(new SAPDataParameter() { Name = "CurrencyTo", Type = typeof(string), Comment = "To Currency." });
            ps.Add(new SAPDataParameter() { Name = "ValidDate", Type = typeof(string), Comment = "Valid Date." });

            SAPModuleAttribute attribute = new SAPModuleAttribute();
            attribute.ModuleName = "Get Currency";
            attribute.ModuleVersion = "1.0.0.0";
            attribute.ScreenNumber = "1000";
            attribute.TCode = "SE16";
            attribute.Author = "Zhou Yang";
            attribute.Email = "yang.zhou4@hp.com";
           

            var tp = SAPAutomationExtension.GetDataClass("Screen_GetCurrency", ps,attribute);
            string code = CodeHelper.GetCode<CodeTypeMember>(tp, p => p.GenerateCodeFromMember).ToString();
           
            

        }

        static void DynamicEmit()
        {
            //ModuleBuilder builder = new ModuleBuilder();
            //TypeBuilder tb = builder.DefineType("Student", TypeAttributes.Class);
            //PropertyBuilder pb = tb.DefineProperty("Name", PropertyAttributes.None, typeof(string), null);
            
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

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
//using Young.DAL;

namespace SAPGuiAutomationLib.Con
{
    class Program
    {
        static void Main(string[] args)
        {
            //IDBProvider prodiver = new ORACLEDB("User Id=ASIAPACIFIC_LILZHANG;Password=newpassword_2015;Data Source=gvu1266.atlanta.hp.com:1525/TKRKI;");

            //DataAccess da = new DataAccess(prodiver);


            //string sql = "select procd.invoice_id, procd.invoice_date, procd.buyer_entity_code,procd.seller_entity_code, procd.order_number, procs.status, procs.status_date,procs.note from PROC_LBOPC_A_DETAIL procd, PROC_LBOPC_A_DETAIL_STATUS procs where procd.PROC_DETAIL_ID = procs.FK_PROC_A_DETAIL_ID and procd.invoice_id in ('H4H2157512')";

            //System.Data.DataSet ds = da.GetData(sql, System.Data.CommandType.Text, null);





            SAPTestHelper.Current.SetSession();
            GuiComponent c = SAPTestHelper.Current.SAPGuiSession.ActiveWindow.FindByName("us","GuiUserArea");

            GuiComponent comp = SAPTestHelper.Current.GetElementById("/app/con[0]/ses[0]/wnd[0]/usr");



            SapCompInfo ci = new SapCompInfo();
            ci.Id = comp.Id;
       
           
            
            
            
            
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").FindByName<GuiUserArea>("usr").FindByName<GuiCTextField>("DATABROWSE-TABLENAME").Text = "abc";

           
            ci.Type = comp.Type;
            ci.Name = comp.Name;

            DisplayCode(ci.FindMethod);
            Console.ReadLine();
            //SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").FindByName<GuiUserArea>("usr");






            //SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").FindByName<GuiUserArea>("usr")
           
            
            
            
            
            
            
            
            
            var shell = SAPTestHelper.Current.GetElementById<GuiContainerShell>("wnd[0]/usr/cntlIMAGE_CONTAINER/shellcont");
            var test = shell.FindByName("shell", "GuiSplitShell");
            //var area = SAPTestHelper.Current.SAPGuiSession.FindById("wnd[0]").FindByName<GuiUserArea>("usr");
           // Console.WriteLine(area.SubType);
            GuiTree tree = SAPTestHelper.Current.GetElementById<GuiTree>("wnd[0]/usr/cntlIMAGE_CONTAINER/shellcont/shell/shellcont[0]/shell");
            var area1 = SAPTestHelper.Current.GetElementById<GuiUserArea>("wnd[0]/usr").FindByName("shell", "GuiTree");
            
            
        }
        static void DisplayCode(CodeExpression Expression)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("c#");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "c";
            StringBuilder sb = new StringBuilder();
            
            using (TextWriter sourceWriter = new StringWriter(sb))
            {
                provider.GenerateCodeFromExpression(Expression, sourceWriter, options);
                
            }
            Console.WriteLine(sb.ToString());
        }
        static void SAPGuiSession_Hit(GuiSession Session, GuiComponent Component, string InnerObject)
        {
            
        }


        static void ShowProp(string typeName,object obj)
        {
            
            
        }
    }
}

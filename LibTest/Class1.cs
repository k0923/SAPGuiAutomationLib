using System;
using System.Collections.Generic;
using System.Text;
using SAPTestRunTime;
using System.Data.SqlClient;
using System.Data;
using SAPAutomation.Framework;
using SAPAutomation.Framework.Attributes;
using System.Diagnostics;
using Young.Data.Attributes;

namespace LibTest
{
    public class Test
    {
        public Test() { }

        public void ShowMessage(string message)
        {
            System.Windows.Forms.MessageBox.Show(message);
        }

        static void Main()
        {
            Process.Start(@"C:\Users\Young\Documents\GitHub\SAPGuiAutomationLib\SAPGuiAutomationLib.Console\bin\u26\SAPGuiAutomationLib.Console.exe");
            Process.Start(@"C:\Users\Young\Documents\GitHub\SAPGuiAutomationLib\SAPGuiAutomationLib.Console\bin\Debug\SAPGuiAutomationLib.Console.exe");
            //DelegateTest();

            //SqlConnectionStringBuilder ssb = new SqlConnectionStringBuilder();
            
            //ssb.DataSource = "yanzhou17.asiapacific.hpqcorp.net";
            //ssb.IntegratedSecurity = true;
            //ssb.InitialCatalog = "SAPTestCenter";
            //SqlConnection sqlCn = new SqlConnection(ssb.ConnectionString);
            //Young.Data.DBConnection.DBAccess da = new Young.Data.DBConnection.DBAccess(sqlCn, new SqlCommand());
            
            //DataSet ds = da.GetData(new SqlDataAdapter(), "select * from Accounts", CommandType.Text);
            //Global.DataSet = ds;
            //ds.Tables[0].TableName = "Accounts";
            //Console.WriteLine(Global.DataSet.Tables[0].TableName);
            //Global.CurrentId = 62;
            //Screen_102 sc102 = new Screen_102();
            //foreach(var p in sc102.Rows)
            //{
            //    Console.WriteLine(p.UserName + ":" + p.Password);
            //}
            //Console.ReadLine();
        }

        static void SAPGuiSession_StartRequest(SAPFEWSELib.GuiSession Session)
        {
            Console.WriteLine("Hi");
        }

        static void DelegateTest()
        {
            
        }
    }

    
    public class Screen_101:DataInitial
    {
        public Screen_101()
        {

        }
        [ColumnBinding("UserName")]
        public string UserName { get; set; }
    }

    
    public class Screen_102:Screen_101
    {
        
        public object Test(DataRow[] drs)
        {
            List<Profile> profiles = new List<Profile>();
            foreach(var dr in drs)
            {
                var profile = new Profile() {UserName = dr["UserName"].ToString(),Password = dr["Password"].ToString() };
                profiles.Add(profile);
            }
            return profiles;
        }

       
        public List<Profile> Rows { get; set; }
    }

    public class Profile
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    
}

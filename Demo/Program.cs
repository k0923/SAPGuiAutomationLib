using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPAutomation;
using SAPAutomation.Data;
using System.Data;
using System.Threading;
using SAPFEWSELib;
using SAPAutomation.Extension;

namespace Demo
{
    class Program
    {
        
        static void Main(string[] args)
        {
            SAPTestHelper.Current.SetSession();
            //GuiTableControl Table = SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").FindByName<GuiTableControl>("SAPL0SAPTCTRL_V_TCURR");
            //GuiScrollbar verbar = Table.VerticalScrollbar;
            
            for(int i = 1;i<=10;i++)
            {
                GuiTableControl Table = SAPTestHelper.Current.SAPGuiSession.FindById<GuiMainWindow>("wnd[0]").FindByName<GuiTableControl>("SAPL0SAPTCTRL_V_TCURR");
                GuiScrollbar verbar = Table.VerticalScrollbar;
                verbar.Position = i*1000;
            }
        }

        

        private static void Demo1()
        {
            //DataTable<GetCurrency> testData = new DataTable<GetCurrency>();
            //testData.GetCopy().ExportToExcel("2.xlsx", "Currency");

            //Thread.Sleep(10000);
            DataTable dt = new DataTable();
            dt.ReadFromExcel("2.xlsx", "Currency");

            DataTable<GetCurrency> data = new DataTable<GetCurrency>(dt);

            //SAPAutoLogon.Logon l = new SAPAutoLogon.Logon();
            //l.StartLogon("LH4_HPI");
            SAPTestHelper.Current.SetSession();

            foreach (GetCurrency curData in data)
            {
                GetCurrency.RunAction(curData);
            }

            //SAPTestHelper.Current.TakeScreenShot(@"\\yanzhou17.asiapacific.hpqcorp.net\Shared Folder\1.jpg");
            //GetCurrency get = new GetCurrency();
            //get.CurFrom = "USD";
            //get.CurTo = "CNY";
            //GetCurrency.RunAction(get);
        }
    }
}

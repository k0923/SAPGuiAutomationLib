using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Threading;
using SAPFEWSELib;
using SAPAutomation;
using SAPAutomation.Framework;

namespace Demo
{
    class Program
    {
        
        static void Main(string[] args)
        {

            Global.DataSet = Young.Data.ExcelHelper.Current.Open(@"C:\test.xlsx").ReadAll();
            Global.CurrentId = 1;

            SAPTestHelper.Current.SetSession();
            SC_102 sc = new SC_102();
            sc.StartTransaction("VA02");

            sc.DataBinding();
            sc.Sales();

            SC_4001_Sales scSales = new SC_4001_Sales();
            scSales.DataBinding();
            

            //SAPTestHelper.Current.SetSession();

            //SC_102 sc = new SC_102();
            //sc.StartTransaction("VA02");
            //sc.DataBinding();

            

            
           
        }

        

     
    }
}

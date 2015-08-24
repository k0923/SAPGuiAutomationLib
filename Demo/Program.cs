using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Threading;
using SAPFEWSELib;
using SAPAutomation;
using SAPAutomation.Framework;
using Young.Data;

namespace Demo
{
    class Program
    {
        
        static void Main(string[] args)
        {
            SAPTestHelper.Current.SetSession();
            //SAPTestHelper.Current.TurnScreenLog(ScreenLogLevel.All);
            DataDriven.IsSampleMode = true;
            //SC_101 sc = new SC_101();
            //sc.DataBinding();
            //SAPTestHelper.Current.SaveLog("1.xml");
            

            SC_4002 s = new SC_4002();
            s.DataBinding();

            //SAPTestHelper.Current.SetSession();

            //SC_102 sc = new SC_102();
            //sc.StartTransaction("VA02");
            //sc.DataBinding();

            

            
           
        }

        

     
    }
}

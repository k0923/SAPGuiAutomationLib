using SAPAutomation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPGuiDemo
{
    public partial class Demo
    {
        public static void StartSAP()
        {
            SAPLogon l = new SAPLogon();
            l.StartProcess();
            l.OpenConnection("saplh4.sapnet.hp.com");
            l.Login("21688419", "3edc$rfv", "100", "EN");
        }

        public static void SetSession(SAPLogon logon)
        {
            
            SAPTestHelper.Current.SetSession();

            
            //SAPTestHelper.Current.SetSession("LH4");

            
            //SAPTestHelper.Current.SetSession(logon);
        }

       
    }
}

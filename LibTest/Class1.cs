using System;
using System.Collections.Generic;
using System.Text;
using SAPTestRunTime;

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
            SAPTestHelper.Current.SetSession();
            SAPTestHelper.Current.SAPGuiSession.StartRequest += SAPGuiSession_StartRequest;
            Console.ReadLine();
        }

        static void SAPGuiSession_StartRequest(SAPFEWSELib.GuiSession Session)
        {
            Console.WriteLine("Hi");
        }
    }
}

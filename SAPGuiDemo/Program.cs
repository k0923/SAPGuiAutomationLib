using SAPAutomation;
using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPGuiDemo.DataEngineDemo;

namespace SAPGuiDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //SAPTestHelper.Current.SetSession();
            //SAPTestHelper.Current.MainWindow.FindDescendantByProperty<GuiOkCodeField>().Text = "SE16";
            //DataDemo.GetStudent();
            //SAPTestHelper.Current.SetSession();
            
            DataDemo.Recursion();
            Console.ReadLine();
        }


       
        

    }
}

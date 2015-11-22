using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.Data;

namespace SAPGuiDemo.DataEngineDemo
{
    public class DataDemo
    {
       
        public static DataEngine GetDataEngine()
        {
            var ds = ExcelHelper.Current.Open("Demo.xlsx").ReadAll();
            ExcelHelper.Current.Close();

            DataEngine de = new DataEngine();
            de.SetData(ds);
            return de;
        }

        public static void GetPeople()
        {
            var dataEngine = DataDemo.GetDataEngine();
            dataEngine.CurrentId = 1;
            var p = dataEngine.Create<People>();

            Console.WriteLine($"{p.Name}:{p.Age}");
            
        }

        public static void GetStudent()
        {
            var dataEngine = DataDemo.GetDataEngine();
            dataEngine.CurrentId = 2;
            var p = dataEngine.Create<Student>();
            
            Console.WriteLine($"{p.Name}:{p.Age} has scores below\nMath:{p.Math}\nEnglish:{p.English}\nHistory:{p.History}\nTotal:{p.TotalScole()}");
            ///--------------------------------------------------------------------------------------
            ExcelHelper.Current.Create("Demo1.xlsx").WriteAll(dataEngine.Data);
            ExcelHelper.Current.Close();
        }

        public static void UsingSampleData()
        {
            var dataEngine = new DataEngine();
            dataEngine.IsUsingSampleData = true;
            var p = dataEngine.Create<Student>();
           
            Console.WriteLine($"{p.Name}:{p.Age} has scores below\nMath:{p.Math}\nEnglish:{p.English}\nHistory:{p.History}\nTotal:{p.TotalScole()}");
            
            
        }


    }
}

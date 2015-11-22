using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SAPGuiDemo.DataEngineDemo
{
    class LegacyData
    {
        public  void GetData()
        {
            DataTable dt = LoadData();

            string name = dt.Rows[0]["Name"].ToString();
            int age = int.Parse(dt.Rows[0]["Age"].ToString()); 
        }

        public  DataTable LoadData()
        {
            return new DataTable();
        }
    }
}

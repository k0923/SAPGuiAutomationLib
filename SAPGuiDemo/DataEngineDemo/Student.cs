using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.Data.Attributes;
using Young.Data;

namespace SAPGuiDemo.DataEngineDemo
{
    [DataBinding("Students", DataMode =DataType.FromPrivateTable)]
    public class Student:People
    {
        [ColumnBinding]
        [SingleSampleData(102)]
        public int Sid { get; set; }

        [ColumnBinding]
        [SingleSampleData(100)]
        public int English { get; set; }

        [ColumnBinding]
        [SingleSampleData(80)]
        public int Math { get; set; }

        [ColumnBinding]
        [SingleSampleData(55)]
        public int History { get; set; }

        // The function should be execute after set the score data so the order = 1
        [ColumnBinding("Total",Directory = DataDirectory.Output,Order =1)]
        public int TotalScole() => English + Math + History;

       
    }
}

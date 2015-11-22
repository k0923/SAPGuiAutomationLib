using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.Data.Attributes;

namespace SAPGuiDemo.DataEngineDemo
{
    [DataBinding]
    public class People
    {
        [ColumnBinding]
        [SingleSampleData("Olivar")]
        public string Name { get; set; }

        
        [ColumnBinding]
        [SingleSampleData(18)]
        public int Age { get; set; }
    }
}

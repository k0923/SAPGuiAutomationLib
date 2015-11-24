using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.Data.Attributes;

namespace SAPGuiDemo.DataEngineDemo
{
    [DataBinding]
    public class Laptop
    {
        [ColumnBinding]
        public string Model { get; set; }

        [ColumnBinding]
        public string Brand { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPGuiAutomationLib
{
    public class SAPDataParameter
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Comment { get; set; }
        public Type Type { get; set; }

        public object Value { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPAutomation.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnBindingAttribute : OrderAttribute
    {
        public string ColName { get; set; }

        public ColumnBindingAttribute(string ColName)
        {
            this.ColName = ColName;
            this.Directory = DataDirectory.Input;
            this.Cycle = CycleType.Default;
        }

        public DataDirectory Directory { get; set; }

    }
}

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

        public ColumnBindingAttribute()
        {
            this.Directory = DataDirectory.Input;
        }

        public ColumnBindingAttribute(string ColName):this()
        {
            this.ColName = ColName;
        }

        public DataDirectory Directory { get; set; }

    }
}

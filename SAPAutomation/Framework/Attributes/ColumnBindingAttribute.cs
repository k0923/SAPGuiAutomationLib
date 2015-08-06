using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPAutomation.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnBindingAttribute : Attribute
    {
        public string ColName { get; set; }

        public ColumnBindingAttribute(string ColName)
        {
            this.ColName = ColName;
        }
    }
}

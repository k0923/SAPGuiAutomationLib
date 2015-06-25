using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPAutomation.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple=false)]
    public class TestDataAttribute:Attribute
    {
        public string FriendlyName { get; set; }
    }
}

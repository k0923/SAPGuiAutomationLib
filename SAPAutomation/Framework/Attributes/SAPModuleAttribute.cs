using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPAutomation.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SAPModuleAttribute : Attribute
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public string Designer { get; set; }

        public string TCode { get; set; }

        public string Box { get; set; }
    }
}

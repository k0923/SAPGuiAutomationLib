using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPTestRunTime
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple=true)]
    public class SAPModuleAttribute:Attribute
    {
        public string ModuleName { get; set; }

        public string ModuleVersion { get; set; }

        public string Author { get; set; }

        public string Email { get; set; }

        public string ScreenNumber { get; set; }

        public string TCode { get; set; }
    }
}

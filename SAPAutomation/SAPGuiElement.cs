using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SAPAutomation
{
    public class SAPGuiElement
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public BindingFlags Action { get; set; }

        public string ActionName { get; set; }

        public object[] ActionValues { get; set; }
    }
}

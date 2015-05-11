using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;

namespace SAPGuiAutomationLib
{
    public class SapCompInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public CodeExpression FindMethod { get; set; }
    }



  
}

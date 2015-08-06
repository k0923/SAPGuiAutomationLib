using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPAutomation.Framework.SAPException
{
    public class GuiNotFoundExpection:Exception
    {
        public GuiNotFoundExpection(string Msg) : base(Msg) 
        { 
           
        }
    }
}

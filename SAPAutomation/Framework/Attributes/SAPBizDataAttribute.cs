using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPAutomation.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple=false)]
    public class SAPBizDataAttribute:Attribute
    {
    }
}

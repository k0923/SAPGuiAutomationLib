using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPGuiAutomationLib.Console
{

    [My(Name="Zhou Yang")]
    public class TestClass:BaseClass
    {
    }

    public class BaseClass
    {
        public BaseClass()
        {
             var myAttributes = this.GetType().GetCustomAttributes(typeof(MyAttribute),true);
             var myAt = myAttributes.First() as MyAttribute;
             System.Console.WriteLine(myAt.Name);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Module,AllowMultiple=true)]
    public class MyAttribute:Attribute
    {
        public string Name { get; set; }
    }
}

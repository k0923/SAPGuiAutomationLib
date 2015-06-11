using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPTestRunTime.Framework
{
    interface ISAPModule
    {
        SAPModuleAttribute ModuleInfo();
    }
    

    public class SAPModule:ISAPModule
    {
        private SAPModuleAttribute _attribute = null;
        public SAPModule()
        {
            var attribute = this.GetType().GetCustomAttributes(typeof(SAPModuleAttribute), true).FirstOrDefault();
            if (attribute != null)
                _attribute = attribute as SAPModuleAttribute;
        }

        public SAPModuleAttribute ModuleInfo()
        {
            return _attribute;
        }
    }

    [AttributeUsage(AttributeTargets.Class,AllowMultiple=false)]
    public class SAPModuleAttribute:Attribute
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public string Designer { get; set; }

        public string TCode { get; set; }
    }

    [AttributeUsage(AttributeTargets.Assembly,AllowMultiple=false)]
    public class BoxAttribute:Attribute
    {
        private string _name;
        public BoxAttribute(string Name)
        {
            _name = Name;
        }
        public string Name { get { return _name; } }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SAPModuleInfoAttribute:Attribute
    {
        public Type ModuleType { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,AllowMultiple=true)]
    public class SAPScreenAttribute:Attribute
    {
        public string ScreenNumber { get; set; }

        public string Program { get; set; }
    }
}

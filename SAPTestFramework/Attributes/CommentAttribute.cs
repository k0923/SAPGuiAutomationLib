using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPAutomation.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
    public class CommentAttribute:Attribute
    {
        public string Summary { get; set; }

        public CommentAttribute() { }
        
        public CommentAttribute(string Description)
        {
            this.Summary = Description;
        }

        
    }
}

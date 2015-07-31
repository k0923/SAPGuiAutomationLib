using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPAutomation.Framework.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class CommentAttribute:Attribute
    {
        public string Summary { get; set; }

        public CommentAttribute() { }
        
        public CommentAttribute(string Description,int a)
        {
            this.Summary = Description;
        }

        
    }
}

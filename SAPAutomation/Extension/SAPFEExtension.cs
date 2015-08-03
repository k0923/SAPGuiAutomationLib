using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPFEWSELib;
using System.IO;

namespace SAPAutomation.Extension
{
    public static partial class SAPFEExtension
    {
        public static void HighLight(this GuiComponent comp)
        {
            GuiVComponent vComp = comp as GuiVComponent;
            if (vComp != null)
                vComp.Visualize(true);
        }


        public static T Wrap<T>(this GuiComponent Component) where T:class
        {
            return Component as T;
        }
        


    }
}

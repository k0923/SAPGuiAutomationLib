using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPFEWSELib;

namespace SAPTestRunTime
{
    public static class SAPFEExtension
    {
        public static T GetComponentById<T>(this GuiSession session,string id) where T:class
        {
            T element = session.FindById(id) as T;
            return element;
        }

        

        public static void HighLight(this GuiComponent comp)
        {
            GuiVComponent vComp = comp as GuiVComponent;
            if (vComp != null)
                vComp.Visualize(true);
        }

        public static T TryGetComponentById<T>(this GuiSession session,string id) where T:class
        {
            try
            {
                return GetComponentById<T>(session, id);
            }
            catch
            {
                return null;
            }
        }

        
    }
}

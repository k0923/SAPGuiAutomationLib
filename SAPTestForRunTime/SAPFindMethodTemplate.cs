using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPTestRunTime
{
    public static partial class SAPFEExtension
    {
        public static IEnumerable<T> findAllByNameTemplate<T>(string Name, Func<string, string, GuiComponentCollection> FindAllByName)
            where T : class
        {
            try
            {
                var Components = FindAllByName(Name, typeof(T).Name);
                if (Components != null)
                {
                    List<T> comps = new List<T>();
                    for (int i = 0; i < Components.Count; i++)
                    {
                        T comp = Components.ElementAt(i) as T;
                        comps.Add(comp);
                    }
                    return comps;
                }

                return null;
            }
            catch
            {
                return null;
            }
            

        }

        public static IEnumerable<T> findAllByNameExTemplate<T>(string Name,int TypeId, Func<string, int, GuiComponentCollection> FindAllByNameEx) where T:class
        {
            try
            {
                var Components = FindAllByNameEx(Name, TypeId);
                if (Components != null)
                {
                    List<T> comps = new List<T>();
                    for (int i = 0; i < Components.Count; i++)
                    {
                        T comp = Components.ElementAt(i) as T;
                        comps.Add(comp);
                    }
                    return comps;
                }
                return null;
            }
            catch
            {
                return null;
            }

            
        }

        private static T findByNameTemplate<T>(string Name, Func<string, string, GuiComponent> FindMethod) where T : class
        {
            try
            {
                return FindMethod(Name, typeof(T).Name) as T;
            }
            catch
            {
                return null;
            }
        }

        private static T findByNameExTemplate<T>(string Name, int TypeId, Func<string, int, GuiComponent> FindMethod) where T : class
        {
            try
            {
                return FindMethod(Name, TypeId) as T;
            }
            catch
            {
                return null;
            }
        }



        private static T findByIdTemplate<T>(string Id, Func<string,object, GuiComponent> FindById) where T : class
        {
            try
            {
                return FindById(Id, Type.Missing) as T;
            }
            catch
            {
                return null;
            }
            
        }

        private static T findChildByConditionTemplate<T>(GuiComponentCollection Components, Func<T, bool> Condition) where T : class
        {
            for (int i = 0; i < Components.Count; i++)
            {
                var component = Components.ElementAt(i) as T;
                if (component != null && Condition(component))
                {
                    return component;
                }
            }
            return null;
        }

    }
}

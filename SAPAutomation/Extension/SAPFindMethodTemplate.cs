using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPAutomation
{
    public static partial class SAPFEExtension
    {
        private static IEnumerable<T> findAllByNameTemplate<T>(string Name, Func<string, string, GuiComponentCollection> FindAllByName)
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

        private static IEnumerable<T> findAllByNameExTemplate<T>(string Name,int TypeId, Func<string, int, GuiComponentCollection> FindAllByNameEx) where T:class
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

        private static T findChildByPropertyTemplate<T>(GuiComponentCollection Components, Func<T, bool> Property) where T : class
        {
            if (Property == null)
                Property = new Func<T, bool>(t => true);
            for (int i = 0; i < Components.Count; i++)
            {
                var component = Components.ElementAt(i) as T;
                if (component != null && Property(component))
                {
                    return component;
                }
            }
            return null;
        }

        private static IEnumerable<T> findChildrenByPropertyTemplate<T>(GuiComponentCollection Components,Func<T,bool> Property) where T:class
        {
            if (Property == null)
                Property = new Func<T, bool>(t => true);
            for(int i=0;i<Components.Count;i++)
            {
                var component = Components.ElementAt(i) as T;
                if(component != null && Property(component))
                {
                    yield return component;
                }
            }
             
        }

        

        private static IEnumerable<T> findDescendantsByPropertyTemplate<T>(GuiComponentCollection Components,Func<T,bool> Property) where T:class
        {
            List<T> itemList = new List<T>();
            if (Property == null)
                Property = new Func<T, bool>(t => true);
            findDescendants<T>(Components, itemList, Property);
            return itemList;
            
        }

        private static void findDescendants<T>(GuiComponentCollection Components,List<T> ItemList, Func<T,bool> Property) where T:class
        {
            for (int i = 0; i < Components.Count; i++)
            {
                var component = Components.ElementAt(i);
                var tComponent = component as T;
                if (tComponent != null && Property(tComponent))
                {
                    ItemList.Add(tComponent);
                }
                else if (component is GuiContainer)
                {
                    findDescendants<T>(((GuiContainer)component).Children, ItemList, Property);
                }
                else if (component is GuiVContainer)
                {
                    findDescendants<T>(((GuiVContainer)component).Children, ItemList, Property);
                }
            }
        }

        private static void findDescendant<T>(GuiComponentCollection Components, Func<T, bool> Property) where T : class
        {
            

            for (int i = 0; i < Components.Count; i++)
            {
                if (item != null)
                    return;
                var component = Components.ElementAt(i);
                
                
                var tComponent = component as T;
                if (tComponent != null && Property(tComponent))
                {
                    item = tComponent;
                }
                else if (component is GuiContainer)
                {
                    findDescendant<T>(((GuiContainer)component).Children,  Property);
                }
                else if (component is GuiVContainer)
                {
                    findDescendant<T>(((GuiVContainer)component).Children,  Property);
                }
            }

        }

        static object item;
        static object _lockObj = new object();

        private static T findDescendantByPropertyTemplate<T>(GuiComponentCollection Components,Func<T,bool> Property) where T :class
        {
            lock (_lockObj) {
                item = null;
                findDescendant<T>(Components, Property);
                return item as T;
            }
            
        }

       

        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPFEWSELib;

namespace SAPTestRunTime
{
    public static partial class SAPFEExtension
    {
        [Obsolete]
        public static T GetComponentById<T>(this GuiSession session, string id) where T : class
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

        [Obsolete]
        public static T TryGetComponentById<T>(this GuiSession session, string id) where T : class
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

        public static T FindById<T>(this GuiSession session, string id) where T : class
        {
            T element = session.FindById(id) as T;
            return element;
        }

        public static T TryFindById<T>(this GuiSession session, string id) where T : class
        {
            try
            {
                return FindById<T>(session, id);
            }
            catch
            {
                return null;
            }
        }



        public static T FindByName<T>(this GuiFrameWindow Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiFrameWindow>(Container, Name);
        }

        public static T FindByName<T>(this GuiMainWindow Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiMainWindow>(Container, Name);
        }

        public static T FindByName<T>(this GuiModalWindow Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiModalWindow>(Container, Name);
        }

        public static T FindByName<T>(this GuiVContainer Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiVContainer>(Container, Name);
        }

        public static T FindByName<T>(this GuiContextMenu Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiContextMenu>(Container, Name);
        }

        public static T FindByName<T>(this GuiUserArea Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiUserArea>(Container, Name);
        }

        public static T FindByName<T>(this GuiSplitterContainer Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiSplitterContainer>(Container, Name);
        }

        public static T FindByName<T>(this GuiShell Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiShell>(Container, Name);
        }

        public static T FindByName<T>(this GuiContainerShell Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiContainerShell>(Container, Name);
        }

        public static T FindByName<T>(this GuiTab Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiTab>(Container, Name);
        }

        public static T FindByName<T>(this GuiTabStrip Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiTabStrip>(Container, Name);
        }

        public static T FindByName<T>(this GuiCustomControl Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiCustomControl>(Container, Name);
        }

        public static T FindByName<T>(this GuiToolbar Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiToolbar>(Container, Name);
        }

        public static T FindByName<T>(this GuiMenubar Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiMenubar>(Container, Name);
        }

        public static T FindByName<T>(this GuiMenu Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiMenu>(Container, Name);
        }

        public static T FindByName<T>(this GuiSimpleContainer Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiSimpleContainer>(Container, Name);
        }

        public static T FindByName<T>(this GuiScrollContainer Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiScrollContainer>(Container, Name);
        }

        public static T FindByName<T>(this GuiStatusbar Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiStatusbar>(Container, Name);
        }

        public static T FindByName<T>(this GuiTableControl Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiTableControl>(Container, Name);
        }

        public static T FindByName<T>(this GuiTitlebar Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiTitlebar>(Container, Name);
        }

        public static T FindByName<T>(this GuiGOSShell Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiGOSShell>(Container, Name);
        }

        public static T FindByName<T>(this GuiDialogShell Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiDialogShell>(Container, Name);
        }

        public static T FindByName<T>(this GuiGridView Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiGridView>(Container, Name);
        }

        public static T FindByName<T>(this GuiTree Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiTree>(Container, Name);
        }

        public static T FindByName<T>(this GuiSplit Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiSplit>(Container, Name);
        }

        public static T FindByName<T>(this GuiPicture Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiPicture>(Container, Name);
        }

        public static T FindByName<T>(this GuiToolbarControl Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiToolbarControl>(Container, Name);
        }

        public static T FindByName<T>(this GuiTextedit Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiTextedit>(Container, Name);
        }

        public static T FindByName<T>(this GuiOfficeIntegration Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiOfficeIntegration>(Container, Name);
        }

        public static T FindByName<T>(this GuiHTMLViewer Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiHTMLViewer>(Container, Name);
        }

        public static T FindByName<T>(this GuiCalendar Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiCalendar>(Container, Name);
        }

        public static T FindByName<T>(this GuiBarChart Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiBarChart>(Container, Name);
        }

        public static T FindByName<T>(this GuiNetChart Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiNetChart>(Container, Name);
        }

        public static T FindByName<T>(this GuiColorSelector Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiColorSelector>(Container, Name);
        }

        public static T FindByName<T>(this GuiSapChart Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiSapChart>(Container, Name);
        }

        public static T FindByName<T>(this GuiChart Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiChart>(Container, Name);
        }

        public static T FindByName<T>(this GuiMap Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiMap>(Container, Name);
        }

        public static T FindByName<T>(this GuiStage Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiStage>(Container, Name);
        }

        public static T FindByName<T>(this GuiGraphAdapt Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiGraphAdapt>(Container, Name);
        }

        public static T FindByName<T>(this GuiEAIViewer2D Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiEAIViewer2D>(Container, Name);
        }

        public static T FindByName<T>(this GuiEAIViewer3D Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiEAIViewer3D>(Container, Name);
        }

        public static T FindByName<T>(this GuiApoGrid Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiApoGrid>(Container, Name);
        }

        public static T FindByName<T>(this GuiAbapEditor Container, string Name)
            where T : class
        {
            return findByNameTemplate<T, GuiAbapEditor>(Container, Name);
        }

        private static TReturn findByNameTemplate<TReturn, TInput>(TInput Container, string Name)
            where TReturn : class
            where TInput : class
        {
            if (Container is GuiVContainer)
            {
                var container = Container as GuiVContainer;
                TReturn item = container.FindByName(Name, typeof(TReturn).Name) as TReturn;
                return item;
            }
            else
            {
                throw new Exception("Not Found FindByName method in type:" + typeof(TInput).Name);
            }
        }


    }
}

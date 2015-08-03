using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPAutomation
{
    public static partial class SAPFEExtension
    {

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiFrameWindow FrameWindow, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, FrameWindow.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiMainWindow MainWindow, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, MainWindow.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiModalWindow ModalWindow, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, ModalWindow.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiVContainer VContainer, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, VContainer.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiContextMenu ContextMenu, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, ContextMenu.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiUserArea UserArea, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, UserArea.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiSplitterContainer SplitterContainer, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, SplitterContainer.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiShell Shell, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, Shell.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiContainerShell ContainerShell, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, ContainerShell.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiTab Tab, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, Tab.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiTabStrip TabStrip, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, TabStrip.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiCustomControl CustomControl, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, CustomControl.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiToolbar Toolbar, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, Toolbar.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiMenubar Menubar, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, Menubar.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiMenu Menu, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, Menu.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiSimpleContainer SimpleContainer, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, SimpleContainer.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiScrollContainer ScrollContainer, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, ScrollContainer.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiStatusbar Statusbar, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, Statusbar.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiTableControl TableControl, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, TableControl.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiTitlebar Titlebar, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, Titlebar.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiGOSShell GOSShell, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, GOSShell.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiDialogShell DialogShell, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, DialogShell.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiGridView GridView, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, GridView.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiTree Tree, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, Tree.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiSplit Split, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, Split.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiPicture Picture, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, Picture.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiToolbarControl ToolbarControl, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, ToolbarControl.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiTextedit Textedit, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, Textedit.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiOfficeIntegration OfficeIntegration, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, OfficeIntegration.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiHTMLViewer HTMLViewer, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, HTMLViewer.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiCalendar Calendar, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, Calendar.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiBarChart BarChart, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, BarChart.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiNetChart NetChart, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, NetChart.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiColorSelector ColorSelector, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, ColorSelector.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiSapChart SapChart, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, SapChart.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiChart Chart, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, Chart.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiMap Map, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, Map.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiStage Stage, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, Stage.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiGraphAdapt GraphAdapt, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, GraphAdapt.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiEAIViewer2D EAIViewer2D, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, EAIViewer2D.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiEAIViewer3D EAIViewer3D, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, EAIViewer3D.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiApoGrid ApoGrid, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, ApoGrid.FindAllByNameEx);
        }

        public static IEnumerable<T> FindAllByNameEx<T>(this GuiAbapEditor AbapEditor, string Name, int TypeId)
            where T : class
        {
            return findAllByNameExTemplate<T>(Name, TypeId, AbapEditor.FindAllByNameEx);
        }
    }
}

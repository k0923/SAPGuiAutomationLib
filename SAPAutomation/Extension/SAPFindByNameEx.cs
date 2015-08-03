using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPAutomation
{
    public static partial class SAPFEExtension
    {

        public static T FindByNameEx<T>(this GuiFrameWindow FrameWindow, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, FrameWindow.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiMainWindow MainWindow, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, MainWindow.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiModalWindow ModalWindow, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, ModalWindow.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiVContainer VContainer, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, VContainer.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiContextMenu ContextMenu, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, ContextMenu.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiUserArea UserArea, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, UserArea.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiSplitterContainer SplitterContainer, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, SplitterContainer.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiShell Shell, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, Shell.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiContainerShell ContainerShell, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, ContainerShell.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiTab Tab, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, Tab.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiTabStrip TabStrip, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, TabStrip.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiCustomControl CustomControl, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, CustomControl.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiToolbar Toolbar, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, Toolbar.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiMenubar Menubar, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, Menubar.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiMenu Menu, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, Menu.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiSimpleContainer SimpleContainer, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, SimpleContainer.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiScrollContainer ScrollContainer, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, ScrollContainer.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiStatusbar Statusbar, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, Statusbar.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiTableControl TableControl, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, TableControl.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiTitlebar Titlebar, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, Titlebar.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiGOSShell GOSShell, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, GOSShell.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiDialogShell DialogShell, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, DialogShell.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiGridView GridView, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, GridView.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiTree Tree, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, Tree.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiSplit Split, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, Split.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiPicture Picture, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, Picture.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiToolbarControl ToolbarControl, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, ToolbarControl.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiTextedit Textedit, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, Textedit.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiOfficeIntegration OfficeIntegration, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, OfficeIntegration.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiHTMLViewer HTMLViewer, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, HTMLViewer.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiCalendar Calendar, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, Calendar.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiBarChart BarChart, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, BarChart.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiNetChart NetChart, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, NetChart.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiColorSelector ColorSelector, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, ColorSelector.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiSapChart SapChart, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, SapChart.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiChart Chart, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, Chart.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiMap Map, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, Map.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiStage Stage, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, Stage.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiGraphAdapt GraphAdapt, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, GraphAdapt.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiEAIViewer2D EAIViewer2D, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, EAIViewer2D.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiEAIViewer3D EAIViewer3D, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, EAIViewer3D.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiApoGrid ApoGrid, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, ApoGrid.FindByNameEx);
        }

        public static T FindByNameEx<T>(this GuiAbapEditor AbapEditor, string Name, int TypeId)
            where T : class
        {
            return findByNameExTemplate<T>(Name, TypeId, AbapEditor.FindByNameEx);
        }
    }
}

using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPAutomation
{
    public static partial class SAPFEExtension
    {
        
        public static T FindByName<T>(this GuiFrameWindow FrameWindow, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, FrameWindow.FindByName);
        }

        public static T FindByName<T>(this GuiMainWindow MainWindow, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, MainWindow.FindByName);
        }

        public static T FindByName<T>(this GuiModalWindow ModalWindow, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, ModalWindow.FindByName);
        }

        public static T FindByName<T>(this GuiVContainer VContainer, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, VContainer.FindByName);
        }

        public static T FindByName<T>(this GuiContextMenu ContextMenu, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, ContextMenu.FindByName);
        }

        public static T FindByName<T>(this GuiUserArea UserArea, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, UserArea.FindByName);
        }

        public static T FindByName<T>(this GuiSplitterContainer SplitterContainer, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, SplitterContainer.FindByName);
        }

        public static T FindByName<T>(this GuiShell Shell, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, Shell.FindByName);
        }

        public static T FindByName<T>(this GuiContainerShell ContainerShell, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, ContainerShell.FindByName);
        }

        public static T FindByName<T>(this GuiTab Tab, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, Tab.FindByName);
        }

        public static T FindByName<T>(this GuiTabStrip TabStrip, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, TabStrip.FindByName);
        }

        public static T FindByName<T>(this GuiCustomControl CustomControl, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, CustomControl.FindByName);
        }

        public static T FindByName<T>(this GuiToolbar Toolbar, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, Toolbar.FindByName);
        }

        public static T FindByName<T>(this GuiMenubar Menubar, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, Menubar.FindByName);
        }

        public static T FindByName<T>(this GuiMenu Menu, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, Menu.FindByName);
        }

        public static T FindByName<T>(this GuiSimpleContainer SimpleContainer, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, SimpleContainer.FindByName);
        }

        public static T FindByName<T>(this GuiScrollContainer ScrollContainer, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, ScrollContainer.FindByName);
        }

        public static T FindByName<T>(this GuiStatusbar Statusbar, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, Statusbar.FindByName);
        }

        public static T FindByName<T>(this GuiTableControl TableControl, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, TableControl.FindByName);
        }

        public static T FindByName<T>(this GuiTitlebar Titlebar, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, Titlebar.FindByName);
        }

        public static T FindByName<T>(this GuiGOSShell GOSShell, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, GOSShell.FindByName);
        }

        public static T FindByName<T>(this GuiDialogShell DialogShell, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, DialogShell.FindByName);
        }

        public static T FindByName<T>(this GuiGridView GridView, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, GridView.FindByName);
        }

        public static T FindByName<T>(this GuiTree Tree, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, Tree.FindByName);
        }

        public static T FindByName<T>(this GuiSplit Split, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, Split.FindByName);
        }

        public static T FindByName<T>(this GuiPicture Picture, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, Picture.FindByName);
        }

        public static T FindByName<T>(this GuiToolbarControl ToolbarControl, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, ToolbarControl.FindByName);
        }

        public static T FindByName<T>(this GuiTextedit Textedit, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, Textedit.FindByName);
        }

        public static T FindByName<T>(this GuiOfficeIntegration OfficeIntegration, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, OfficeIntegration.FindByName);
        }

        public static T FindByName<T>(this GuiHTMLViewer HTMLViewer, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, HTMLViewer.FindByName);
        }

        public static T FindByName<T>(this GuiCalendar Calendar, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, Calendar.FindByName);
        }

        public static T FindByName<T>(this GuiBarChart BarChart, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, BarChart.FindByName);
        }

        public static T FindByName<T>(this GuiNetChart NetChart, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, NetChart.FindByName);
        }

        public static T FindByName<T>(this GuiColorSelector ColorSelector, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, ColorSelector.FindByName);
        }

        public static T FindByName<T>(this GuiSapChart SapChart, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, SapChart.FindByName);
        }

        public static T FindByName<T>(this GuiChart Chart, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, Chart.FindByName);
        }

        public static T FindByName<T>(this GuiMap Map, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, Map.FindByName);
        }

        public static T FindByName<T>(this GuiStage Stage, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, Stage.FindByName);
        }

        public static T FindByName<T>(this GuiGraphAdapt GraphAdapt, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, GraphAdapt.FindByName);
        }

        public static T FindByName<T>(this GuiEAIViewer2D EAIViewer2D, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, EAIViewer2D.FindByName);
        }

        public static T FindByName<T>(this GuiEAIViewer3D EAIViewer3D, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, EAIViewer3D.FindByName);
        }

        public static T FindByName<T>(this GuiApoGrid ApoGrid, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, ApoGrid.FindByName);
        }

        public static T FindByName<T>(this GuiAbapEditor AbapEditor, string Name)
            where T : class
        {
            return findByNameTemplate<T>(Name, AbapEditor.FindByName);
        }

        
    }
}

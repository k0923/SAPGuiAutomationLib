using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPAutomation
{
    public static partial class SAPFEExtension
    {
        public static IEnumerable<T> FindAllByName<T>(this GuiFrameWindow FrameWindow, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, FrameWindow.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiMainWindow MainWindow, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, MainWindow.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiModalWindow ModalWindow, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, ModalWindow.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiVContainer VContainer, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, VContainer.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiContextMenu ContextMenu, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, ContextMenu.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiUserArea UserArea, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, UserArea.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiSplitterContainer SplitterContainer, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, SplitterContainer.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiShell Shell, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, Shell.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiContainerShell ContainerShell, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, ContainerShell.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiTab Tab, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, Tab.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiTabStrip TabStrip, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, TabStrip.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiCustomControl CustomControl, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, CustomControl.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiToolbar Toolbar, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, Toolbar.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiMenubar Menubar, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, Menubar.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiMenu Menu, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, Menu.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiSimpleContainer SimpleContainer, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, SimpleContainer.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiScrollContainer ScrollContainer, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, ScrollContainer.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiStatusbar Statusbar, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, Statusbar.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiTableControl TableControl, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, TableControl.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiTitlebar Titlebar, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, Titlebar.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiGOSShell GOSShell, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, GOSShell.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiDialogShell DialogShell, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, DialogShell.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiGridView GridView, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, GridView.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiTree Tree, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, Tree.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiSplit Split, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, Split.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiPicture Picture, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, Picture.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiToolbarControl ToolbarControl, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, ToolbarControl.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiTextedit Textedit, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, Textedit.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiOfficeIntegration OfficeIntegration, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, OfficeIntegration.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiHTMLViewer HTMLViewer, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, HTMLViewer.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiCalendar Calendar, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, Calendar.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiBarChart BarChart, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, BarChart.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiNetChart NetChart, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, NetChart.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiColorSelector ColorSelector, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, ColorSelector.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiSapChart SapChart, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, SapChart.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiChart Chart, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, Chart.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiMap Map, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, Map.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiStage Stage, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, Stage.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiGraphAdapt GraphAdapt, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, GraphAdapt.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiEAIViewer2D EAIViewer2D, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, EAIViewer2D.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiEAIViewer3D EAIViewer3D, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, EAIViewer3D.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiApoGrid ApoGrid, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, ApoGrid.FindAllByName);
        }

        public static IEnumerable<T> FindAllByName<T>(this GuiAbapEditor AbapEditor, string Name)
            where T : class
        {
            return findAllByNameTemplate<T>(Name, AbapEditor.FindAllByName);
        }
    }
}

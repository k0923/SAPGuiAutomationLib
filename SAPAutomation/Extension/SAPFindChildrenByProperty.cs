using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPAutomation.Extension
{
    public static partial class SAPFEExtension
    {
        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiConnection Connection, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(Connection.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiSession Session, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(Session.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiFrameWindow FrameWindow, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(FrameWindow.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiApplication Application, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(Application.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiMainWindow MainWindow, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(MainWindow.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiModalWindow ModalWindow, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(ModalWindow.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiContainer Container, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(Container.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiVContainer VContainer, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(VContainer.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiContextMenu ContextMenu, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(ContextMenu.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiUserArea UserArea, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(UserArea.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiSplitterContainer SplitterContainer, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(SplitterContainer.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiShell Shell, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(Shell.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiContainerShell ContainerShell, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(ContainerShell.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiTab Tab, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(Tab.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiTabStrip TabStrip, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(TabStrip.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiCustomControl CustomControl, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(CustomControl.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiToolbar Toolbar, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(Toolbar.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiMenubar Menubar, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(Menubar.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiMenu Menu, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(Menu.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiSimpleContainer SimpleContainer, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(SimpleContainer.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiScrollContainer ScrollContainer, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(ScrollContainer.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiStatusbar Statusbar, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(Statusbar.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiTableControl TableControl, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(TableControl.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiTitlebar Titlebar, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(Titlebar.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiGOSShell GOSShell, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(GOSShell.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiDialogShell DialogShell, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(DialogShell.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiGridView GridView, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(GridView.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiTree Tree, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(Tree.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiSplit Split, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(Split.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiPicture Picture, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(Picture.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiToolbarControl ToolbarControl, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(ToolbarControl.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiTextedit Textedit, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(Textedit.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiOfficeIntegration OfficeIntegration, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(OfficeIntegration.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiHTMLViewer HTMLViewer, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(HTMLViewer.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiCalendar Calendar, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(Calendar.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiBarChart BarChart, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(BarChart.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiNetChart NetChart, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(NetChart.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiColorSelector ColorSelector, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(ColorSelector.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiSapChart SapChart, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(SapChart.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiChart Chart, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(Chart.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiMap Map, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(Map.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiStage Stage, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(Stage.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiGraphAdapt GraphAdapt, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(GraphAdapt.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiEAIViewer2D EAIViewer2D, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(EAIViewer2D.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiEAIViewer3D EAIViewer3D, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(EAIViewer3D.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiApoGrid ApoGrid, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(ApoGrid.Children, Property);
        }

        public static IEnumerable<T> FindChildrenByProperty<T>(this GuiAbapEditor AbapEditor, Func<T, bool> Property)
            where T : class
        {
            return findChildrenByPropertyTemplate<T>(AbapEditor.Children, Property);
        }
    }
}

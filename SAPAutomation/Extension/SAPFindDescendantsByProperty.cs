using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPAutomation
{
    public static partial class SAPFEExtension
    {
        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiConnection Connection, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(Connection.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiSession Session, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(Session.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiFrameWindow FrameWindow, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(FrameWindow.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiApplication Application, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(Application.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiMainWindow MainWindow, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(MainWindow.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiModalWindow ModalWindow, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(ModalWindow.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiContainer Container, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(Container.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiVContainer VContainer, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(VContainer.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiContextMenu ContextMenu, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(ContextMenu.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiUserArea UserArea, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(UserArea.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiSplitterContainer SplitterContainer, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(SplitterContainer.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiShell Shell, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(Shell.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiContainerShell ContainerShell, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(ContainerShell.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiTab Tab, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(Tab.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiTabStrip TabStrip, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(TabStrip.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiCustomControl CustomControl, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(CustomControl.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiToolbar Toolbar, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(Toolbar.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiMenubar Menubar, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(Menubar.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiMenu Menu, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(Menu.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiSimpleContainer SimpleContainer, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(SimpleContainer.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiScrollContainer ScrollContainer, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(ScrollContainer.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiStatusbar Statusbar, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(Statusbar.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiTableControl TableControl, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(TableControl.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiTitlebar Titlebar, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(Titlebar.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiGOSShell GOSShell, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(GOSShell.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiDialogShell DialogShell, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(DialogShell.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiGridView GridView, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(GridView.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiTree Tree, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(Tree.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiSplit Split, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(Split.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiPicture Picture, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(Picture.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiToolbarControl ToolbarControl, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(ToolbarControl.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiTextedit Textedit, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(Textedit.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiOfficeIntegration OfficeIntegration, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(OfficeIntegration.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiHTMLViewer HTMLViewer, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(HTMLViewer.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiCalendar Calendar, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(Calendar.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiBarChart BarChart, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(BarChart.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiNetChart NetChart, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(NetChart.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiColorSelector ColorSelector, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(ColorSelector.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiSapChart SapChart, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(SapChart.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiChart Chart, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(Chart.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiMap Map, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(Map.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiStage Stage, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(Stage.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiGraphAdapt GraphAdapt, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(GraphAdapt.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiEAIViewer2D EAIViewer2D, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(EAIViewer2D.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiEAIViewer3D EAIViewer3D, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(EAIViewer3D.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiApoGrid ApoGrid, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(ApoGrid.Children, Property);
        }

        public static IEnumerable<T> FindDescendantsByProperty<T>(this GuiAbapEditor AbapEditor, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantsByPropertyTemplate<T>(AbapEditor.Children, Property);
        }
    }
}

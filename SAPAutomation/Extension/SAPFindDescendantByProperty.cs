using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPAutomation
{
    public static partial class SAPFEExtension
    {
        public static T FindDescendantByProperty<T>(this GuiConnection Connection, Func<T, bool> Property = null)
            where T : class
        {
            if (Property == null)
                Property = new Func<T, bool>(t => true);
            return findDescendantByPropertyTemplate<T>(Connection.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiSession Session, Func<T, bool> Property = null)
            where T : class
        {
            if (Property == null)
                Property = new Func<T, bool>(t => true);
            return findDescendantByPropertyTemplate<T>(Session.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiFrameWindow FrameWindow, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(FrameWindow.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiApplication Application, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(Application.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiMainWindow MainWindow, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(MainWindow.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiModalWindow ModalWindow, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(ModalWindow.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiContainer Container, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(Container.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiVContainer VContainer, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(VContainer.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiContextMenu ContextMenu, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(ContextMenu.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiUserArea UserArea, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(UserArea.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiSplitterContainer SplitterContainer, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(SplitterContainer.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiShell Shell, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(Shell.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiContainerShell ContainerShell, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(ContainerShell.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiTab Tab, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(Tab.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiTabStrip TabStrip, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(TabStrip.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiCustomControl CustomControl, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(CustomControl.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiToolbar Toolbar, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(Toolbar.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiMenubar Menubar, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(Menubar.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiMenu Menu, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(Menu.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiSimpleContainer SimpleContainer, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(SimpleContainer.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiScrollContainer ScrollContainer, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(ScrollContainer.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiStatusbar Statusbar, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(Statusbar.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiTableControl TableControl, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(TableControl.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiTitlebar Titlebar, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(Titlebar.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiGOSShell GOSShell, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(GOSShell.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiDialogShell DialogShell, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(DialogShell.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiGridView GridView, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(GridView.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiTree Tree, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(Tree.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiSplit Split, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(Split.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiPicture Picture, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(Picture.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiToolbarControl ToolbarControl, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(ToolbarControl.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiTextedit Textedit, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(Textedit.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiOfficeIntegration OfficeIntegration, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(OfficeIntegration.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiHTMLViewer HTMLViewer, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(HTMLViewer.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiCalendar Calendar, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(Calendar.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiBarChart BarChart, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(BarChart.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiNetChart NetChart, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(NetChart.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiColorSelector ColorSelector, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(ColorSelector.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiSapChart SapChart, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(SapChart.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiChart Chart, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(Chart.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiMap Map, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(Map.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiStage Stage, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(Stage.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiGraphAdapt GraphAdapt, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(GraphAdapt.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiEAIViewer2D EAIViewer2D, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(EAIViewer2D.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiEAIViewer3D EAIViewer3D, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(EAIViewer3D.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiApoGrid ApoGrid, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(ApoGrid.Children, Property);
        }

        public static T FindDescendantByProperty<T>(this GuiAbapEditor AbapEditor, Func<T, bool> Property = null)
            where T : class
        {
            return findDescendantByPropertyTemplate<T>(AbapEditor.Children, Property);
        }
    }
}

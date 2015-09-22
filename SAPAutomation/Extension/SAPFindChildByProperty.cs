using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPAutomation
{
    public static partial class SAPFEExtension
    {
        public static T FindChildByProperty<T>(this GuiConnection Connection, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(Connection.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiSession Session, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(Session.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiFrameWindow FrameWindow, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(FrameWindow.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiApplication Application, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(Application.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiMainWindow MainWindow, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(MainWindow.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiModalWindow ModalWindow, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(ModalWindow.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiContainer Container, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(Container.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiVContainer VContainer, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(VContainer.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiContextMenu ContextMenu, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(ContextMenu.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiUserArea UserArea, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(UserArea.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiSplitterContainer SplitterContainer, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(SplitterContainer.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiShell Shell, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(Shell.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiContainerShell ContainerShell, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(ContainerShell.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiTab Tab, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(Tab.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiTabStrip TabStrip, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(TabStrip.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiCustomControl CustomControl, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(CustomControl.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiToolbar Toolbar, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(Toolbar.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiMenubar Menubar, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(Menubar.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiMenu Menu, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(Menu.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiSimpleContainer SimpleContainer, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(SimpleContainer.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiScrollContainer ScrollContainer, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(ScrollContainer.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiStatusbar Statusbar, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(Statusbar.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiTableControl TableControl, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(TableControl.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiTitlebar Titlebar, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(Titlebar.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiGOSShell GOSShell, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(GOSShell.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiDialogShell DialogShell, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(DialogShell.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiGridView GridView, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(GridView.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiTree Tree, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(Tree.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiSplit Split, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(Split.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiPicture Picture, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(Picture.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiToolbarControl ToolbarControl, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(ToolbarControl.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiTextedit Textedit, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(Textedit.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiOfficeIntegration OfficeIntegration, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(OfficeIntegration.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiHTMLViewer HTMLViewer, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(HTMLViewer.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiCalendar Calendar, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(Calendar.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiBarChart BarChart, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(BarChart.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiNetChart NetChart, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(NetChart.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiColorSelector ColorSelector, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(ColorSelector.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiSapChart SapChart, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(SapChart.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiChart Chart, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(Chart.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiMap Map, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(Map.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiStage Stage, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(Stage.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiGraphAdapt GraphAdapt, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(GraphAdapt.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiEAIViewer2D EAIViewer2D, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(EAIViewer2D.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiEAIViewer3D EAIViewer3D, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(EAIViewer3D.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiApoGrid ApoGrid, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(ApoGrid.Children, Property);
        }

        public static T FindChildByProperty<T>(this GuiAbapEditor AbapEditor, Func<T, bool> Property = null)
            where T : class
        {
            return findChildByPropertyTemplate<T>(AbapEditor.Children, Property);
        }
    }
}

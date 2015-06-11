using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPAutomation.Extension
{
    public static partial class SAPFEExtension
    {
        public static T FindChildByCondition<T>(this GuiConnection Connection, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(Connection.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiSession Session, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(Session.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiFrameWindow FrameWindow, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(FrameWindow.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiApplication Application, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(Application.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiMainWindow MainWindow, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(MainWindow.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiModalWindow ModalWindow, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(ModalWindow.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiContainer Container, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(Container.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiVContainer VContainer, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(VContainer.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiContextMenu ContextMenu, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(ContextMenu.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiUserArea UserArea, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(UserArea.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiSplitterContainer SplitterContainer, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(SplitterContainer.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiShell Shell, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(Shell.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiContainerShell ContainerShell, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(ContainerShell.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiTab Tab, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(Tab.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiTabStrip TabStrip, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(TabStrip.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiCustomControl CustomControl, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(CustomControl.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiToolbar Toolbar, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(Toolbar.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiMenubar Menubar, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(Menubar.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiMenu Menu, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(Menu.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiSimpleContainer SimpleContainer, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(SimpleContainer.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiScrollContainer ScrollContainer, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(ScrollContainer.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiStatusbar Statusbar, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(Statusbar.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiTableControl TableControl, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(TableControl.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiTitlebar Titlebar, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(Titlebar.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiGOSShell GOSShell, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(GOSShell.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiDialogShell DialogShell, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(DialogShell.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiGridView GridView, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(GridView.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiTree Tree, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(Tree.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiSplit Split, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(Split.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiPicture Picture, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(Picture.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiToolbarControl ToolbarControl, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(ToolbarControl.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiTextedit Textedit, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(Textedit.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiOfficeIntegration OfficeIntegration, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(OfficeIntegration.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiHTMLViewer HTMLViewer, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(HTMLViewer.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiCalendar Calendar, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(Calendar.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiBarChart BarChart, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(BarChart.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiNetChart NetChart, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(NetChart.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiColorSelector ColorSelector, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(ColorSelector.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiSapChart SapChart, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(SapChart.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiChart Chart, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(Chart.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiMap Map, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(Map.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiStage Stage, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(Stage.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiGraphAdapt GraphAdapt, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(GraphAdapt.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiEAIViewer2D EAIViewer2D, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(EAIViewer2D.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiEAIViewer3D EAIViewer3D, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(EAIViewer3D.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiApoGrid ApoGrid, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(ApoGrid.Children, Condition);
        }

        public static T FindChildByCondition<T>(this GuiAbapEditor AbapEditor, Func<T, bool> Condition)
            where T : class
        {
            return findChildByConditionTemplate<T>(AbapEditor.Children, Condition);
        }
    }
}

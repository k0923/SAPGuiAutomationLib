using SAPAutomation;
using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPGuiDemo
{
    public partial class Demo
    {
        public static void LegacyGetElement()
        {
            //1 By Id
            GuiComponent comp = SAPTestHelper.Current.SAPGuiSession.FindById("/app/con[0]/ses[0]/wnd[0]/tbar[0]/okcd");
            (comp as GuiOkCodeField).Text = "SE16";
            //2 By Name
            GuiComponent comp1 = SAPTestHelper.Current.MainWindow.FindByName("okcd", "GuiOkCodeField");
            (comp as GuiOkCodeField).Text = "SE16";
        }


        public static void GetElement()
        {
            //1 By Id
            SAPTestHelper.Current.SAPGuiSession.FindById<GuiOkCodeField>("/app/con[0]/ses[0]/wnd[0]/tbar[0]/okcd").Text = "SE16";
            //2 By Name
            SAPTestHelper.Current.MainWindow.FindByName<GuiOkCodeField>("okcd").Text = "SE16";
            //3 By Condition
            SAPTestHelper.Current.MainWindow.FindDescendantsByProperty<GuiOkCodeField>();
        }
    }
}

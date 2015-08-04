using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPAutomation.Framework
{
    public class SAPGuiScreen
    {
        protected GuiMainWindow MainWindow
        {
            get
            {
                return SAPTestHelper.Current.GetElementById<GuiMainWindow>("wnd[0]");
            }
        }

        public void Enter()
        {
            MainWindow.FindByName<GuiButton>("btn[0]").Press();
        }

        public void Back()
        {
            MainWindow.FindByName<GuiButton>("btn[3]").Press();
        }

        public void Exit()
        {
            MainWindow.FindByName<GuiButton>("btn[15]").Press();
        }

        public void Cancel()
        {
            MainWindow.FindByName<GuiButton>("btn[12]").Press();
        }

        public void StartTransaction(string TCode)
        {
            SAPTestHelper.Current.SAPGuiSession.StartTransaction(TCode);
        }

        public void EndTransaction()
        {
            SAPTestHelper.Current.SAPGuiSession.EndTransaction();
        }
        public string Transaction
        {
            get
            {
                return MainWindow.FindByName<GuiOkCodeField>("okcd").Text;
            }
            set
            {
                MainWindow.FindByName<GuiOkCodeField>("okcd").Text = value;
            }
        }

        public void SendKeys(SAPKeys Key)
        {
            MainWindow.SendVKey((int)Key);
        }
    }

    

    
}

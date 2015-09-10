using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Young.Data;
using Young.Data.Attributes;

namespace SAPAutomation.Framework
{
    [DataBinding]
    public abstract class SAPGuiScreen:DataDriven
    {
        

        public SAPGuiScreen()
        {

        }

       
        public abstract string TCode { get;  }
        

        public static SAPGuiScreen Create<T>() where T:SAPGuiScreen,new()
        {
            return new T();
        }


        
        protected GuiMainWindow MainWindow
        {
            get
            {
                return SAPTestHelper.Current.GetElementById<GuiMainWindow>("wnd[0]");
            }
        }

        protected GuiStatusbar StatusBar
        {
            get
            {
                return SAPTestHelper.Current.GetElementById<GuiStatusbar>("wnd[0]/sbar");
            }
        }

        public string StatusMessage
        {
            get
            {
                return StatusBar.Text;
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

    //public class Test:SAPGuiScreen
    //{
    //    public Test()
    //    {
    //        Test t = new Test();
    //    }
    //}

    

    
}

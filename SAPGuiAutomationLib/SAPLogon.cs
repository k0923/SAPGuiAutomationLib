using SAPFEWSELib;
using SapROTWr;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SAPGuiAutomationLib
{
    public delegate void LoginHandler(GuiSession sender, EventArgs e);
    public class SAPLogon
    {
        private GuiSession _sapGuiSession;
        private GuiConnection _sapGuiConnection;
        private GuiApplication _sapGuiApplication;

        public event LoginHandler BeforeLogin;
        public event LoginHandler AfterLogin;

        public GuiSession SapGuiSession { get { return _sapGuiSession; } }

        public GuiConnection SapGuiConnection { get { return _sapGuiConnection; } }

        public GuiApplication SapGuiApplication { get { return _sapGuiApplication; } }

        public SAPLogon() { }

        public void StartProcess(string processPath ="saplogon.exe")
        {
            Process.Start(processPath);
        }

        public void OpenConnection(string server, int secondsOfTimeout = 10)
        {
            SapROTWr.CSapROTWrapper sapROTWrapper = new SapROTWr.CSapROTWrapper();
            getSAPGuiApplication(sapROTWrapper, secondsOfTimeout);
            _sapGuiApplication.OpenConnectionByConnectionString(server);
            var index = _sapGuiApplication.Connections.Count - 1;
            this._sapGuiConnection = _sapGuiApplication.Children.ElementAt(index) as GuiConnection;
            index = _sapGuiConnection.Sessions.Count - 1;
            this._sapGuiSession = _sapGuiConnection.Children.Item(index) as GuiSession;
        }

        public void Login(string UserName, string Password, string Client, string Language)
        {
            if(BeforeLogin != null)
            {
                BeforeLogin(_sapGuiSession, new EventArgs());
            }

            _sapGuiSession.GetComponentById<GuiTextField>("wnd[0]/usr/txtRSYST-BNAME").Text = UserName;
            _sapGuiSession.GetComponentById<GuiTextField>("wnd[0]/usr/pwdRSYST-BCODE").Text = Password;
            _sapGuiSession.GetComponentById<GuiTextField>("wnd[0]/usr/txtRSYST-MANDT").Text = Client;
            _sapGuiSession.GetComponentById<GuiTextField>("wnd[0]/usr/txtRSYST-LANGU").Text = Language;

            var window = _sapGuiSession.GetComponentById<GuiFrameWindow>("wnd[0]");
            window.SendVKey(0);
           

            if (AfterLogin != null)
            {
                AfterLogin(_sapGuiSession, new EventArgs());
            }
            else
            {
                GuiRadioButton rb_Button = _sapGuiSession.TryGetComponentById<GuiRadioButton>("wnd[1]/usr/radMULTI_LOGON_OPT2");

                if (rb_Button != null)
                {
                    rb_Button.Select();
                    window.SendVKey(0);
                }
            }

            
            
        }

        

        private void getSAPGuiApplication(CSapROTWrapper sapROTWrapper, int secondsOfTimeout)
        {
            object SapGuilRot = sapROTWrapper.GetROTEntry("SAPGUI");
            if (SapGuilRot == null)
            {
                if (secondsOfTimeout < 0)
                {
                    throw new TimeoutException(string.Format("Can get sap script engine in {0} seconds", secondsOfTimeout));
                }
                Thread.Sleep(1000);
                secondsOfTimeout -= 1;
                getSAPGuiApplication(sapROTWrapper, secondsOfTimeout);
            }
            else
            {
                object engine = SapGuilRot.GetType().InvokeMember("GetSCriptingEngine", System.Reflection.BindingFlags.InvokeMethod,
                null, SapGuilRot, null);
                this._sapGuiApplication = engine as GuiApplication;
            }
        }
    }
}

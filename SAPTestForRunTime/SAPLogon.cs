using SAPFEWSELib;
using SapROTWr;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace SAPTestRunTime
{
    public delegate void LoginHandler(GuiSession sender, EventArgs e);

    public class SAPLogon
    {
        private GuiSession _sapGuiSession;
        private GuiConnection _sapGuiConnection;
        private GuiApplication _sapGuiApplication;

        public event LoginHandler FailLogin;
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
            _sapGuiApplication = SAPTestHelper.GetSAPGuiApp(secondsOfTimeout);
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

            _sapGuiSession.FindById<GuiTextField>("wnd[0]/usr/txtRSYST-BNAME").Text = UserName;
            _sapGuiSession.FindById<GuiTextField>("wnd[0]/usr/pwdRSYST-BCODE").Text = Password;
            _sapGuiSession.FindById<GuiTextField>("wnd[0]/usr/txtRSYST-MANDT").Text = Client;
            _sapGuiSession.FindById<GuiTextField>("wnd[0]/usr/txtRSYST-LANGU").Text = Language;
            

            var window = _sapGuiSession.FindById<GuiFrameWindow>("wnd[0]");
            window.SendVKey(0);

            GuiStatusbar status = _sapGuiSession.FindById<GuiStatusbar>("wnd[0]/sbar");
            if(status!=null && status.MessageType.ToLower()=="e")
            {
                _sapGuiConnection.CloseSession(_sapGuiSession.Id);
                if(FailLogin!=null)
                {
                    FailLogin(_sapGuiSession, new EventArgs());
                }
                return;
            }

            if (AfterLogin != null)
            {
                AfterLogin(_sapGuiSession, new EventArgs());
            }

            GuiRadioButton rb_Button = _sapGuiSession.FindById<GuiRadioButton>("wnd[1]/usr/radMULTI_LOGON_OPT2");

            if (rb_Button != null)
            {
                rb_Button.Select();
                window.SendVKey(0);
            }
            
        }

        
        

        
    }
}

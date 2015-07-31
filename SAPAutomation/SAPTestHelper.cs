using SAPFEWSELib;
using SapROTWr;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using SAPAutomation.Extension;

namespace SAPAutomation
{
    public delegate void OnRequestErrorHanlder(GuiSession Session);
    
    public sealed class SAPTestHelper
    {
        public event OnRequestErrorHanlder OnRequestError;
        public event OnRequestErrorHanlder OnRequestBlock;

        private static object _lockObj = new object();

        private static SAPTestHelper _instance;

        public Queue<ScreenData> ScreenDatas { get; private set; }

        private ScreenData _currentScreen;

        private GuiApplication _sapGuiApplication;
        private GuiSession _sapGuiSession;
        private GuiConnection _sapGuiConnection;

        public GuiApplication SAPGuiApplication { get { return _sapGuiApplication; } }
        public GuiSession SAPGuiSession { get { return _sapGuiSession; } }
        public GuiConnection SAPGuiConnection { get { return _sapGuiConnection; } }

        public static GuiApplication GetSAPGuiApp(int secondsOfTimeout = 10)
        {
            SapROTWr.CSapROTWrapper sapROTWrapper = new SapROTWr.CSapROTWrapper();
            return getSAPGuiApp(sapROTWrapper, secondsOfTimeout);
        }

        private static GuiApplication getSAPGuiApp(CSapROTWrapper sapROTWrapper, int secondsOfTimeout)
        {
            
            object SapGuilRot = sapROTWrapper.GetROTEntry("SAPGUI");
            if (secondsOfTimeout < 0)
            {
                throw new TimeoutException(string.Format("Can get sap script engine in {0} seconds", secondsOfTimeout));
            }
            else
            {
                if (SapGuilRot == null)
                {
                    Thread.Sleep(1000);
                    return getSAPGuiApp(sapROTWrapper, secondsOfTimeout - 1);
                }
                else
                {
                    object engine = SapGuilRot.GetType().InvokeMember("GetSCriptingEngine", System.Reflection.BindingFlags.InvokeMethod, null, SapGuilRot, null);
                    if (engine == null)
                        throw new NullReferenceException("No SAP GUI application found");
                    return engine as GuiApplication;
                }
            }
        }

        private SAPTestHelper() 
        {
            this.ScreenDatas = new Queue<ScreenData>();
        }

        public void TurnScreenLog(bool on)
        {
            if(_sapGuiSession != null)
            {
                _sapGuiSession.Record = on;
            }
        }

        public static SAPTestHelper Current
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                            _instance = new SAPTestHelper();
                    }
                }
                return _instance;
            }
        }

        #region SetSession
        public void SetSession(SAPLogon logon)
        {
            SetSession(logon.SapGuiApplication, logon.SapGuiConnection, logon.SapGuiSession);
        }

        public void SetSession(GuiApplication application, GuiConnection connection, GuiSession session)
        {
            this._sapGuiApplication = application;
            this._sapGuiConnection = connection;
            this._sapGuiSession = session;
            _currentScreen = new ScreenData(session.Info.SystemName, session.Info.Transaction,session.Info.Program, session.Info.ScreenNumber);
            hookSessionEvent();
        }

        public void SetSession(string BoxName)
        {
            var application = SAPTestHelper.GetSAPGuiApp();
            GuiConnection connection = null;
            GuiSession session = null;
            int index = application.Connections.Count - 1;
            if (index < 0)
            {
                throw new Exception("No SAP GUI Connections found");
            }
            for (int i = 0; i < application.Children.Count; i++)
            {
                var con = application.Children.ElementAt(i) as GuiConnection;
                index = con.Sessions.Count - 1;
                if (index < 0)
                {
                    throw new Exception("No SAP GUI Session Found");
                }
                for (int j = 0; j < connection.Sessions.Count; j++)
                {
                    var ses = con.Children.ElementAt(j) as GuiSession;
                    if (ses.Info.SystemName.ToLower() == BoxName.ToLower())
                    {
                        session = ses;
                        break;
                    }
                   
                }
                if (session != null)
                {
                    connection = con;
                    break;
                }
            }
            if(session!=null)
            {
                SetSession(application, connection, session);
            }
            else
            {
                throw new Exception("No SAP GUI Session Found");
            }
        }

        public void SetSession()
        {
            var application = SAPTestHelper.GetSAPGuiApp();
            int index = application.Connections.Count - 1;
            if (index < 0)
            {
                throw new Exception("No SAP GUI Connections found");
            }

            var connection = application.Children.ElementAt(index) as GuiConnection;
            index = connection.Sessions.Count - 1;
            if (index < 0)
            {
                throw new Exception("No SAP GUI Session Found");
            }
            var session = connection.Children.ElementAt(index) as GuiSession;

            SetSession(application, connection, session);
        }

        private void hookSessionEvent()
        {
            _sapGuiSession.Destroy -= _sapGuiSession_Destroy;
            _sapGuiSession.Destroy += _sapGuiSession_Destroy;
            _sapGuiSession.EndRequest -= _sapGuiSession_EndRequest;
            _sapGuiSession.EndRequest += _sapGuiSession_EndRequest;
            _sapGuiSession.Change -= _sapGuiSession_Change;
            _sapGuiSession.Change += _sapGuiSession_Change;
        }
        #endregion

        #region Get SAP GUI Component
        public T GetElementById<T>(string id) where T : class
        {
            var component = GetElementById(id);
            T element = component as T;
            return element;
        }

        public GuiComponent GetElementById(string id)
        {
            GuiComponent component = _sapGuiSession.FindById(id);
            return component;
        }

        public GuiComponent TryGetElementById(string id)
        {
            try
            {
                return GetElementById(id);
            }
            catch
            {
                return null;
            }
        }

        public T TryGetElementById<T>(string id) where T : class
        {
            try
            {
                return GetElementById<T>(id);
            }
            catch
            {
                return null;
            }
        }
        

        /// <summary>
        /// This Method will find the element until the timeout, if secondsOfTimeout set to less than 0, this method will continue to find element without timeout
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="secondsOfFrequence"></param>
        /// <param name="secondsOfTimeout"></param>
        /// <returns></returns>
        public T GetElementUntil<T>(string id, int secondsOfFrequence, int secondsOfTimeout) where T : class
        {
            T comp = TryGetElementById<T>(id);
            if (comp == null)
            {
                Thread.Sleep(secondsOfFrequence * 1000);
                if (secondsOfTimeout < 0)
                {
                    return GetElementUntil<T>(id, secondsOfFrequence, secondsOfTimeout);
                }
                else
                {
                    if (secondsOfFrequence == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return GetElementUntil<T>(id, secondsOfFrequence, secondsOfTimeout - 1);
                    }
                }
            }

            return comp;

        }
        #endregion

        #region ScreenShot
        private ImageCodecInfo getEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public void TakeScreenShot(string filePath)
        {
            FileInfo f = new FileInfo(filePath);
            if (!f.Directory.Exists)
                f.Directory.Create();
            GuiFrameWindow win = _sapGuiSession.ActiveWindow;
            win.Restore();
            
            byte[] screenData = (byte[])win.HardCopyToMemory();
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            ImageCodecInfo jpgEncoder = getEncoder(ImageFormat.Jpeg);

            using (var ms = new MemoryStream(screenData))
            {
                Bitmap bmp = new Bitmap(ms);
                EncoderParameters paras = new EncoderParameters(1);

                EncoderParameter para1 = new EncoderParameter(myEncoder, 50L);
                paras.Param[0] = para1;
                bmp.Save(filePath, jpgEncoder, paras);
            }

            
        }
        #endregion

        public void CloseConnection()
        {
            _sapGuiConnection.CloseSession(SAPGuiSession.Id);
            _sapGuiConnection.CloseConnection();
        }

        void _sapGuiSession_Destroy(GuiSession Session)
        {
            _sapGuiSession = null;
        }

        

        void _sapGuiSession_Change(GuiSession Session, GuiComponent Component, object CommandArray)
        {
            if(_currentScreen!=null)
            {
                SAPGuiElement comp = new SAPGuiElement()
                { 
                    Id = Component.Id,
                    Type = Component.Type,
                    Name = Component.Name
                };

                object[] objs = CommandArray as object[];
                objs = objs[0] as object[];
                switch(objs[0].ToString().ToLower())
                {
                    case "m":
                        comp.Action = BindingFlags.InvokeMethod;
                        break;
                    case "sp":
                        comp.Action = BindingFlags.SetProperty;
                        break;
                }
                var action = objs[1].ToString();
                upperFirstChar(ref action);
                comp.ActionName = action;

                var count = objs.Count();

                if(count>2)
                {
                    comp.ActionValues = new object[count - 2];
                    for(int i=2;i<count;i++)
                    {
                        comp.ActionValues[i - 2] = objs[i];
                    }
                }
                _currentScreen.SAPGuiElements.Add(comp);
            }
        }

        private void upperFirstChar(ref string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                s = char.ToUpper(s[0]) + s.Substring(1);
            }
        }

        void _sapGuiSession_EndRequest(GuiSession Session)
        {
            if (_currentScreen != null)
                ScreenDatas.Enqueue(_currentScreen);
            _currentScreen = new ScreenData(Session.Info.SystemName, Session.Info.Transaction,Session.Info.Program, Session.Info.ScreenNumber);
            GuiStatusbar status = _sapGuiSession.FindById<GuiStatusbar>("wnd[0]/sbar");
            if(status !=null)
            {
                switch(status.MessageType)
                {
                    case "E":
                        if(OnRequestError!=null)
                            OnRequestError(Session);
                        break;
                    case "S":
                        if(OnRequestBlock!=null)
                            OnRequestBlock(Session);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

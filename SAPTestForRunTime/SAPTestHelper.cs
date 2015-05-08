using SAPFEWSELib;
using SapROTWr;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace SAPTestRunTime
{
    public sealed class SAPTestHelper
    {
        private static object _lockObj = new object();

        private static SAPTestHelper _instance;

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

        private SAPTestHelper() { }

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

        public void SetSession(SAPLogon logon)
        {
            SetSession(logon.SapGuiApplication, logon.SapGuiConnection, logon.SapGuiSession);
        }

        public void SetSession(GuiApplication application, GuiConnection connection, GuiSession session)
        {
            
            this._sapGuiApplication = application;
            this._sapGuiConnection = connection;
            this._sapGuiSession = session;
        }

        public void SetSession(string BoxName)
        {
            _sapGuiSession = null;
            _sapGuiConnection = null;
            _sapGuiApplication = SAPTestHelper.GetSAPGuiApp();
            int index = _sapGuiApplication.Connections.Count - 1;
            if (index < 0)
            {
                throw new Exception("No SAP GUI Connections found");
            }
            for(int i =0;i<_sapGuiApplication.Children.Count;i++)
            {
                GuiConnection con = _sapGuiApplication.Children.ElementAt(i) as GuiConnection;
                index = con.Sessions.Count - 1;
                if (index < 0)
                {
                    throw new Exception("No SAP GUI Session Found");
                }
                for(int j=0;j<con.Sessions.Count;j++)
                {
                    GuiSession session = con.Children.ElementAt(j) as GuiSession;
                    if(session.Info.SystemName.ToLower() == BoxName.ToLower())
                    {
                        _sapGuiSession = session;
                        break;
                    }
                }
                if (_sapGuiSession != null)
                {
                    _sapGuiConnection = con;
                    break;
                }
                    
            }
        }

        public void SetSession()
        {
            _sapGuiApplication = SAPTestHelper.GetSAPGuiApp();
            int index = _sapGuiApplication.Connections.Count - 1;
            if (index < 0)
            {
                throw new Exception("No SAP GUI Connections found");
            }

            _sapGuiConnection = _sapGuiApplication.Children.ElementAt(index) as GuiConnection;
            index = _sapGuiConnection.Sessions.Count - 1;
            if (index < 0)
            {
                throw new Exception("No SAP GUI Session Found");
            }
            _sapGuiSession = _sapGuiConnection.Children.ElementAt(index) as GuiSession;

            hookSessionEvent();
        }

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

        public void TakeScreenShot(string filePath)
        {
            byte[] name = (byte[])SAPTestHelper.Current.GetElementById<GuiFrameWindow>("wnd[0]").HardCopyToMemory();
            MemoryStream ms = new MemoryStream(name);
            ms.WriteTo(new FileStream(filePath, FileMode.Create));
        }

        public void CloseConnection()
        {
            _sapGuiConnection.CloseSession(SAPGuiSession.Id);
            _sapGuiConnection.CloseConnection();
        }

        void _sapGuiSession_Destroy(GuiSession Session)
        {
            _sapGuiSession = null;
        }

        private void hookSessionEvent()
        {
            _sapGuiSession.Destroy -= _sapGuiSession_Destroy;
            _sapGuiSession.Destroy += _sapGuiSession_Destroy;
        }
    }
}

﻿//using SAPFEWSELib;
//using SapROTWr;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using SAPTestRunTime;

//namespace SAPGuiAutomationLib
//{
//    public sealed class SAPTestHelper
//    {
//        private static object _lockObj = new object();
//        private static SAPTestHelper _instance;
//        private GuiApplication _sapGuiApplication;
//        private GuiSession _sapGuiSession;
//        private GuiConnection _sapGuiConnection;
//        private Assembly _sapGuiApiAssembly;
//        private static string _prefix = "SAPFEWSELib.";
//        private Action<RecordStep> _stepAction;
//        private Action<WrapComp> _hitAction;

//        public GuiApplication SAPGuiApplication { get { return _sapGuiApplication; } }
//        public GuiSession SAPGuiSession { get { return _sapGuiSession; } }
//        public GuiConnection SAPGuiConnection { get { return _sapGuiConnection; } }

//        public Assembly SAPGuiApiAssembly { get { return _sapGuiApiAssembly; } }

//        private static GuiApplication getSAPGuiApp(CSapROTWrapper sapROTWrapper, int secondsOfTimeout)
//        {
//            object SapGuilRot = sapROTWrapper.GetROTEntry("SAPGUI");
//            if (secondsOfTimeout < 0)
//            {
//                throw new TimeoutException(string.Format("Can get sap script engine in {0} seconds", secondsOfTimeout));
//            }
//            else
//            {
//                if (SapGuilRot == null)
//                {
//                    Thread.Sleep(1000);
//                    return getSAPGuiApp(sapROTWrapper, secondsOfTimeout - 1);
//                }
//                else
//                {
//                    object engine = SapGuilRot.GetType().InvokeMember("GetSCriptingEngine", System.Reflection.BindingFlags.InvokeMethod, null, SapGuilRot, null);
//                    if (engine == null)
//                        throw new NullReferenceException("No SAP GUI application found");
//                    return engine as GuiApplication;
//                }
//            }
//        }

//        public static GuiApplication GetSAPGuiApp(int secondsOfTimeout = 10)
//        {
//            SapROTWr.CSapROTWrapper sapROTWrapper = new SapROTWr.CSapROTWrapper();
//            return getSAPGuiApp(sapROTWrapper, secondsOfTimeout);
//        }

//        public void CloseConnection()
//        {
//            _sapGuiConnection.CloseSession(SAPGuiSession.Id);
//            _sapGuiConnection.CloseConnection();
//        }

//        public static SAPTestHelper Current
//        {
//            get
//            {
//                if (_instance == null)
//                {
//                    lock (_lockObj)
//                    {
//                        if (_instance == null)
//                            _instance = new SAPTestHelper();
//                    }
//                }
//                return _instance;
//            }
//        }

//        public void SetSession(SAPLogon logon)
//        {
//            SetSession(logon.SapGuiApplication, logon.SapGuiConnection, logon.SapGuiSession);
//        }

//        public void SetSession(GuiApplication application, GuiConnection connection, GuiSession session)
//        {
//            if (_sapGuiSession != null)
//                SetVisualMode(false);

//            this._sapGuiApplication = application;
//            this._sapGuiConnection = connection;
//            this._sapGuiSession = session;

//            hookSessionEvent();
//        }

//        private void hookSessionEvent()
//        {
//            _sapGuiSession.Hit -= _sapGuiSession_Hit;
//            _sapGuiSession.Change -= _sapGuiSession_Change;
//            _sapGuiSession.Destroy -= _sapGuiSession_Destroy;

//            _sapGuiSession.Hit += _sapGuiSession_Hit;
//            _sapGuiSession.Change += _sapGuiSession_Change;
//            _sapGuiSession.Destroy += _sapGuiSession_Destroy;
//        }

//        void _sapGuiSession_Destroy(GuiSession Session)
//        {
//            _sapGuiSession = null;
//        }

//        public void SetSession()
//        {
            
//            if(_sapGuiSession != null)
//                SetVisualMode(false);

//            _sapGuiApplication = SAPTestHelper.GetSAPGuiApp();
//            int index = _sapGuiApplication.Connections.Count - 1;
//            if (index < 0)
//            {
//                throw new Exception("No SAP GUI Connections found");
//            }

//            _sapGuiConnection = _sapGuiApplication.Children.ElementAt(index) as GuiConnection;
//            index = _sapGuiConnection.Sessions.Count - 1;
//            if (index < 0)
//            {
//                throw new Exception("No SAP GUI Session Found");
//            }
//            _sapGuiSession = _sapGuiConnection.Children.ElementAt(index) as GuiSession;
            
//            hookSessionEvent();
//        }

//        public T GetElementById<T>(string id) where T : class
//        {
//            var component = GetElementById(id);
//            T element = component as T;
//            return element;
//        }

//        public GuiComponent GetElementById(string id)
//        {
//            GuiComponent component = _sapGuiSession.FindById(id);
//            return component;
//        }

//        public GuiComponent TryGetElementById(string id)
//        {
//            try
//            {
//                return GetElementById(id);
//            }
//            catch
//            {
//                return null;
//            }
//        }

//        public T TryGetElementById<T>(string id) where T : class
//        {
//            try
//            {
//                return GetElementById<T>(id);
//            }
//            catch
//            {
//                return null;
//            }
//        }

//        /// <summary>
//        /// This Method will find the element until the timeout, if secondsOfTimeout set to less than 0, this method will continue to find element without timeout
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="id"></param>
//        /// <param name="secondsOfFrequence"></param>
//        /// <param name="secondsOfTimeout"></param>
//        /// <returns></returns>
//        public T GetElementUntil<T>(string id, int secondsOfFrequence, int secondsOfTimeout) where T : class
//        {
//            T comp = TryGetElementById<T>(id);
//            if (comp == null)
//            {
//                Thread.Sleep(secondsOfFrequence * 1000);
//                if (secondsOfTimeout < 0)
//                {
//                    return GetElementUntil<T>(id, secondsOfFrequence, secondsOfTimeout);
//                }
//                else
//                {
//                    if (secondsOfFrequence == 0)
//                    {
//                        return null;
//                    }
//                    else
//                    {
//                        return GetElementUntil<T>(id, secondsOfFrequence, secondsOfTimeout - 1);
//                    }
//                }
//            }

//            return comp;

//        }

//        public void TakeScreenShot(string filePath)
//        {
//            byte[] name = (byte[])SAPTestHelper.Current.GetElementById<GuiFrameWindow>("wnd[0]").HardCopyToMemory();
//            MemoryStream ms = new MemoryStream(name);
//            ms.WriteTo(new FileStream(filePath, FileMode.Create));
//        }

//        /// <summary>
//        /// This method will load sap interoperate library, for default the library name should be "Interop.SAPFEWSELib.dll"
//        /// </summary>
//        /// <param name="RawSAPLib">The Full path of the sap library</param>
//        public void SetSAPApiAssembly(string RawSAPLib)
//        {
//            _sapGuiApiAssembly = Assembly.LoadFile(RawSAPLib);
//        }

//        public void SetSAPApiAssembly()
//        {
//            var file = Path.Combine(Environment.CurrentDirectory, "Interop.SAPFEWSELib.dll");
//            Stream stm = Assembly.GetExecutingAssembly().GetManifestResourceStream("SAPGuiAutomationLib.Resources.Interop.SAPFEWSELib.dll");
//            byte[] bs = new byte[(int)stm.Length];
//            stm.Read(bs, 0, (int)stm.Length);
//            stm.Close();
//            _sapGuiApiAssembly = Assembly.Load(bs);
//        }

        
//        public IEnumerable<T> GetSAPTypeInfoes<T>(string typeName,object obj, Func<Type,object,IEnumerable<T>> infoMethod) where T:class
//        {
//            if (_sapGuiApiAssembly == null)
//                throw new ArgumentNullException("No SAP Library found, please mark sure you load the lib named Interop.SAPFEWSELib.dll");

//            Type t = _sapGuiApiAssembly.GetType(_prefix + typeName).GetInterfaces()[0];
//            return infoMethod(t, obj);
//        }

//        public IEnumerable<T> GetSAPTypeInfoes<T>(string typeName, Func<Type, IEnumerable<T>> infoFunc) where T : class
//        {
//            if (_sapGuiApiAssembly == null)
//                throw new ArgumentNullException("No SAP Library found, please mark sure you load the lib named Interop.SAPFEWSELib.dll");

//            Type t = _sapGuiApiAssembly.GetType(_prefix + typeName).GetInterfaces()[0];
//            return infoFunc(t);

//        }

//        public IEnumerable<T> GetSAPTypeInfoes<T>(GuiComponent Component, Func<Type, IEnumerable<T>> infoFunc) where T : class
//        {
//            return GetSAPTypeInfoes<T>(Component.Type, infoFunc);
//        }

//        public void LoopSAPComponents<T>(WrapComp CurrentComponent, T item, int childrenIndex, int MaxChildrenCount, Func<WrapComp, T, int, T> Method) where T : class
//        {
//            var count = 0;
//            if (CurrentComponent.Comp is GuiContainer)
//            {
//                count = ((GuiContainer)CurrentComponent.Comp).Children.Count;
//            }
//            if (CurrentComponent.Comp is GuiVContainer)
//            {
//                count = ((GuiVContainer)CurrentComponent.Comp).Children.Count;
//            }
//            T newItem = Method(CurrentComponent, item, count);
//            if (CurrentComponent.Comp is GuiVContainer)
//            {
//                int startIndex = childrenIndex;
//                int endIndex = count;

//                if (count > MaxChildrenCount)
//                {
//                    endIndex = (childrenIndex + MaxChildrenCount > count) ? count : childrenIndex + MaxChildrenCount;
//                }
//                for (int i = startIndex; i < endIndex; i++)
//                {
//                    GuiComponent comp = ((GuiVContainer)CurrentComponent.Comp).Children.ElementAt(i);
//                    WrapComp wrComp = new WrapComp() { Comp = comp };
//                    LoopSAPComponents(wrComp, newItem, 0, MaxChildrenCount, Method);
//                }
//            }
//            if (CurrentComponent.Comp is GuiContainer)
//            {
//                int startIndex = 0;
//                int endIndex = count;

//                if (count > MaxChildrenCount)
//                {
//                    endIndex = (childrenIndex + MaxChildrenCount > count) ? count : childrenIndex + MaxChildrenCount;

//                }

//                for (int i = startIndex; i < endIndex; i++)
//                {
//                    GuiComponent comp = ((GuiContainer)CurrentComponent.Comp).Children.ElementAt(i);
//                    WrapComp wrComp = new WrapComp() { Comp = comp };
//                    LoopSAPComponents(wrComp, newItem, 0, MaxChildrenCount, Method);
//                }
//            }
//        }

//        public void LoopSAPComponents<T>(WrapComp CurrentComponent, T item, Func<WrapComp, T, T> Method) where T : class
//        {
//            T newItem = Method(CurrentComponent, item);

//            if (CurrentComponent.Comp is GuiVContainer && !(CurrentComponent.Comp is GuiTableControl))
//            {
//                for (int i = 0; i < ((GuiVContainer)CurrentComponent.Comp).Children.Count; i++)
//                {
//                    GuiComponent comp = ((GuiVContainer)CurrentComponent.Comp).Children.ElementAt(i);
//                    WrapComp wrComp = new WrapComp() { Comp = comp };
//                    LoopSAPComponents(wrComp, newItem, Method);
//                }
//            }
//            if (CurrentComponent.Comp is GuiContainer && !(CurrentComponent.Comp is GuiTableControl))
//            {
//                for (int i = 0; i < ((GuiContainer)CurrentComponent.Comp).Children.Count; i++)
//                {
//                    GuiComponent comp = ((GuiContainer)CurrentComponent.Comp).Children.ElementAt(i);
//                    WrapComp wrComp = new WrapComp() { Comp = comp };
//                    LoopSAPComponents(wrComp, newItem, Method);
//                }
//            }
//        }

        

//        public void StartRecording(Action<RecordStep> StepAction)
//        {
//            checkSapSession();
//            _stepAction = StepAction;
//            _sapGuiSession.Record = true;
//        }

//        public void Spy(Action<WrapComp> HitAction)
//        {
//            checkSapSession();
//            _hitAction = HitAction;
//        }

//        public void SetVisualMode(bool on)
//        {
//            for (int i = 0; i < _sapGuiSession.Children.Count; i++)
//            {
//                var fWin = _sapGuiSession.Children.ElementAt(i) as GuiFrameWindow;
//                if (fWin != null)
//                {
//                    fWin.ElementVisualizationMode = on;
//                }
//            }
//        }

//        void _sapGuiSession_Hit(GuiSession Session, GuiComponent Component, string InnerObject)
//        {
//            WrapComp comp = new WrapComp();
//            comp.Comp = Component;
//            _hitAction(comp);
//        }

//        public void StopRecording()
//        {
//            checkSapSession();
//            _sapGuiSession.Record = false;
//        }

//        private void checkSapSession()
//        {
//            if (_sapGuiSession == null)
//                throw new Exception("No SAP GUI Session Found");
//        }

//        void _sapGuiSession_Change(GuiSession Session, GuiComponent Component, object CommandArray)
//        {
//            SapCompInfo cpInfo = new SapCompInfo();

//            cpInfo.Id = Component.Id;
//            cpInfo.Name = Component.Name;
//            cpInfo.Type = Component.Type;

//            RecordStep step = new RecordStep();
//            step.CompInfo = cpInfo;

//            object[] objs = CommandArray as object[];

//            objs = objs[0] as object[];
//            switch (objs[0].ToString().ToLower())
//            {
//                case "m":
//                    step.Action = BindingFlags.InvokeMethod;
//                    break;
//                case "sp":
//                    step.Action = BindingFlags.SetProperty;
//                    break;
//            }

//            step.ActionName = objs[1].ToString();

//            var count = objs.Count();

//            if (count > 2)
//            {
//                step.ActionParams = new object[count - 2];
//                for (int i = 2; i < count; i++)
//                {
//                    step.ActionParams[i - 2] = objs[i];
//                }
//            }

//            _stepAction(step);
//        }


//        public object RunAction(RecordStep step)
//        {
//            GuiComponent comp = _sapGuiSession.TryGetComponentById<GuiComponent>(step.CompInfo.Id);
//            if (comp == null)
//                throw new Exception(string.Format("Can't find component using id {0}",step.CompInfo.Id));
//            string typeName = _prefix + comp.Type;
//            Type t = _sapGuiApiAssembly.GetType(typeName);
//            if (t == null)
//                throw new Exception(string.Format("Can't find type {0}", typeName));
//            return t.InvokeMember(step.ActionName, step.Action, null, comp, step.ActionParams);
//        }

//        public object InvokeMember(string SAPGuiId, string MemberName, BindingFlags Flags, object[] parameters)
//        {
//            GuiComponent comp = SAPTestHelper.Current.TryGetElementById(SAPGuiId);
//            if (comp == null)
//                throw new Exception("Can't find component");
//            string typeName = _prefix + comp.Type;
//            Type t = _sapGuiApiAssembly.GetType(typeName);
//            if (t == null)
//                throw new Exception(string.Format("Can't find type {0}", typeName));
//            return t.InvokeMember(MemberName, Flags, null, comp, parameters);
//        }

//        public object InvokeMember(GuiComponent Obj, string MemberName, BindingFlags Flags, object[] parameters)
//        {
//            Type t = _sapGuiApiAssembly.GetType(_prefix + Obj.Type, true);
//            return t.InvokeMember(MemberName, Flags, null, Obj, parameters);
//        }
//    }
//}
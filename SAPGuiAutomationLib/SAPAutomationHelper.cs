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
using System.Threading.Tasks;


namespace SAPGuiAutomationLib
{
    public sealed class SAPAutomationHelper
    {
        private static object _lockObj = new object();
        private static SAPAutomationHelper _instance;
        private bool _isFindById = true;

        private GuiSession _sapGuiSession;

        private Assembly _sapGuiApiAssembly;
        private static string _prefix = "SAPFEWSELib.";
        private Action<RecordStep> _stepAction;
        private Action<dynamic> _hitAction;
        private Action<string> _failRequestAction;
        private Action<dynamic> _sessionDestroy;


        public GuiSession SAPGuiSession { get { return _sapGuiSession; } }

        public Assembly SAPGuiApiAssembly { get { return _sapGuiApiAssembly; } }

        public static SAPAutomationHelper Current
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                            _instance = new SAPAutomationHelper();
                    }
                }
                return _instance;
            }
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

        public void SetSession(GuiSession Session, Action<dynamic> SessionDestroy = null)
        {
            if (_sapGuiSession != null && _sapGuiSession.Record == true)
                _sapGuiSession.Record = false;
            _sapGuiSession = Session;
            hookSessionEvent();
            _sessionDestroy = SessionDestroy;
        }

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

        private void hookSessionEvent()
        {
            _sapGuiSession.Hit -= _sapGuiSession_Hit;
            _sapGuiSession.Change -= _sapGuiSession_Change;
            _sapGuiSession.Destroy -= _sapGuiSession_Destroy;
            _sapGuiSession.EndRequest -= _sapGuiSession_EndRequest;

            _sapGuiSession.EndRequest += _sapGuiSession_EndRequest;
            _sapGuiSession.Hit += _sapGuiSession_Hit;
            _sapGuiSession.Change += _sapGuiSession_Change;
            _sapGuiSession.Destroy += _sapGuiSession_Destroy;
        }

        void _sapGuiSession_EndRequest(GuiSession Session)
        {
            var status = this.GetSAPComponentById<GuiStatusbar>("wnd0]/sbar");
            if (status != null && status.MessageType == "E" && _failRequestAction != null)
            {
                _failRequestAction(status.Text);
            }

        }

        void _sapGuiSession_Destroy(GuiSession Session)
        {
            if (_sessionDestroy != null)
                _sessionDestroy(Session);
            _sapGuiSession = null;
        }

        /// <summary>
        /// This method will load sap interoperate library, for default the library name should be "Interop.SAPFEWSELib.dll"
        /// </summary>
        /// <param name="RawSAPLib">The Full path of the sap library</param>
        public void SetSAPApiAssembly(string RawSAPLib)
        {
            _sapGuiApiAssembly = Assembly.LoadFile(RawSAPLib);
        }

        public void SetSAPApiAssembly()
        {
            var file = Path.Combine(Environment.CurrentDirectory, "Interop.SAPFEWSELib.dll");
            Stream stm = Assembly.GetExecutingAssembly().GetManifestResourceStream("SAPGuiAutomationLib.Resources.Interop.SAPFEWSELib.dll");
            byte[] bs = new byte[(int)stm.Length];
            stm.Read(bs, 0, (int)stm.Length);
            stm.Close();
            _sapGuiApiAssembly = Assembly.Load(bs);
        }

        public Type GetSAPTypeInfo(string typeName)
        {
            if (_sapGuiApiAssembly == null)
                throw new ArgumentNullException("No SAP Library found, please mark sure you load the lib named Interop.SAPFEWSELib.dll");
            Type t = _sapGuiApiAssembly.GetType(_prefix + typeName).GetInterfaces()[0];
            return t;
        }

        [Obsolete]
        public T GetSAPTypeInfo<T>(string typeName, Func<Type, T> infoMethod) where T : class
        {
            if (_sapGuiApiAssembly == null)
                throw new ArgumentNullException("No SAP Library found, please mark sure you load the lib named Interop.SAPFEWSELib.dll");
            Type t = _sapGuiApiAssembly.GetType(_prefix + typeName).GetInterfaces()[0];
            return infoMethod(t);
        }

        [Obsolete]
        public IEnumerable<T> GetSAPTypeInfoes<T>(string typeName, object obj, Func<Type, object, IEnumerable<T>> infoMethod) where T : class
        {
            if (_sapGuiApiAssembly == null)
                throw new ArgumentNullException("No SAP Library found, please mark sure you load the lib named Interop.SAPFEWSELib.dll");

            Type t = _sapGuiApiAssembly.GetType(_prefix + typeName).GetInterfaces()[0];
            return infoMethod(t, obj);
        }

        [Obsolete]
        public IEnumerable<T> GetSAPTypeInfoes<T>(string typeName, Func<Type, IEnumerable<T>> infoFunc) where T : class
        {
            if (_sapGuiApiAssembly == null)
                throw new ArgumentNullException("No SAP Library found, please mark sure you load the lib named Interop.SAPFEWSELib.dll");

            Type t = _sapGuiApiAssembly.GetType(_prefix + typeName).GetInterfaces()[0];
            return infoFunc(t);

        }

        [Obsolete]
        public IEnumerable<T> GetSAPTypeInfoes<T>(GuiComponent Component, Func<Type, IEnumerable<T>> infoFunc) where T : class
        {
            return GetSAPTypeInfoes<T>(Component.Type, infoFunc);
        }

        /// <summary>
        /// CurrentComponent is type of GuiComponent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="CurrentComponent"></param>
        /// <param name="item"></param>
        /// <param name="childrenIndex"></param>
        /// <param name="MaxChildrenCount"></param>
        /// <param name="Method"></param>
        public void LoopSAPComponents<T>(dynamic CurrentComponent, T item, int childrenIndex, int MaxChildrenCount, Func<dynamic, T, int, T> Method) where T : class
        {
            var count = 0;
            if (CurrentComponent is GuiContainer)
            {
                count = ((GuiContainer)CurrentComponent).Children.Count;
            }
            if (CurrentComponent is GuiVContainer)
            {
                count = ((GuiVContainer)CurrentComponent).Children.Count;
            }
            T newItem = Method(CurrentComponent, item, count);
            if (CurrentComponent is GuiVContainer)
            {
                int startIndex = childrenIndex;
                int endIndex = count;

                if (count > MaxChildrenCount)
                {
                    endIndex = (childrenIndex + MaxChildrenCount > count) ? count : childrenIndex + MaxChildrenCount;
                }
                for (int i = startIndex; i < endIndex; i++)
                {
                    GuiComponent comp = ((GuiVContainer)CurrentComponent).Children.ElementAt(i);

                    LoopSAPComponents(comp, newItem, 0, MaxChildrenCount, Method);
                }
            }
            if (CurrentComponent is GuiContainer)
            {
                int startIndex = 0;
                int endIndex = count;

                if (count > MaxChildrenCount)
                {
                    endIndex = (childrenIndex + MaxChildrenCount > count) ? count : childrenIndex + MaxChildrenCount;

                }

                for (int i = startIndex; i < endIndex; i++)
                {
                    GuiComponent comp = ((GuiContainer)CurrentComponent).Children.ElementAt(i);

                    LoopSAPComponents(comp, newItem, 0, MaxChildrenCount, Method);
                }
            }
        }

        /// <summary>
        /// CurrentComponent is type of GuiComponent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="CurrentComponent"></param>
        /// <param name="item"></param>
        /// <param name="Method"></param>
        public void LoopSAPComponents<T>(dynamic CurrentComponent, T item, Func<dynamic, T, T> Method) where T : class
        {
            T newItem = Method(CurrentComponent, item);

            if (CurrentComponent is GuiVContainer && !(CurrentComponent is GuiTableControl))
            {
                for (int i = 0; i < ((GuiVContainer)CurrentComponent).Children.Count; i++)
                {
                    GuiComponent comp = ((GuiVContainer)CurrentComponent).Children.ElementAt(i);
                    LoopSAPComponents(comp, newItem, Method);
                }
            }
            if (CurrentComponent is GuiContainer && !(CurrentComponent is GuiTableControl))
            {
                for (int i = 0; i < ((GuiContainer)CurrentComponent).Children.Count; i++)
                {
                    GuiComponent comp = ((GuiContainer)CurrentComponent).Children.ElementAt(i);
                    LoopSAPComponents(comp, newItem, Method);
                }
            }
        }



        public void GetFailRequest(Action<string> FailRequest)
        {
            this._failRequestAction = FailRequest;
        }

        public void StartRecording(Action<RecordStep> StepAction, bool isFindById = true)
        {
            checkSapSession();
            _stepAction = StepAction;
            _isFindById = isFindById;
            _sapGuiSession.Record = true;
        }

        public void Spy(Action<dynamic> HitAction)
        {
            checkSapSession();
            _hitAction = HitAction;
        }

        public void SetVisualMode(bool on)
        {
            for (int i = 0; i < _sapGuiSession.Children.Count; i++)
            {
                var fWin = _sapGuiSession.Children.ElementAt(i) as GuiFrameWindow;
                if (fWin != null)
                {
                    fWin.ElementVisualizationMode = on;
                }
            }
        }

        public T GetSAPComponentById<T>(string id) where T : class
        {
            try
            {
                return _sapGuiSession.FindById(id) as T;
            }
            catch
            {
                return null;
            }
        }

        void _sapGuiSession_Hit(GuiSession Session, GuiComponent Component, string InnerObject)
        {
            _hitAction(Component);
        }

        public void StopRecording()
        {
            checkSapSession();
            _sapGuiSession.Record = false;
        }

        private void checkSapSession()
        {
            if (_sapGuiSession == null)
                throw new Exception("No SAP GUI Session Found");
        }

        void _sapGuiSession_Change(GuiSession Session, GuiComponent Component, object CommandArray)
        {
            if (_stepAction == null)
                return;
            SapCompInfo cpInfo = new SapCompInfo();
            cpInfo.Id = Component.Id;
            cpInfo.Name = Component.Name;
            cpInfo.Type = Component.GetDetailType();

            if (Component is GuiVComponent)
            {
                var vc = Component as GuiVComponent;
                try
                {
                    cpInfo.Tip = vc.DefaultTooltip == "" ? vc.Tooltip : vc.DefaultTooltip;
                }
                catch
                {

                }
            }


            if (_isFindById)
                cpInfo.FindMethod = Component.FindByIdCode();
            else
                cpInfo.FindMethod = Component.FindByNameCode();

            RecordStep step = new RecordStep();
            step.CompInfo = cpInfo;

            object[] objs = CommandArray as object[];

            objs = objs[0] as object[];
            switch (objs[0].ToString().ToLower())
            {
                case "m":
                    step.Action = BindingFlags.InvokeMethod;
                    break;
                case "sp":
                    step.Action = BindingFlags.SetProperty;
                    break;
            }

            var action = objs[1].ToString();
            upperFirstChar(ref action);

            step.ActionName = action;

            var count = objs.Count();

            if (count > 2)
            {
                step.ActionParams = new List<SAPDataParameter>();

                if (step.Action == BindingFlags.InvokeMethod)
                {
                    MethodInfo method = GetSAPTypeInfo(step.CompInfo.Type).GetMethod(step.ActionName);
                    //MethodInfo method = SAPAutomationHelper.Current.GetSAPTypeInfo<MethodInfo>(step.CompInfo.Type, t => t.GetMethod(step.ActionName));
                    int index = 2;
                    foreach (var pInfo in method.GetParameters())
                    {
                        var para = new SAPDataParameter();
                        para.Type = pInfo.ParameterType;
                        para.Comment = pInfo.Name;
                        para.Value = objs[index];
                        para.Name = pInfo.Name;
                        step.ActionParams.Add(para);
                        index++;
                    }
                }
                else
                {
                    for (int i = 2; i < count; i++)
                    {
                        var para = new SAPDataParameter();
                        para.Type = objs[i].GetType();
                        para.Value = objs[i];
                        para.Comment = step.CompInfo.Tip;

                        step.ActionParams.Add(para);
                    }
                }

            }

            _stepAction(step);
        }


        public object RunAction(RecordStep step)
        {
            GuiComponent comp = GetSAPComponentById<GuiComponent>(step.CompInfo.Id);

            if (comp == null)
                throw new Exception(string.Format("Can't find component using id {0}", step.CompInfo.Id));
            string typeName = _prefix + comp.Type;
            Type t = _sapGuiApiAssembly.GetType(typeName);
            if (t == null)
                throw new Exception(string.Format("Can't find type {0}", typeName));
            return t.InvokeMember(step.ActionName, step.Action, null, comp, step.ActionParams == null ? null : step.ActionParams.Select(a => a.Value).ToArray());

        }

        public object InvokeMember(string SAPGuiId, string MemberName, BindingFlags Flags, object[] parameters)
        {

            GuiComponent comp = GetSAPComponentById<GuiComponent>(SAPGuiId);

            if (comp == null)
                throw new Exception("Can't find component");
            string typeName = _prefix + comp.Type;
            Type t = _sapGuiApiAssembly.GetType(typeName);
            if (t == null)
                throw new Exception(string.Format("Can't find type {0}", typeName));
            return t.InvokeMember(MemberName, Flags, null, comp, parameters);
        }

        public object InvokeMember(GuiComponent Obj, string MemberName, BindingFlags Flags, object[] parameters)
        {
            Type t = _sapGuiApiAssembly.GetType(_prefix + Obj.Type, true);
            return t.InvokeMember(MemberName, Flags, null, Obj, parameters);
        }

        private void upperFirstChar(ref string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                s = char.ToUpper(s[0]) + s.Substring(1);
            }
        }

        #region Remove
        //public void LoopSAPComponents<T>(WrapComp CurrentComponent, T item, int childrenIndex, int MaxChildrenCount, Func<WrapComp, T, int, T> Method) where T : class
        //{
        //    var count = 0;
        //    if (CurrentComponent.Comp is GuiContainer)
        //    {
        //        count = ((GuiContainer)CurrentComponent.Comp).Children.Count;
        //    }
        //    if (CurrentComponent.Comp is GuiVContainer)
        //    {
        //        count = ((GuiVContainer)CurrentComponent.Comp).Children.Count;
        //    }
        //    T newItem = Method(CurrentComponent, item, count);
        //    if (CurrentComponent.Comp is GuiVContainer)
        //    {
        //        int startIndex = childrenIndex;
        //        int endIndex = count;

        //        if (count > MaxChildrenCount)
        //        {
        //            endIndex = (childrenIndex + MaxChildrenCount > count) ? count : childrenIndex + MaxChildrenCount;
        //        }
        //        for (int i = startIndex; i < endIndex; i++)
        //        {
        //            GuiComponent comp = ((GuiVContainer)CurrentComponent.Comp).Children.ElementAt(i);
        //            WrapComp wrComp = new WrapComp() { Comp = comp };
        //            LoopSAPComponents(wrComp, newItem, 0, MaxChildrenCount, Method);
        //        }
        //    }
        //    if (CurrentComponent.Comp is GuiContainer)
        //    {
        //        int startIndex = 0;
        //        int endIndex = count;

        //        if (count > MaxChildrenCount)
        //        {
        //            endIndex = (childrenIndex + MaxChildrenCount > count) ? count : childrenIndex + MaxChildrenCount;

        //        }

        //        for (int i = startIndex; i < endIndex; i++)
        //        {
        //            GuiComponent comp = ((GuiContainer)CurrentComponent.Comp).Children.ElementAt(i);
        //            WrapComp wrComp = new WrapComp() { Comp = comp };
        //            LoopSAPComponents(wrComp, newItem, 0, MaxChildrenCount, Method);
        //        }
        //    }
        //}

        //public void LoopSAPComponents<T>(WrapComp CurrentComponent, T item, Func<WrapComp, T, T> Method) where T : class
        //{
        //    T newItem = Method(CurrentComponent, item);

        //    if (CurrentComponent.Comp is GuiVContainer && !(CurrentComponent.Comp is GuiTableControl))
        //    {
        //        for (int i = 0; i < ((GuiVContainer)CurrentComponent.Comp).Children.Count; i++)
        //        {
        //            GuiComponent comp = ((GuiVContainer)CurrentComponent.Comp).Children.ElementAt(i);
        //            WrapComp wrComp = new WrapComp() { Comp = comp };
        //            LoopSAPComponents(wrComp, newItem, Method);
        //        }
        //    }
        //    if (CurrentComponent.Comp is GuiContainer && !(CurrentComponent.Comp is GuiTableControl))
        //    {
        //        for (int i = 0; i < ((GuiContainer)CurrentComponent.Comp).Children.Count; i++)
        //        {
        //            GuiComponent comp = ((GuiContainer)CurrentComponent.Comp).Children.ElementAt(i);
        //            WrapComp wrComp = new WrapComp() { Comp = comp };
        //            LoopSAPComponents(wrComp, newItem, Method);
        //        }
        //    }
        //}
        #endregion
    }
}

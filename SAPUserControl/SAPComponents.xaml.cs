using SAPAutomation;
using SAPFEWSELib;
using SAPGuiAutomationLib;
using SAPUserControl.ViewModel;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace SAPUserControl
{
    public delegate void OnSpyHandler(object sender, EventArgs e);

    /// <summary>
    /// SAPComponents.xaml 的交互逻辑
    /// </summary>
    public partial class SAPComponents : UserControl
    {
        public event OnSpyHandler BeforeSpy;
        public event OnSpyHandler AfterSpy;
        private GuiSession _session;
        private int _maxCount;
        private GuiVComponent _lastHighlight;
        XmlDocument xDoc;
        private bool _isExpand = false;

        public SAPComponents()
        {
            InitializeComponent();
            SAPAutomationHelper.Current.OnSetSession += Current_OnSetSession;
            _maxCount = 20;
            this.IsEnabled = false;
        }

        private void Current_OnSetSession(object sender, GuiSession session)
        {
            _session = session;
            this.IsEnabled = true;
            clearContent();
            _session.Destroy -= _session_Destroy;
            _session.Destroy += _session_Destroy;
        }

       

        void clearContent()
        {
            lv_Methods.DataContext = null;
            lv_Props.DataContext = null;
            tv_Elements.DataContext = null;
        }

        void _session_Destroy(GuiSession Session)
        {
            this.Dispatcher.BeginInvoke(new Action(() => { this.IsEnabled = false; }));

        }

        private async void btn_ShowAll_Click(object sender, RoutedEventArgs e)
        {
            _lastHighlight = null;

            try
            {
                _isExpand = false;
                xDoc = new XmlDocument();
                XmlElement root = xDoc.CreateElement("Node");
                root.SetAttribute("name", "root");
                Task loopNodeTask = new Task(() =>
                {
                    var comp = SAPAutomationHelper.Current.SAPGuiSession as GuiComponent;
                    //WrapComp comp = new WrapComp() { Comp = SAPAutomationHelper.Current.SAPGuiSession as GuiComponent };



                    SAPAutomationHelper.Current.LoopSAPComponents<XmlElement>(comp, root, 0, _maxCount, addNode);

                    xDoc.AppendChild(root.FirstChild);
                });
                //setWorking(true);
                loopNodeTask.Start();
                await loopNodeTask;

                tv_Elements.DataContext = xDoc;
                //setWorking(false);
            }
            catch (Exception ex)
            {
                //setWorking(false);
                MessageBox.Show(ex.Message);
            }
        }

        private XmlElement addNode(dynamic Comp, XmlElement item, int count)
        {
            var comp = Comp as GuiComponent;
            XmlElement newItem = xDoc.CreateElement("Node");
            newItem.SetAttribute("name", comp.Name);
            newItem.SetAttribute("id", comp.Id);
            newItem.SetAttribute("type", SAPAutomationExtension.GetDetailType(comp));
            newItem.SetAttribute("num", count.ToString());
            newItem.SetAttribute("isExpand", _isExpand.ToString());
            if (comp is GuiVComponent)
            {
                try
                {
                    newItem.SetAttribute("text", ((GuiVComponent)comp).Tooltip);
                }
                catch { }
            }
            item.AppendChild(newItem);
            return newItem;
        }

        private void tv_Elements_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {

                XmlElement element = tv_Elements.SelectedItem as XmlElement;



                GuiVComponent comp = SAPAutomationHelper.Current.GetSAPComponentById<GuiVComponent>(element.GetAttribute("id"));
                if (_lastHighlight != null)
                {
                    _lastHighlight.Visualize(false);

                }
                _lastHighlight = comp;
                _lastHighlight.Visualize(true);
            }
            catch { }
        }

        private void mi_Load_Click(object sender, RoutedEventArgs e)
        {
            //object obj = sender;
            if (tv_Elements.SelectedItem != null)
            {
                XmlElement root = tv_Elements.SelectedItem as XmlElement;
                //XmlElement rootCopy = root.Clone() as XmlElement;
                string id = root.GetAttribute("id");
                GuiComponent cp = SAPAutomationHelper.Current.GetSAPComponentById<GuiComponent>(id);

                if (root.ChildNodes.Count == 1)
                {
                    root.RemoveChild(root.FirstChild);
                }

                SAPAutomationHelper.Current.LoopSAPComponents<XmlElement>(cp, root, root.ChildNodes.Count, _maxCount, addNode);
                root = tv_Elements.SelectedItem as XmlElement;
                var lastNode = root.LastChild.Clone();
                root.RemoveChild(root.LastChild);
                int count = lastNode.ChildNodes.Count;
                for (int i = 0; i < count; i++)
                {
                    root.AppendChild(lastNode.ChildNodes[0]);
                }

            }
        }

        private void tv_Elements_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (tv_Elements.SelectedItem != null)
            {
                XmlElement root = tv_Elements.SelectedItem as XmlElement;
                int count = int.Parse(root.GetAttribute("num"));
                if (count > root.ChildNodes.Count)
                {
                    mi_Load.IsEnabled = true;
                }
                else
                {
                    mi_Load.IsEnabled = false;
                }
            }
            else
                mi_Load.IsEnabled = false;


        }

        private void tv_Elements_MouseMove(object sender, MouseEventArgs e)
        {
            if (tv_Elements.SelectedItem != null && e.LeftButton == MouseButtonState.Pressed && e.OriginalSource.GetType() == typeof(TextBlock))
            {

                XmlElement element = tv_Elements.SelectedItem as XmlElement;
                SapCompInfo ci = new SapCompInfo();
                ci.Id = element.GetAttribute("id");
                ci.Type = element.GetAttribute("type");
                ci.Name = element.GetAttribute("name");
                ci.FindMethod = ci.FindByNameCode();

                CodeDomProvider provider = CodeDomProvider.CreateProvider("c#");
                CodeGeneratorOptions options = new CodeGeneratorOptions();
                options.BracingStyle = "C";
                StringBuilder sb = new StringBuilder();

                try
                {
                    using (TextWriter sourceWriter = new StringWriter(sb))
                    {
                        provider.GenerateCodeFromExpression(ci.FindMethod, sourceWriter, options);
                    }
                    DragDrop.DoDragDrop(tv_Elements, sb.ToString(), DragDropEffects.Copy);
                    e.Handled = true;
                }
                catch
                {

                }

            }
        }

        private void mi_Prop_Click(object sender, RoutedEventArgs e)
        {
            if (tv_Elements.SelectedItem != null)
            {
                XmlElement root = tv_Elements.SelectedItem as XmlElement;
                //XmlElement rootCopy = root.Clone() as XmlElement;
                string id = root.GetAttribute("id");
                GuiComponent cp = SAPAutomationHelper.Current.GetSAPComponentById<GuiComponent>(id);

                displayData(cp);
            }
        }


        private void displayData(GuiComponent c)
        {
            string type = c.GetDetailType();

            Type t = SAPAutomationHelper.Current.GetSAPTypeInfo(type);
            //Type detailType = null; GuiShell shellObj = null;
            //if (c.Type.ToLower().Contains("shell"))
            //{
            //    shellObj = c as GuiShell;
            //    if (shellObj != null)
            //    {
            //        foreach (Type t in SAPAutomationHelper.Current.SAPGuiApiAssembly.GetTypes().Where(tp => tp.IsInterface))
            //        {
            //            if (t.Name.Contains("Gui" + shellObj.SubType))
            //            {
            //                detailType = t;
            //                break;
            //            }
            //        }
            //    }
            //}

            //string typeName = detailType == null ? c.Type : detailType.Name;

            //var props = SAPAutomationHelper.Current.GetSAPTypeInfoes<PropertyInfo>(typeName, t => t.GetProperties().Where(p => p.IsSpecialName == false));

            List<SAPElementProperty> pps = new List<SAPElementProperty>();
            foreach (var p in t.GetProperties().Where(p => p.IsSpecialName == false))
            {
                SAPElementProperty prop = new SAPElementProperty();
                prop.Name = p.Name;
                prop.IsReadOnly = !p.CanWrite;
                try
                {
                    prop.Value = p.GetValue(c).ToString();
                }
                catch
                {
                    prop.Value = "";
                }
                pps.Add(prop);
            }
            List<string> mds = getMethods(t);
            lv_Props.Dispatcher.BeginInvoke(new Action(() =>
            {
                lv_Props.DataContext = pps;
                lv_Methods.DataContext = mds;
            }));


        }

        private List<string> getMethods(Type t)
        {
            List<string> methods = new List<string>();
            //var ms = SAPAutomationHelper.Current.GetSAPTypeInfoes<MethodInfo>(typeName, t => t.GetMethods().Where(m => m.IsSpecialName == false));
            foreach (var m in t.GetMethods().Where(m => m.IsSpecialName == false))
            {
                string method = string.Empty;
                method += m.ReturnType.Name + " " + m.Name;

                ParameterInfo[] paInfoes = m.GetParameters();
                if (paInfoes.Count() > 0)
                {
                    method += "(";
                    foreach (var p in paInfoes)
                    {
                        if (p.IsOptional)
                        {
                            method += "[Optional]";
                        }
                        method += p.ParameterType.Name + " " + p.Name + ",";
                    }
                    method = method.Substring(0, method.Length - 1);
                    method += ")";
                }
                else
                {
                    method += "()";
                }

                methods.Add(method);


            }
            return methods;
        }

        private void btn_Spy_Click(object sender, RoutedEventArgs e)
        {
            if (BeforeSpy != null)
                BeforeSpy(this, new EventArgs());
            //App.Current.MainWindow.WindowState = WindowState.Minimized;
            _isExpand = true;
            SAPAutomationHelper.Current.SetVisualMode(true);
            SAPAutomationHelper.Current.Spy((c) =>
            {
                displayData(c);
                GuiComponent cp = c;
                Stack<SAPCompoentBasicInfo> comps = new Stack<SAPCompoentBasicInfo>();
                do
                {
                    SAPCompoentBasicInfo info = new SAPCompoentBasicInfo();
                    info.Comp = cp;
                    info.ChildrenCount = 0;
                    if (cp is GuiVContainer)
                    {
                        info.ChildrenCount = (cp as GuiVContainer).Children.Count;
                    }
                    if (cp is GuiContainer)
                    {
                        info.ChildrenCount = (cp as GuiContainer).Children.Count;
                    }
                    comps.Push(info);

                    cp = cp.Parent;

                }
                while ((cp is GuiConnection) == false);
                xDoc = new XmlDocument();

                XmlElement root = xDoc.CreateElement("Node");
                root.SetAttribute("name", "root");
                var firstComp = comps.Pop();

                XmlElement temp = addNode(firstComp.Comp, root, firstComp.ChildrenCount);


                while (comps.Count > 0)
                {
                    var comp = comps.Pop();
                    temp = addNode(comp.Comp, temp, comp.ChildrenCount);
                }
                xDoc.AppendChild(root.FirstChild);

                tv_Elements.Dispatcher.BeginInvoke(new Action(() =>
                {
                    tv_Elements.DataContext = xDoc;
                    SAPAutomationHelper.Current.SetVisualMode(false);
                    if (AfterSpy != null)
                        AfterSpy(this, new EventArgs());
                    //App.Current.MainWindow.WindowState = WindowState.Normal;

                }));



            });
        }

        private void mi_CopyProp_Click(object sender, RoutedEventArgs e)
        {
            if (lv_Props.SelectedItem != null)
            {
                SAPElementProperty prop = lv_Props.SelectedItem as SAPElementProperty;
                Clipboard.SetData(DataFormats.Text, prop.Name);
            }
        }

        private void mi_CopyValue_Click(object sender, RoutedEventArgs e)
        {
            if (lv_Props.SelectedItem != null)
            {
                SAPElementProperty prop = lv_Props.SelectedItem as SAPElementProperty;
                Clipboard.SetData(DataFormats.Text, prop.Value);
            }
        }

      

        public void OnSetSession(object sender, GuiSession session)
        {
            throw new NotImplementedException();
        }


        List<GuiVComponent> batchComponents;
       
        private void btn_BatchSpy_Click(object sender, RoutedEventArgs e)
        { 
            
                batchComponents = new List<GuiVComponent>();
                BatchSpyWindow win = new BatchSpyWindow(batchComponents);
                win.ShowDialog();
            
            
                xDoc = new XmlDocument();

                XmlElement root = xDoc.CreateElement("Node");
                root.SetAttribute("name", "root");
                var firstComp = SAPAutomationHelper.Current.SAPGuiSession.ActiveWindow;

                XmlElement temp = addNode(firstComp, root, batchComponents.Count);
                foreach(var comp in batchComponents)
                {
                    addNode(comp, temp, 0);
                }

                
                xDoc.AppendChild(root.FirstChild);
            tv_Elements.DataContext = xDoc;

        }
        
    }

    public class SAPCompoentBasicInfo
    {
        public GuiComponent Comp { get; set; }

        public int ChildrenCount { get; set; }

        public int CurrentIndex { get; set; }
    }
}


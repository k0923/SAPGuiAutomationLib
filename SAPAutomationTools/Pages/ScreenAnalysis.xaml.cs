using SAPAutomation;
using SAPAutomationTools.ViewModel;
using SAPFEWSELib;
using SAPGuiAutomationLib;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace SAPAutomationTools.Pages
{
    /// <summary>
    /// ScreenAnalysis.xaml 的交互逻辑
    /// </summary>
    /// 
    public delegate void OnSessionSettingHanlder(GuiSession session);

    public partial class ScreenAnalysis : Page
    {
        public event OnSessionSettingHanlder OnSetSession;

        private GuiSession _session;
        private GuiApplication _app;

        public ScreenAnalysis()
        {
            InitializeComponent();
            SAPAutomationHelper.Current.SetSAPApiAssembly();
        }

        private void hookWorkingEvent()
        {
            var ctls = FindVisualChildren<UserControl>(this);
            if (ctls != null && ctls.Count() > 0)
            {
                foreach (IWorking c in ctls)
                {
                    c.OnWorking += c_OnWorking;
                    c.AfterWorking += c_AfterWorking;
                }
            }
        }

        void c_AfterWorking(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() => {
                this.IsEnabled = true;
                if (pb.IsIndeterminate)
                    pb.IsIndeterminate = false;
                pb.Value = 0;
            }));

        }

        void c_OnWorking(object sender, WorkingEventArgs e)
        {


            this.Dispatcher.BeginInvoke(new Action(() => {
                if (this.IsEnabled)
                    this.IsEnabled = false;
            }));

            pb.Dispatcher.BeginInvoke(new Action(() => {
                if (!e.IsProcessKnow && pb.IsIndeterminate == false)
                {
                    pb.IsIndeterminate = true; ;
                }
                else
                {
                    pb.Value = e.Current;
                    pb.Maximum = e.Max;
                }
            }));



        }

        private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T && child is IWorking)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void setSession(GuiSession session)
        {
            _session = session;
            SAPAutomationHelper.Current.SetSession(_session);
            if (OnSetSession != null)
            {
                OnSetSession(_session);
            }

        }



        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            if (lv_Sessions.SelectedItem != null && _app != null)
            {
                var vm = lv_Sessions.SelectedItem as SAPSessionVM;
                GuiSession session = _app.FindById<GuiSession>(vm.Id);
                setSession(session);
            }


            ep_Session.IsExpanded = false;

        }

        private void ep_Session_Expanded(object sender, RoutedEventArgs e)
        {
            _app = null;
            try
            {
                _app = SAPTestHelper.GetSAPGuiApp(1);
                List<SAPSessionVM> sessions = new List<SAPSessionVM>();
                for (int i = 0; i < _app.Children.Count; i++)
                {
                    GuiConnection cn = _app.Children.ElementAt(i) as GuiConnection;
                    for (int j = 0; j < cn.Children.Count; j++)
                    {
                        GuiSession s = cn.Children.ElementAt(j) as GuiSession;
                        sessions.Add(new SAPSessionVM()
                        {
                            Id = s.Id,
                            System = s.Info.SystemName,
                            Transaction = s.Info.Transaction
                        });
                    }
                }
                lv_Sessions.ItemsSource = sessions;
            }
            catch { }
        }

        private void ep_Session_Collapsed(object sender, RoutedEventArgs e)
        {
            lv_Sessions.ItemsSource = null;
        }



       

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            hookWorkingEvent();
            var win = Window.GetWindow(this);
            uc_SAPComponents.BeforeSpy += (s, e1) => { win.WindowState = WindowState.Minimized; };
            uc_SAPComponents.AfterSpy += (s, e1) => { win.WindowState = WindowState.Normal; };
            
        }

        
    }
}

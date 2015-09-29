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

namespace SAPUserControl
{
    /// <summary>
    /// SAPSessionInfo.xaml 的交互逻辑
    /// </summary>
    public partial class SAPSessionInfo : UserControl
    {
        public SAPSessionInfo()
        {
            InitializeComponent();
            SAPAutomationHelper.Current.OnSetSession += Current_OnSetSession;
        }

        private void Current_OnSetSession(object sender, GuiSession session)
        {
            session.EndRequest -= session_EndRequest;
            session.EndRequest += session_EndRequest;
            session.Destroy -= session_Destroy;
            session.Destroy += session_Destroy;
            showSessionInfo(session);
        }

       

        void session_EndRequest(GuiSession Session)
        {
            showSessionInfo(Session);
        }

        void session_Destroy(GuiSession Session)
        {
            lv_SessionInfo.Dispatcher.BeginInvoke(new Action(() => { lv_SessionInfo.DataContext = null; }));
        }

        private void showSessionInfo(GuiSession Session)
        {
            var props = SAPAutomationHelper.Current.GetSAPTypeInfo("GuiSessionInfo").GetProperties().Where(p => p.IsSpecialName == false);

            var sessionInfo = from p in props
                              select new { Name = p.Name, Value = p.GetValue(Session.Info) };
            var sis = sessionInfo.ToList();
            lv_SessionInfo.Dispatcher.BeginInvoke(new Action(() => { lv_SessionInfo.DataContext = sis; }));
        }

        
    }
}

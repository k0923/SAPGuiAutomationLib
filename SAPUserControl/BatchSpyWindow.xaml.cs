using SAPAutomation;
using SAPFEWSELib;
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
using System.Windows.Shapes;
using SAPGuiAutomationLib;

namespace SAPUserControl
{
    /// <summary>
    /// BatchSpyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BatchSpyWindow : Window
    {
        Point start;
        Point end;
        bool isStartSet;
        List<GuiVComponent> selectedComponents;
        

        public BatchSpyWindow(List<GuiVComponent> SelectedComponents)
        {
            InitializeComponent();
            this.selectedComponents = SelectedComponents;
            SAPAutomationHelper.Current.SAPGuiSession.ActiveWindow.Restore();
            this.Width = SAPAutomationHelper.Current.SAPGuiSession.ActiveWindow.Width;

            this.Height = SAPAutomationHelper.Current.SAPGuiSession.ActiveWindow.Height;

            this.Left = SAPAutomationHelper.Current.SAPGuiSession.ActiveWindow.Left;
            this.Top = SAPAutomationHelper.Current.SAPGuiSession.ActiveWindow.Top;
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            sp.Opacity = 0;
            if (selectedComponents != null)
            {
                foreach (var comp in selectedComponents)
                {
                    comp.Visualize(false);
                }
            }
            start = e.GetPosition(this);
            rect.SetValue(Canvas.LeftProperty, start.X);
            rect.SetValue(Canvas.TopProperty, start.Y);
            isStartSet = true;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isStartSet && e.LeftButton == MouseButtonState.Pressed)
            {
                end = e.GetPosition(this);

                if (end.X < start.X)
                {
                    rect.SetValue(Canvas.LeftProperty, end.X);
                }
                if (end.Y < start.Y)
                {
                    rect.SetValue(Canvas.TopProperty, end.Y);
                }
                double width = Math.Abs(end.X - start.X);
                double height = Math.Abs(end.Y - start.Y);


                rect.Width = width;
                rect.Height = height;
                rectG_Area.Rect = new Rect(start, end);

                

            }


        }

        private void cv_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int X1, X2, Y1, Y2;
            X1 = (int)start.X;
            X2 = (int)end.X;
            Y1 = (int)start.Y;
            Y2 = (int)end.Y;
            if (end.X < start.X)
            {
                X1 = (int)end.X;
                X2 = (int)start.X;
            }
            if (end.Y < start.Y)
            {
                Y1 = (int)end.Y;
                Y2 = (int)start.Y;
            }

            var comps = SAPAutomationHelper.Current.SAPGuiSession.ActiveWindow.FindByRegion(X1, Y1, X2, Y2);
            selectedComponents.Clear();
            foreach (var c in comps)
            {
                selectedComponents.Add(c);
                c.Visualize(true);
            }
            sp.Opacity = 1;
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

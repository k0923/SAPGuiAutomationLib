using SAPAutomation.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPAutomation;
using SAPFEWSELib;
using Young.Data.Attributes;

namespace Demo
{
    public class SC_101:SAPGuiScreen
    {
        private string _order;

        [ColumnBinding]
        [SingleSampleData("ZCR")]
        public string Order
        {
            get
            {
                return _order;
            }
            set
            {
                _order = value;
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("VBAK-AUART").Text = _order;
            }
        }

        [ColumnBinding]
        [SingleSampleData("CH00")]
        public string Sales_Organization
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("VBAK-VKORG").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("VBAK-VKORG").Text = value;
            }
        }

        [ColumnBinding]
        [SingleSampleData("C2")]
        public string DistributionChannel
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("VBAK-VTWEG").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("VBAK-VTWEG").Text = value;
            }
        }

        [ColumnBinding]
        [SingleSampleData("ZZ")]
        public string Division
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("VBAK-SPART").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("VBAK-SPART").Text = value;
            }
        }

        [MethodBinding(Order=99)]
        public SC_4001_Sales Process()
        {
            this.SendKeys(SAPKeys.Enter);
            return new SC_4001_Sales();
        }

    }
}

using SAPAutomation;
using SAPAutomation.Framework;
using SAPFEWSELib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Young.Data.Attributes;

namespace Demo
{
    public class SC_4002:SAPGuiScreen
    {
        [MethodBinding]
        public SC_4002_Sales Sales()
        {
            SAPTestHelper.Current.MainWindow.FindByName<GuiTab>("T\\01").Select();
            return new SC_4002_Sales();
        }
        
       

        [MethodBinding(Order=1)]
        public SC_4002_Additional_DataB Additional_DataB()
        {
            SAPTestHelper.Current.MainWindow.FindByName<GuiTab>("T\\13").Select();
                return new SC_4002_Additional_DataB();
        }
        
        [MethodBinding(Order=2)]
        public SC_4002_Texts Texts()
        {
            SAPTestHelper.Current.MainWindow.FindByName<GuiTab>("T\\09").Select();
                return new SC_4002_Texts();
        }
        
    }

    public class SC_4002_Sales:SAPGuiScreen
    {
        [ColumnBinding]
        [SingleSampleData("C02")]
        public string OrderReason
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiComboBox>("VBAK-AUGRU").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiComboBox>("VBAK-AUGRU").Key = value;
            }
        }
    }

    public class SC_4002_Additional_DataB : SAPGuiScreen
    {
        [ColumnBinding]
        [SingleSampleData("X")]
        public string ConfigCheck
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("VBAK-ZZCNFGCHK").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("VBAK-ZZCNFGCHK").Text = value;
            }
        }

        [ColumnBinding]
        [SingleSampleData("875007743")]
        public string CustBaseNo
        {
            get
            {
                return SAPTestHelper.Current.MainWindow.FindByName<GuiTextField>("VBAK-ZZCUSTBASE").Text;
            }
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiTextField>("VBAK-ZZCUSTBASE").Text = value;
            }
        }
    }

    public class SC_4002_Texts : SAPGuiScreen
    {
        private GuiTree _tree;

        public SC_4002_Texts()
        {
            _tree = SAPTestHelper.Current.MainWindow.FindByName<GuiContainerShell>("shellcont[0]").FindByName<GuiTree>("shell");
            
        }

        [ColumnBinding]
        [SingleSampleData("Z157")]
        public string TreeKey
        {
            set
            {
                _tree.SelectNode(value);
            }
        }

        [ColumnBinding(Order=1)]
        [SingleSampleData("credit order")]
        public string Comment
        {
            set
            {
                SAPTestHelper.Current.MainWindow.FindByName<GuiContainerShell>("shellcont[1]").FindByName<GuiTextedit>("shell").Text = value;
            }
        }

        [MethodBinding(Order=99)]
        public void Process()
        {
            this.SendKeys(SAPKeys.Ctrl_S);
        }
    }
}

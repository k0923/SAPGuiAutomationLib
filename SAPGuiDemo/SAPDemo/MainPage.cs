using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.Data.Attributes;
using SAPAutomation;
using SAPFEWSELib;

namespace SAPGuiDemo.SAPDemo
{
    

    [DataBinding]
    public class MainPage
    {
        [ColumnBinding]
        public string TCode { get; set; }

        [MethodBinding(Order = 2)]
        public TablePage Submit()
        {
            SAPTestHelper.Current.SAPGuiSession.StartTransaction("/n");
            SAPTestHelper.Current.MainWindow.FindDescendantByProperty<GuiOkCodeField>().Text = TCode;
            SAPTestHelper.Current.MainWindow.SendKey(SAPKeys.Enter);
            return new TablePage();
        }
    }

    [DataBinding]
    public class TablePage
    {
        [ColumnBinding]
        public string TableName { get; set; }

        [MethodBinding(Order =2)]
        public FieldPage Submit()
        {
            SAPTestHelper.Current.MainWindow.FindDescendantByProperty<GuiCTextField>().Text = TableName;
            SAPTestHelper.Current.MainWindow.SendKey(SAPKeys.Enter);
            return new FieldPage();
        }
    }

    [DataBinding]
    public class FieldPage
    {
        [ColumnBinding]
        public string RateType { get; set; }

        [ColumnBinding]
        public string CurFrom { get; set; }

        [ColumnBinding]
        public string CurTo { get; set; }

        [MethodBinding(Order =2)]
        public void Submit()
        {
            SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("I1-LOW").Text = RateType;
            SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("I2-LOW").Text = CurFrom;
            SAPTestHelper.Current.MainWindow.FindByName<GuiCTextField>("I3-LOW").Text = CurTo;
            SAPTestHelper.Current.MainWindow.FindByName<GuiButton>("btn[8]").Press();
        }
    }
}

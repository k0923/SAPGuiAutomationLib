using SAPFEWSELib;
using SAPAutomation;
using Young.Data;
using MB1A;
using Demo.WorkFlows;
using SAPTestFramework;
using System.Data;
using SAPAutomation.Framework.Report;

namespace Demo
{


    class Program
    {
        
        static void Main(string[] args)
        {
            
            

            SAPTestHelper.Current.SetSession();
            SAPTestHelper.Current.TurnScreenLog(ScreenLogLevel.All);
            DataEngine de = new DataEngine();
            DataEngine.GlobalBindingMode = new BindingMode()
            {
                LoopMode = LoopType.Loop,
                RecusionMode = RecusionType.NoRecursion
            };
            DataSet ds = ExcelHelper.Current.Open(@"C:\Demo\1.xlsx").ReadAll();
            de.SetData(ds);
            ExcelHelper.Current.Close();
            de.CurrentId = 1;

            Driver.Current.SetDataEngine(de);

            WorkFlow flow = Driver.Current.GetWorkFlow(typeof(MB1A_CreateGoodsIssueDoc));
            flow.Execute();
            
            flow = Driver.Current.GetWorkFlow(typeof(MB03_DisplayMaterialDocument));
            flow.Execute();
            Driver.Current.Finish();

            SAPReport report = new SAPReport();
            TestStep step1 = new TestStep();
            report.Detail.Add(step1);
            step1.InputDatas.Add(new TestData());
           
        }


        public static string GetCell(GuiGridView GridView, int row, string FriendlyColName)
        {
            string column = "";
            GridView.SelectAll();
            foreach (var colName in GridView.SelectedColumns)
            {
                string displayCol = GridView.GetDisplayedColumnTitle(colName);
                if (displayCol.IndexOf(FriendlyColName) >= 0)
                {
                    column = colName;
                    break;
                }

            }
            GridView.ClearSelection();
            return GridView.GetCellValue(row, column);
        }

        //static void Script1()
        //{
        //    SAPGuiScreen sc = null;
            
        //    sc = new EnterGoodsIssue_Initial();
        //    sc.StartTransaction("MB1A");
        //    sc.DataBinding();
        //    sc.SendKeys(SAPKeys.Enter);
        //    sc = new EnterGoodsIssue_NewItems();
        //    sc.DataBinding();
        //    sc.SendKeys(SAPKeys.Enter);
        //    sc = new EnterGoodsIssue_NewItem();
        //    sc.DataBinding();
        //    sc.SendKeys(SAPKeys.Enter);
        //    sc.SendKeys(SAPKeys.Ctrl_S);

            

            
        //}

        

     
    }

    public class CreateMaterial : WorkFlow
    {
        public override string BoxName
        {
            get
            {
                return "C50_HPE";
            }
        }

        public override string FlowName
        {
            get
            {
                return "CreateMaterial";
            }
        }

       

        public override void Execute()
        {
            
            var screen1 = getScreenComponent<EnterGoodsIssue_Initial>();
            
            //var screen2 = getScreenComponent<EnterGoodsIssue_NewItems>();

            //var screen3 = getScreenComponent<EnterGoodsIssue_NewItem>();
        }
    }
}

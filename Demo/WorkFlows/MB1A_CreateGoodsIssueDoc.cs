using MB1A;
using SAPAutomation;
using SAPAutomation.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WorkFlows
{
    public class MB1A_CreateGoodsIssueDoc : WorkFlow
    {
        public override string BoxName
        {
            get
            {
                return "MB1A";
            }
        }

        public override string FlowName
        {
            get
            {
                return "MB1A_CreateGoodsIssueDoc";
            }
        }

        public override void Execute()
        {
            SAPTestHelper.Current.SAPGuiSession.StartTransaction("MB1A");
            EnterGoodsIssue_Initial sc1 = getScreenComponent<EnterGoodsIssue_Initial>();
            sc1.FillData();
            sc1.SendKeys(SAPKeys.Enter);

            EnterGoodsIssue_NewItems sc2 = getScreenComponent<EnterGoodsIssue_NewItems>();
            sc2.FillData();
            sc2.SendKeys(SAPKeys.Enter);

            EnterGoodsIssue_NewItem sc3 = getScreenComponent<EnterGoodsIssue_NewItem>();
            sc3.FillData();
            sc3.SendKeys(SAPKeys.Enter);

            sc2.SendKeys(SAPKeys.Ctrl_S);
            updateScreenComponent<EnterGoodsIssue_Initial>(sc1);
            
        }
    }
}

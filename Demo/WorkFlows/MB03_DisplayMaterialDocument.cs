using SAPAutomation;
using SAPAutomation.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.MB03;
using SAPTestFramework;

namespace Demo.WorkFlows
{
    public class MB03_DisplayMaterialDocument : WorkFlow
    {
        public override string BoxName
        {
            get
            {
                return "MB03";
            }
        }

        public override string FlowName
        {
            get
            {
                return "MB03_DisplayMaterialDocument";
            }
        }

        public override void Execute()
        {
            SAPTestHelper.Current.SAPGuiSession.StartTransaction("MB03");
            DisplayMaterialDocument_Initial_Screen sc1 = getScreenComponent<DisplayMaterialDocument_Initial_Screen>();
            sc1.FillData();
            sc1.SendKeys(SAPKeys.Enter);

            DisplayMaterialDocument_Overview sc2 = getScreenComponent<DisplayMaterialDocument_Overview>();
            sc2.AccountingDocuments();
            sc2.SelectAccount();

            DisplayDocument_Overview sc3 = getScreenComponent<DisplayDocument_Overview>();
            

        }
    }
}

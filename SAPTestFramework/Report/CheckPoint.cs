using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPAutomation.Framework.Report
{
    public class CheckPoint
    {
        public string CSPN { get; set; }

        public string CPStepName { get; set; }

        public string CPName { get; set; }

        public DateTime CPTime { get; set; }

        public string CPStatus { get; set; }

        public string CPOriginalValue { get; set; }

        public string CPExpected { get; set; }

        public string CPActual { get; set; }

        public string CPCompareMode { get; set; }

        public string CPSnapShot { get; set; }
    }
}

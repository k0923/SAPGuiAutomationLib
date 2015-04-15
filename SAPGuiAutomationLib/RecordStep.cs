using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPGuiAutomationLib
{
    public class RecordStep
    {
        public int StepId { get; set; }

        public RecordAction Action { get; set; }

        public SapCompInfo CompInfo { get; set; }

        public string ActionName { get; set; }

        public object[] ActionParams { get; set; }
    }
}

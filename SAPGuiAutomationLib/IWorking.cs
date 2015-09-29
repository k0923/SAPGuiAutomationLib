using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPAutomationTools
{
    public delegate void OnWorkingHandler(object sender, WorkingEventArgs e);
    public delegate void OnWorkFinishHandler(object sender, EventArgs e);
    public interface IWorking
    {
        event OnWorkingHandler OnWorking;

        event OnWorkFinishHandler AfterWorking;
    }

    public class WorkingEventArgs : EventArgs
    {
        public int Current { get; set; }

        public int Max { get; set; }

        public bool IsProcessKnow { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;

namespace SAPGuiAutomationLib
{
    public class RecordStep:WPFNotify
    {
        private int _stepId;
        public int StepId 
        {
            get { return _stepId; }
            set
            {
                SetProperty<int>(ref _stepId, value);
            }
        }

        public BindingFlags Action { get; set; }

        public SapCompInfo CompInfo { get; set; }

        public string ActionName { get; set; }

        public object[] ActionParams { get; set; }

        

        
    }

    
}

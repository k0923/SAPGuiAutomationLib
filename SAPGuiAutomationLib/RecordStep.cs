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
        public RecordStep() {
           
        }

        private int _stepId;
        public int StepId
        {
            get { return _stepId; }
            set
            {
                SetProperty<int>(ref _stepId, value);
            }
        }

        private bool _isParameterize;
        public bool IsParameterize
        {
            get { return _isParameterize; }
            set { SetProperty<bool>(ref _isParameterize, value); }
        }

        public BindingFlags Action { get; set; }

        public SapCompInfo CompInfo { get; set; }

        public string ActionName { get; set; }

        public List<SAPDataParameter> ActionParams { get; set; }

        public bool TakeScreenShot { get; set; }
        
    }

    
}

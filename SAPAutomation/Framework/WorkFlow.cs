using SAPAutomation.Framework.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.Data;

namespace SAPAutomation.Framework
{
    public abstract class WorkFlow
    {
        private List<Type> _screenTypes;

        public IEnumerable<Type> ScreenTypes { get { return _screenTypes; } }

        protected void registerScreen<T>() where T :SAPGuiScreen,new()
        {
            _screenTypes.Add(typeof(T));
        }

        public abstract string FlowName { get; }

        public abstract string BoxName { get; }

        private DataEngine _dataEngine;

        private TestStep _step;

        public void SetBasicInfo(DataEngine dataEngine,TestStep step)
        {
            _dataEngine = dataEngine;
            _step = step;
        }

        protected WorkFlow()
        {
            _screenTypes = new List<Type>();
        }

        public abstract void Execute();

        protected T getScreenComponent<T>() where T :SAPGuiScreen,new()
        {
            T screen = _dataEngine.Create<T>();
            return screen;
        }

        protected void addCheckpoint(CheckPoint cp)
        {
            _step.CheckPoints.Add(cp);
        }
        
    }

   
   

    

    
}

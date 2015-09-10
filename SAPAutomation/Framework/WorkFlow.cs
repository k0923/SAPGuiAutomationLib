using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.Data;

namespace SAPAutomation.Framework
{
    public delegate void OnFlowInitialHander(WorkFlow sender, WorkFlowEventArgs e);

    public abstract class WorkFlow
    {
        private List<Type> _screenTypes;

        public IEnumerable<Type> ScreenTypes { get { return _screenTypes; } }

        protected void registerScreen<T>() where T :SAPGuiScreen,new()
        {
            _screenTypes.Add(typeof(T));
        }

        //public static event OnFlowInitialHander OnFlowInitial;

        public abstract string FlowName { get; }

        public abstract string BoxName { get; }

        private DataEngine _dataEngine;

        public void SetDataEngine(DataEngine dataEngine)
        {
            _dataEngine = dataEngine;
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
        
    }

    public class WorkFlowEventArgs:EventArgs
    {

    }

   

    

    
}

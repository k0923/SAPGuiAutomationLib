using SAPAutomation.Framework.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.Data;

namespace SAPTestFramework
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

        protected void updateScreenComponent<T>(T item) where T : SAPGuiScreen
        {
            _dataEngine.Update(item);
        }

        protected void addCheckpoint(CheckPoint cp)
        {
            _step.CheckPoints.Add(cp);
        }
        
        protected void addOutputValue(TestData output)
        {
            setTestData(_step.OutputDatas, output);
        }

        protected void addInputValue(TestData input)
        {
            setTestData(_step.InputDatas, input);
        }

        private void setTestData(List<TestData> datas, TestData data)
        {
            var myData = datas.Where(c => c.FieldName == data.FieldName).FirstOrDefault();
            if (myData != null)
                myData.FieldValue = data.FieldValue;
            else
            {
                datas.Add(data);
            }
            
        }
    }
}

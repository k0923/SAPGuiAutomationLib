using SAPAutomation.Framework.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.Data;

namespace SAPTestFramework
{
    public class Driver
    {
        private static object _lockObj = new object();
        private static Driver _instance;

        private WorkFlow _currentWorkFlow;
        private TestStep _currentStep;

        private SAPReport _report;

        private Driver()
        {
            _report = new SAPReport();
            _report.Summary.Start = DateTime.Now;
            _report.Summary.Machine = Environment.MachineName;
            _report.Summary.Executor = Environment.UserName;
        }

        private DataEngine _dataEngine;

        public void SetDataEngine(DataEngine engine)
        {
            _dataEngine = engine;
            _dataEngine.OnSettingProperty += _dataEngine_OnSettingProperty;
            _dataEngine.OnGettingProperty += _dataEngine_OnGettingProperty;
        }

        private void _dataEngine_OnGettingProperty(object sender, SetPropertyArgs e)
        {
            if(_currentStep != null)
            {
                foreach(var d in getTestData(e))
                {
                    setTestData(_currentStep.OutputDatas, d);
                }
            }
        }

        private void _dataEngine_OnSettingProperty(object sender, SetPropertyArgs e)
        {
            if(_currentStep != null)
            {
                foreach(var d in getTestData(e))
                {
                    setTestData(_currentStep.InputDatas, d);
                }
            }
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

        private IEnumerable<TestData> getTestData(SetPropertyArgs e)
        {
            List<TestData> datas = new List<TestData>();
            if(e.Value.GetType() == typeof(DataRow[]))
            {
                DataRow[] rows = e.Value as DataRow[];
                int index = 0;
                foreach(var row in rows)
                {
                    foreach(var col in e.Attribute.ColNames)
                    {
                        datas.Add(new TestData() { FieldName = string.Format("{0}[{1}]", col, index), FieldValue = row[col] });
                    }
                    index++;
                }
            }
            else
            {
                datas.Add(new TestData() { FieldName = e.Attribute.ColNames.First(), FieldValue = e.Value });
            }
            return datas;
        }

        public SAPReport MyReport { get { return _report; } }

        public static Driver Current
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                            _instance = new Driver();
                    }
                }
                return _instance;
            }
        }

        public T GetWorkFlow<T>() where T :WorkFlow,new()
        {
            _currentWorkFlow = new T();
            _currentStep = new TestStep()
            {
                Name = _currentWorkFlow.FlowName,
                CaseName = _currentWorkFlow.FlowName,
                BoxName = _currentWorkFlow.BoxName,
            };
            _report.Detail.Add(_currentStep);
            _currentWorkFlow.SetBasicInfo(_dataEngine, _currentStep);
            return _currentWorkFlow as T; 
        }

        public WorkFlow GetWorkFlow(Type workFlowType)
        {
            if(workFlowType.IsSubclassOf(typeof(WorkFlow)))
            {
                _currentWorkFlow = Activator.CreateInstance(workFlowType) as WorkFlow;
                _currentStep = new TestStep()
                {
                    Name = _currentWorkFlow.FlowName,
                    CaseName = _currentWorkFlow.FlowName,
                    BoxName = _currentWorkFlow.BoxName,
                };
                _report.Detail.Add(_currentStep);
                _currentWorkFlow.SetBasicInfo(_dataEngine, _currentStep);
                return _currentWorkFlow;
            }
            return null;
        }

        public void SaveReport()
        {
            _report.Save("Report.xml");
        }

        public void Finish()
        {
            _report.Summary.End = DateTime.Now;
            SaveReport();
            _instance = null;
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SAPAutomation.Framework.Report
{
    [XmlRoot("ReportRoot")]
    public class SAPReport
    {
        private Summary _summary;
        private EventList<TestStep> _steps;

        private bool _isAutoSave;

        private string _reportPath = "Report.xml";

        public SAPReport(string ReportFilePath):this()
        {
            _reportPath = ReportFilePath;
        }

        public SAPReport() {
            _summary = new Summary();
            _steps = new EventList<TestStep>();
            _steps.OnAdd += _steps_OnAdd;
        }

        public void TurnAutoSave(bool isAutoSave)
        {
            _isAutoSave = isAutoSave;
        }

        private void _steps_OnAdd(object sender, TestStep item)
        {
            item.Number = _steps.Count + 1;
            if(_isAutoSave)
                this.Save(_reportPath);
        }

        public Summary Summary
        {
            get { return _summary; }
            set { _summary = value; }
        }

        public void Save(string ReportFilePath)
        {
            XmlSerializer xs = new XmlSerializer(typeof(SAPReport));
            using (FileStream fs = new FileStream(ReportFilePath, FileMode.Create))
            {
                xs.Serialize(fs, this);
            }
        }

        public static SAPReport Restore(string ReportFilePath)
        {
            SAPReport report = null;
            XmlSerializer xs = new XmlSerializer(typeof(SAPReport));
            using (FileStream fs = new FileStream(ReportFilePath, FileMode.Open))
            {
                report = xs.Deserialize(fs) as SAPReport;
            }
            return report;
        }

        public EventList<TestStep> Detail { get { return _steps; } set { _steps = value; } }
    }
}

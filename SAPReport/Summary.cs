using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Driver.Report
{
    public class Summary
    {
        public Summary()
        {
            Start = DateTime.Now;
        }

        public string TestName { get; set; }

        public string Status { get; set; }

        public string CompanyCode { get; set; }

        public string Asset { get; set; }

        public string BoxNameList { get; set; }

        public int TotalSteps { get; set; }

        public int RunTime { get; set; }

        public string RunMode { get; set; }

        [XmlElement("TestMachine")]
        public string Machine { get; set; }

        public string SAPVersion { get; set; }

        public string TimeZone { get; set; }

        [XmlElement("TestStartTime")]
        public DateTime Start { get; set; }

        [XmlElement("TestEndTime")]
        public DateTime End { get; set; }

        public string Duration { get; set; }

        public string Executor { get; set; }

    }
}

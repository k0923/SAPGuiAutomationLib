using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Driver.Report
{
    public class TestStep
    {
        private List<TestData> _inputs;

        private List<TestData> _outputs;

        private List<CheckPoint> _checkpoints;

        public TestStep()
        {
            _inputs = new List<TestData>();
            _outputs = new List<TestData>();
            _checkpoints = new List<CheckPoint>();
        }

        

        [XmlAttribute]
        public string Name { get; set; }

        public string CaseName { get; set; }

        public int Number { get; set; }

        public string BoxName { get; set; }

        public string ErrorLog { get; set; }

        public string SAPMessage { get; set; }

        [XmlElement("CaseStatus")]
        public string Status { get; set; }

        public string ErrorSnapShot { get; set; }

        public List<TestData> InputDatas { get { return _inputs; } set { _inputs = value; } }

        public List<TestData> OutputDatas { get { return _outputs; } set { _outputs = value; } }

        public List<CheckPoint> CheckPoints { get { return _checkpoints; } set { _checkpoints = value; } }
    }


}

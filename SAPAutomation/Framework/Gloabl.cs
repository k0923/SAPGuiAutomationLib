using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SAPAutomation.Framework
{
    public static class Global
    {
        public static DataSet DataSet { get; set; }

        public static int CurrentId { get; set; }
    }
}

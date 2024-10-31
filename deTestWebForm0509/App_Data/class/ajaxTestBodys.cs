using NPOI.OpenXmlFormats;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace deTestWebForm0509
{
    public class ajaxTestBodys
    {

        public string name { get; set; }
        public string age { get; set; }
        public string address { get; set; }
        public string gender { get; set; }
        public string phone { get; set; }
        public string knowledge { get; set; }
        public string bodyStyle { get; set; }

    }
}
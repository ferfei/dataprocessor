using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataProcessorAPI.Service
{
    public class Records
    {
        public Record[] records { get; set; }
    }

    public class Record
    {
        public string POI_CODE { get; set; }
        public string DEPARTMENT_CODE { get; set; }
        public string DEPARTMENT_DESCRIPTION { get; set; }
        public string AGENCY_SUBELEMENT_CODE { get; set; }
        public string AGENCY_SUBELEMENT_DESCRIPTION { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
    }

}

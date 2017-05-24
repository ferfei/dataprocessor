using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DataProcessorAPI.Service
{
    public class QueryReport
    {
        public long recordSummaryCount;

        public List<QueryResultDetail> results;

        public QueryReport()
        {
        }

        public string getQueryResultsAsJSON()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{\"records\":[");
            int num = 0;
            string[] strArrays = null;
            foreach (QueryResultDetail result in this.results)
            {
                if (num == 0)
                {
                    strArrays = result.key.Split(new char[] { '|' });
                    num = 1;
                }
                stringBuilder.Append(result.getRecordAsJSON(strArrays)).Append(",");
            }
            stringBuilder.Append("{}]}");
            return stringBuilder.ToString();
        }
    }
}
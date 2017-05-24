using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DataProcessorAPI.Service
{
    public class QueryResultDetail
    {
        public string key;

        public string @value;

        public QueryResultDetail()
        {
        }

        public string getRecordAsJSON()
        {
            StringBuilder stringBuilder = new StringBuilder("{");
            string[] strArrays = this.@value.Split(new char[] { '|' });
            string[] strArrays1 = this.key.Split(new char[] { '|' });
            int num = 0;
            string[] strArrays2 = strArrays1;
            for (int i = 0; i < (int)strArrays2.Length; i++)
            {
                string str = strArrays2[i];
                string[] strArrays3 = new string[] { "\"", strArrays1[num], "\":\"", strArrays[num], "\"," };
                stringBuilder.Append(string.Concat(strArrays3));
                num++;
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        public string getRecordAsJSON(string[] elementNames)
        {
            StringBuilder stringBuilder = new StringBuilder("{");
            string[] strArrays = this.@value.Split(new char[] { '|' });
            int num = 0;
            string[] strArrays1 = elementNames;
            for (int i = 0; i < (int)strArrays1.Length; i++)
            {
                string str = strArrays1[i];
                string[] strArrays2 = new string[] { "\"", elementNames[num], "\":\"", strArrays[num], "\"," };
                stringBuilder.Append(string.Concat(strArrays2));
                num++;
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
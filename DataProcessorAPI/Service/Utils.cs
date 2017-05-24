using DataProcessorAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataProcessorAPI.Service
{
    public class Utils
    {
        public static string ConvertStringToHex(byte[] b)
        {
            string str = "";
            for (int i = 0; i < (int)b.Length; i++)
            {
                str = string.Concat(str, Convert.ToString((b[i] & 255) + 256, 16).Substring(1));
            }
            return str;
        }

        public static Dictionary<string, Record[]> GetAgencyList(AsyncParameter param)
        {
            var agencies = new Dictionary<string, Record[]>();
            var user = new User()
            {
                name = param.Username,
                password = param.Password
            };

            var proxy = new Proxy(param.Key, param.Url, user);
            var result = proxy.GetData<Records>(param.QueryId);

            agencies = result.records.ToLookup(a => a.DEPARTMENT_CODE).Where(x => x.Key != null).ToDictionary(x => x.Key, x => x.ToArray());

            return agencies;
        }

        public static QueryReport GetQueryResult(int queryId, Proxy proxy)
        {
            return proxy.GetData(queryId);
        }

        public static Records GetQueryRecords(int queryId, Proxy proxy)
        {
            return proxy.GetData<Records>(queryId);
        }

        public static void CreateAgencies(Dictionary<string, Record[]> agencies)
        {
            foreach (var agency in agencies)
            {
                CreateAgency(agency);
            }
        }

        public static Proxy GetProxy(AsyncParameter param)
        {
            var user = new User()
            {
                name = param.Username,
                password = param.Password
            };

            var proxy = new Proxy(param.Key, param.Url, user);
            return proxy;
        }

        public static void CreateAgency(KeyValuePair<string, Record[]> records)
        {
            EOPF_AGENCY_INFO info = null;

            var dept = records.Value.FirstOrDefault();
            using (var context = new Entities())
            {
                if (!context.EOPF_AGENCY_INFO.Any(a => a.EAI_CODE.ToUpper() == dept.DEPARTMENT_CODE.ToUpper()))
                {
                    info = new EOPF_AGENCY_INFO()
                    {
                        EAI_CODE = dept.DEPARTMENT_CODE,
                        EAI_DESCRIPTION = dept.DEPARTMENT_DESCRIPTION
                    };

                    context.EOPF_AGENCY_INFO.Add(info);
                    context.SaveChangesAsync();
                    CreateSubAgency(info.EAI_ID, records.Value);
                }
            }
        }

        public static void CreateSubAgency(decimal? agency, Record[] records)
        {
            EOPF_AGENCY_DETAILS detail = null;

            using (var context = new Entities())
            {
                foreach (Record record in records)
                {
                    if (!context.EOPF_AGENCY_DETAILS.Any(a => a.EAD_EAI_ID == agency && a.EAD_CODE == record.AGENCY_SUBELEMENT_CODE))
                    {
                        detail = new EOPF_AGENCY_DETAILS()
                        {
                            EAD_EAI_ID = agency,
                            EAD_POI_CODE = Convert.ToInt32(record.POI_CODE),
                            EAD_CODE = record.AGENCY_SUBELEMENT_CODE,
                            EAD_DESCRIPTION = record.AGENCY_SUBELEMENT_DESCRIPTION,
                            EAD_CITY = record.CITY,
                            EAD_STATE = record.STATE
                        };
                        context.EOPF_AGENCY_DETAILS.Add(detail);
                    }
                    context.SaveChangesAsync();
                }
            }
        }
    }
}
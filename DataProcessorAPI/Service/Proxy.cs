using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataProcessorAPI.Service
{
    public class Proxy
    {
        private EgusWebServiceClient Client;

        public Proxy(string key, string url, User user)
        {
            Client = new EgusWebServiceClient(key, url, user);
        }

        public T GetData<T>(int queryId) where T : class
        {
            Query query = new Query();
            query.id = queryId;

            var output = Client.runQuery(query);

            return JsonConvert.DeserializeObject<T>(output.getQueryResultsAsJSON());
        }
        public QueryReport GetData(int queryId)
        {
            Query query = new Query();
            query.id = queryId;

            var output = Client.runQuery(query);

            return output;
        }
    }
}

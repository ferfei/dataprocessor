using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Script.Serialization;

namespace DataProcessorAPI.Service
{
    public class EgusWebServiceClient
    {
        private string decryptKey;

        private string baseURL;

        private WebRequest webRequestData;

        private WebRequest webRequestToken;

        private JavaScriptSerializer jsSerializer;

        private User user;

        public EgusWebServiceClient(string decryptKey, string baseURL, User user)
        {
            this.decryptKey = decryptKey;
            this.baseURL = baseURL;
            this.jsSerializer = new JavaScriptSerializer();
            this.user = user;
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.AcceptAllCertifications);
        }

        public bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private static string EncodeTo64(string toEncode)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(toEncode));
        }

        private string getHashable(string token, Query query, string encryptKey)
        {
            string str = "";
            foreach (Predicate predicate in query.predicates)
            {
                str = string.Concat(str, predicate.bindPos, predicate.@value);
            }
            object[] objArray = new object[] { str, token, query.id, encryptKey };
            str = string.Concat(objArray);
            return str;
        }

        private string getToken(User user)
        {
            this.webRequestToken = WebRequest.Create(string.Concat(this.baseURL, "/token"));
            this.webRequestToken.Method = "POST";
            this.webRequestToken.ContentType = "application/json";
            user.authorizedQueries = new List<int>();
            string str = this.jsSerializer.Serialize(user);
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            this.webRequestToken.ContentLength = (long)((int)bytes.Length);
            Stream requestStream = this.webRequestToken.GetRequestStream();
            requestStream.Write(bytes, 0, (int)bytes.Length);
            requestStream.Close();
            requestStream = this.webRequestToken.GetResponse().GetResponseStream();
            return (new StreamReader(requestStream)).ReadToEnd();
        }

        public QueryReport runQuery(Query query)
        {
            string token = this.getToken(this.user);
            string str = this.jsSerializer.Serialize(query.predicates);
            SHA512CryptoServiceProvider sHA512CryptoServiceProvider = new SHA512CryptoServiceProvider();
            string hashable = this.getHashable(token, query, this.decryptKey);
            byte[] numArray = sHA512CryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(hashable), 0, hashable.Length);
            string hex = Utils.ConvertStringToHex(numArray);
            object[] objArray = new object[] { this.baseURL, "/", token, "/query/", query.id, "/summary/", hex };
            this.webRequestData = WebRequest.Create(string.Concat(objArray));
            this.webRequestData.Method = "PUT";
            this.webRequestData.ContentType = "plain/text";
            byte[] bytes = Encoding.ASCII.GetBytes(Convert.ToBase64String(Crypto.encrypt(str, this.decryptKey)));
            this.webRequestData.ContentLength = (long)((int)bytes.Length);
            Stream requestStream = this.webRequestData.GetRequestStream();
            requestStream.Write(bytes, 0, (int)bytes.Length);
            requestStream.Close();
            WebResponse response = this.webRequestData.GetResponse();
            requestStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(requestStream);
            byte[] numArray1 = Convert.FromBase64String(streamReader.ReadToEnd());
            byte[] numArray2 = Crypto.decrypt(numArray1, this.decryptKey);
            QueryReport obj = this.translateToObject(Encoding.UTF8.GetString(numArray2));
            streamReader.Close();
            requestStream.Close();
            response.Close();
            return obj;
        }

        private QueryReport translateToObject(string jsonString)
        {
            return this.jsSerializer.Deserialize<QueryReport>(jsonString);
        }
    }
}
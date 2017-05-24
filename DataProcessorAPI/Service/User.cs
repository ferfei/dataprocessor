using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataProcessorAPI.Service
{
    public class User
    {
        public List<int> authorizedQueries
        {
            get;
            set;
        }

        public string description
        {
            get;
            set;
        }

        public string encryptKey
        {
            get;
            set;
        }

        public object id
        {
            get;
            set;
        }

        public string name
        {
            get;
            set;
        }

        public string password
        {
            get;
            set;
        }

        public User()
        {
        }
    }
}
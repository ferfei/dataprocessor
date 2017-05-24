using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataProcessorAPI.Service
{
    public class AsyncParameter
    {
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Key { get; set; }
        public int QueryId { get; set; }
        public char Active { get; set; }

    }
}

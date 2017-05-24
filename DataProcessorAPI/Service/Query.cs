using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataProcessorAPI.Service
{
    public class Query
    {
        public int id;

        public List<Predicate> predicates;

        public Query()
        {
            this.predicates = new List<Predicate>();
        }
    }
}
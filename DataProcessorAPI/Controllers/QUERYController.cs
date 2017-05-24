using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DataProcessorAPI.Models;
using DataProcessorAPI.Service;

namespace DataProcessorAPI.Controllers
{
    public class QUERYController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/QUERY
        public IQueryable<EOPF_DP_QUERY> GetEOPF_DP_QUERY()
        {
            return db.EOPF_DP_QUERY;
        }

        // GET: api/QUERY/5
        [ResponseType(typeof(EOPF_DP_QUERY))]
        public IHttpActionResult GetEOPF_DP_QUERY(int id)
        {
            EOPF_DP_QUERY eOPF_DP_QUERY = db.EOPF_DP_QUERY.Find(id);
            if (eOPF_DP_QUERY == null)
            {
                return NotFound();
            }

            return Ok(eOPF_DP_QUERY);
        }

        // PUT: api/QUERY/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEOPF_DP_QUERY(int id, EOPF_DP_QUERY eOPF_DP_QUERY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eOPF_DP_QUERY.ID)
            {
                return BadRequest();
            }

            db.Entry(eOPF_DP_QUERY).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EOPF_DP_QUERYExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/QUERY
        [ResponseType(typeof(EOPF_DP_QUERY))]
        public IHttpActionResult PostEOPF_DP_QUERY(EOPF_DP_QUERY eOPF_DP_QUERY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EOPF_DP_QUERY.Add(eOPF_DP_QUERY);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = eOPF_DP_QUERY.ID }, eOPF_DP_QUERY);
        }

        // DELETE: api/QUERY/5
        [ResponseType(typeof(EOPF_DP_QUERY))]
        public IHttpActionResult DeleteEOPF_DP_QUERY(int id)
        {
            EOPF_DP_QUERY eOPF_DP_QUERY = db.EOPF_DP_QUERY.Find(id);
            if (eOPF_DP_QUERY == null)
            {
                return NotFound();
            }

            db.EOPF_DP_QUERY.Remove(eOPF_DP_QUERY);
            db.SaveChanges();

            return Ok(eOPF_DP_QUERY);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: api/QUERY/RUN/5
        [ResponseType(typeof(void))]
        public IHttpActionResult RUN_QUERY(IEnumerable<EOPF_DP_QUERY> queries)
        {
            EOPF_DP_SETTING setting = db.EOPF_DP_SETTING.First();
            AsyncParameter param = new AsyncParameter
            {
                Key = setting.SERVICE_KEY,
                Url = setting.SERVICE_URL,
                Username = setting.SERVICE_USERNAME,
                Password = setting.SERVICE_PASSWORD,
            };

            Proxy proxy = Utils.GetProxy(param);

            foreach (var query in queries)
            {
                Records result = Utils.GetQueryRecords(query.QUERY, proxy);

                if ( query.EOPF_DP_MAPPING.TABLE_NAME.Equals("EOPF_AGENCY_INFO") )
                {
                    var agencies = new Dictionary<string, Record[]>();
                    agencies = result.records.ToLookup(a => a.DEPARTMENT_CODE).Where(x => x.Key != null).ToDictionary(x => x.Key, x => x.ToArray());
                    Utils.CreateAgencies(agencies);
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        private bool EOPF_DP_QUERYExists(int id)
        {
            return db.EOPF_DP_QUERY.Count(e => e.ID == id) > 0;
        }
    }
}
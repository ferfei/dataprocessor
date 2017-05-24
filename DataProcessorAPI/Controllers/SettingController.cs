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

namespace DataProcessorAPI.Controllers
{
    public class SettingController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Setting
        public IQueryable<EOPF_DP_SETTING> GetEOPF_DP_SETTING()
        {
            return db.EOPF_DP_SETTING;
        }

        // GET: api/Setting/5
        [ResponseType(typeof(EOPF_DP_SETTING))]
        public IHttpActionResult GetEOPF_DP_SETTING(decimal id)
        {
            EOPF_DP_SETTING eOPF_DP_SETTING = db.EOPF_DP_SETTING.Find(id);
            if (eOPF_DP_SETTING == null)
            {
                return NotFound();
            }

            return Ok(eOPF_DP_SETTING);
        }

        // PUT: api/Setting/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEOPF_DP_SETTING(decimal id, EOPF_DP_SETTING eOPF_DP_SETTING)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eOPF_DP_SETTING.ID)
            {
                return BadRequest();
            }

            db.Entry(eOPF_DP_SETTING).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EOPF_DP_SETTINGExists(id))
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

        // POST: api/Setting
        [ResponseType(typeof(EOPF_DP_SETTING))]
        public IHttpActionResult PostEOPF_DP_SETTING(EOPF_DP_SETTING eOPF_DP_SETTING)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EOPF_DP_SETTING.Add(eOPF_DP_SETTING);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = eOPF_DP_SETTING.ID }, eOPF_DP_SETTING);
        }

        // DELETE: api/Setting/5
        [ResponseType(typeof(EOPF_DP_SETTING))]
        public IHttpActionResult DeleteEOPF_DP_SETTING(decimal id)
        {
            EOPF_DP_SETTING eOPF_DP_SETTING = db.EOPF_DP_SETTING.Find(id);
            if (eOPF_DP_SETTING == null)
            {
                return NotFound();
            }

            db.EOPF_DP_SETTING.Remove(eOPF_DP_SETTING);
            db.SaveChanges();

            return Ok(eOPF_DP_SETTING);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EOPF_DP_SETTINGExists(decimal id)
        {
            return db.EOPF_DP_SETTING.Count(e => e.ID == id) > 0;
        }
    }
}
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
using AciLabTestApp;

namespace AciLabTestApp.Controllers
{
    public class TestController : ApiController
    {
        private StudentDBEntities db = new StudentDBEntities();

        // GET api/Test
        public IQueryable<tblLogin> GettblLogins()
        {
            return db.tblLogins;
        }

        // GET api/Test/5
        [ResponseType(typeof(tblLogin))]
        public IHttpActionResult GettblLogin(int id)
        {
            tblLogin tbllogin = db.tblLogins.Find(id);
            if (tbllogin == null)
            {
                return NotFound();
            }

            return Ok(tbllogin);
        }

        // PUT api/Test/5
        public IHttpActionResult PuttblLogin(int id, tblLogin tbllogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbllogin.loginId)
            {
                return BadRequest();
            }

            db.Entry(tbllogin).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblLoginExists(id))
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

        // POST api/Test
        [ResponseType(typeof(tblLogin))]
        public IHttpActionResult PosttblLogin(tblLogin tbllogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblLogins.Add(tbllogin);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbllogin.loginId }, tbllogin);
        }

        // DELETE api/Test/5
        [ResponseType(typeof(tblLogin))]
        public IHttpActionResult DeletetblLogin(int id)
        {
            tblLogin tbllogin = db.tblLogins.Find(id);
            if (tbllogin == null)
            {
                return NotFound();
            }

            db.tblLogins.Remove(tbllogin);
            db.SaveChanges();

            return Ok(tbllogin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblLoginExists(int id)
        {
            return db.tblLogins.Count(e => e.loginId == id) > 0;
        }
    }
}
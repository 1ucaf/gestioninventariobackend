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
using DataAccess;
using Modelo;

namespace GestorInventarioBackend.Controllers
{
    public class PerifericoesController : ApiController
    {
        private RegistroContext db = new RegistroContext();

        // GET: api/Perifericoes
        public IQueryable<Periferico> GetPerifericos()
        {
            return db.Perifericos;
        }

        // GET: api/Perifericoes/5
        [ResponseType(typeof(Periferico))]
        public IHttpActionResult GetPeriferico(int id)
        {
            Periferico periferico = db.Perifericos.Find(id);
            if (periferico == null)
            {
                return NotFound();
            }

            return Ok(periferico);
        }

        // PUT: api/Perifericoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPeriferico(int id, Periferico periferico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != periferico.PerifericoId)
            {
                return BadRequest();
            }

            db.Entry(periferico).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerifericoExists(id))
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

        // POST: api/Perifericoes
        [ResponseType(typeof(Periferico))]
        public IHttpActionResult PostPeriferico(Periferico periferico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Perifericos.Add(periferico);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = periferico.PerifericoId }, periferico);
        }

        // DELETE: api/Perifericoes/5
        [ResponseType(typeof(Periferico))]
        public IHttpActionResult DeletePeriferico(int id)
        {
            Periferico periferico = db.Perifericos.Find(id);
            if (periferico == null)
            {
                return NotFound();
            }

            db.Perifericos.Remove(periferico);
            db.SaveChanges();

            return Ok(periferico);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PerifericoExists(int id)
        {
            return db.Perifericos.Count(e => e.PerifericoId == id) > 0;
        }
    }
}
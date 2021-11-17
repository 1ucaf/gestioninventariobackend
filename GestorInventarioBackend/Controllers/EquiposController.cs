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
    public class EquiposController : ApiController
    {
        private RegistroContext db = new RegistroContext();

        // GET: Equipos
        public IQueryable<object> GetEquipos()
        {
            return db.Equipos;
        }

        // GET: Equipos/5
        [ResponseType(typeof(Equipo))]
        public IHttpActionResult GetEquipo(int id)
        {
            Equipo equipo = db.Equipos.Find(id);
            if (equipo == null)
            {
                return NotFound();
            }

            return Ok(equipo);
        }

        // PUT: Equipos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEquipo(int id, Equipo equipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != equipo.EquipoId)
            {
                return BadRequest();
            }

            db.Entry(equipo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipoExists(id))
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

        // POST: Equipos
        [ResponseType(typeof(Equipo))]
        public IHttpActionResult PostEquipo(Equipo equipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Equipos.Add(equipo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = equipo.EquipoId }, equipo);
        }

        // DELETE: Equipos/5
        [ResponseType(typeof(Equipo))]
        public IHttpActionResult DeleteEquipo(int id)
        {
            Equipo equipo = db.Equipos.Find(id);
            if (equipo == null)
            {
                return NotFound();
            }

            db.Equipos.Remove(equipo);
            db.SaveChanges();

            return Ok(equipo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EquipoExists(int id)
        {
            return db.Equipos.Count(e => e.EquipoId == id) > 0;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using DataAccess;
using Modelo;

namespace GestorInventarioBackend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RegistroesController : ApiController
    {
        private RegistroContext db = new RegistroContext();

        // GET: api/Registroes
        [Authorize]
        public IQueryable<object> GetRegistros()
        {
            return db.Registros.Select(registro => new
            {
                RegistroId = registro.RegistroId,
                Descripcion = registro.Descripcion,
                Fecha = registro.Fecha,
                EquipoId = registro.EquipoId,
            });
        }

        // GET: api/Registroes/5
        [Authorize]
        [ResponseType(typeof(object))]
        public IHttpActionResult GetRegistro(int id)
        {
            Registro registro = db.Registros.Find(id);
            if (registro == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                RegistroId = registro.RegistroId,
                Descripcion = registro.Descripcion,
                Fecha = registro.Fecha,
                EquipoId = registro.EquipoId,
            });
        }

        // PUT: api/Registroes/5
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRegistro(int id, Registro registro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registro.RegistroId)
            {
                return BadRequest();
            }

            db.Entry(registro).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistroExists(id))
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

        // POST: api/Registroes
        [Authorize]
        [ResponseType(typeof(Registro))]
        public IHttpActionResult PostRegistro(Registro registro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Registros.Add(registro);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = registro.RegistroId }, registro);
        }

        // DELETE: api/Registroes/5
        [Authorize]
        [ResponseType(typeof(Registro))]
        public IHttpActionResult DeleteRegistro(int id)
        {
            Registro registro = db.Registros.Find(id);
            if (registro == null)
            {
                return NotFound();
            }

            db.Registros.Remove(registro);
            db.SaveChanges();

            return Ok(registro);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RegistroExists(int id)
        {
            return db.Registros.Count(e => e.RegistroId == id) > 0;
        }
    }
}
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
using GestorInventarioBackend.Utilities;
using Modelo;

namespace GestorInventarioBackend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OficinasController : ApiController
    {
        private RegistroContext db = new RegistroContext();

        // GET: api/Oficinas
        [Authorize]
        public List<object> GetOficinas()
        {
            try
            {
                List<object> oficinas = new List<object>();
                foreach(var oficina in db.Oficinas)
                {
                    var Fechas = new List<DateTime>();
                    foreach(var equipo in oficina.Equipos)
                    {
                        Fechas.Add(new DateTime(equipo.Adquisicion.Ticks));
                    }
                    var prom = Utils.GetPromedioDeAntiguedadPorOficina(Fechas);
                    oficinas.Add(new
                    {
                        OficinaId = oficina.OficinaId,
                        Nombre = oficina.Nombre,
                        PromAntEquipos = prom
                    });
                }
                return oficinas;
            } catch(Exception e)
            {
                return new List<object> { e };
            }
        }

        // GET: api/Oficinas/5
        [Authorize]
        [ResponseType(typeof(Oficina))]
        public IHttpActionResult GetOficina(int id)
        {
            Oficina oficina = db.Oficinas.Find(id);
            if (oficina == null)
            {
                return NotFound();
            }

            var Fechas = new List<DateTime>();
            foreach (var equipo in oficina.Equipos)
            {
                Fechas.Add(new DateTime(equipo.Adquisicion.Ticks));
            }
            var prom = Utils.GetPromedioDeAntiguedadPorOficina(Fechas);

            return Ok(
                new
                {
                    OficinaId = oficina.OficinaId,
                    Nombre = oficina.Nombre,
                    PromAntEquipos = prom
                }
            );
        }

        // PUT: api/Oficinas/5
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOficina(int id, Oficina oficina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != oficina.OficinaId)
            {
                return BadRequest();
            }

            db.Entry(oficina).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OficinaExists(id))
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

        // POST: api/Oficinas
        [Authorize]
        [ResponseType(typeof(Oficina))]
        public IHttpActionResult PostOficina(Oficina oficina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Oficinas.Add(oficina);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = oficina.OficinaId }, oficina);
        }

        // DELETE: api/Oficinas/5
        [Authorize]
        [ResponseType(typeof(Oficina))]
        public IHttpActionResult DeleteOficina(int id)
        {
            Oficina oficina = db.Oficinas.Find(id);
            if (oficina == null)
            {
                return NotFound();
            }

            db.Oficinas.Remove(oficina);
            db.SaveChanges();

            return Ok(oficina);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OficinaExists(int id)
        {
            return db.Oficinas.Count(e => e.OficinaId == id) > 0;
        }

    }
}
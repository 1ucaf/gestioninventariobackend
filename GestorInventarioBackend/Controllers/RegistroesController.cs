﻿using System;
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
    public class RegistroesController : ApiController
    {
        private RegistroContext db = new RegistroContext();

        // GET: api/Registroes
        public IQueryable<Registro> GetRegistros()
        {
            return db.Registros;
        }

        // GET: api/Registroes/5
        [ResponseType(typeof(Registro))]
        public IHttpActionResult GetRegistro(int id)
        {
            Registro registro = db.Registros.Find(id);
            if (registro == null)
            {
                return NotFound();
            }

            return Ok(registro);
        }

        // PUT: api/Registroes/5
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
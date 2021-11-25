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
    public class UsersController : ApiController
    {
        private RegistroContext db = new RegistroContext();

        // GET: api/Users
        [Authorize]
        public IQueryable<object> GetUsers()
        {
            return db.Users.Select(user => new
            {
                UserName = user.UserName,
                Email = user.Email,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Equipo = user.EquipoAsignado.Descripcion
            });
        }

        // GET: api/Users/5
        [Authorize]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                UserName = user.UserName,
                Email = user.Email,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                EquipoId = user.EquipoAsignado != null ? user.EquipoAsignado.EquipoId : null
            });
        }

        // PUT: api/Users/5
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(string id, UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserName)
            {
                return BadRequest();
            }

            Equipo equipo = db.Equipos.Where(equipo1 => equipo1.EquipoId == user.EquipoId).FirstOrDefault();

            User user1 = new User()
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                EquipoAsignado = equipo,
            };

            db.Entry(user1).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [Authorize]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Equipo equipo = db.Equipos.Where(equipo1 => equipo1.EquipoId == user.EquipoId).FirstOrDefault();

            User user1 = new User()
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                EquipoAsignado = equipo,
            };

            db.Users.Add(user1);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.UserName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user.UserName }, user);
        }

        // DELETE: api/Users/5
        [Authorize]
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(string id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.UserName == id) > 0;
        }

        public class UserDTO
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }

            public int EquipoId { get; set; }
        }
    }
}
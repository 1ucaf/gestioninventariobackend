using Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RegistroContext : DbContext
    {
        public RegistroContext() : base()
        {
            
        }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<User> Users{ get; set; }
        public DbSet<Registro> Registros { get; set; }
        public DbSet<Proveedor> Proveedores{ get; set; }
        public DbSet<Periferico> Perifericos{ get; set; }
        public DbSet<Oficina> Oficinas{ get; set; }




    }
}

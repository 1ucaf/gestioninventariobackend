using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Equipo
    {
        public int EquipoId { get; set; }
        public string Descripcion { get; set; }
        public DateTime Adquisicion { get; set; }
        public DateTime VencimientoGarantia { get; set; }

 /*       public virtual Proveedor Proveedor { get; set; }*/
        public int ProveedorId { get; set; }

        //public virtual List<Periferico> Perifericos { get; set; }

        //public virtual Oficina Oficina { get; set; }
        public int OficinaId { get; set; }

        //public virtual List<Registro> Registros { get; set; }

        //public virtual List<User> Users { get; set; }
    }
}

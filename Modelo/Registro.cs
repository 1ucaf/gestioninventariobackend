using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Registro
    {
        public int RegistroId { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }

        public virtual Equipo Equipo { get; set; }
        public int EquipoId { get; set; }

    }
}

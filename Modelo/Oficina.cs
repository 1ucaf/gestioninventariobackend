using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Oficina
    {
        public int OficinaId { get; set; }
        public string Nombre { get; set; }

        public virtual List<Equipo> Equipos { get; set; }
    }
}

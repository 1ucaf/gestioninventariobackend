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

        public double GetPromedioDeAntiguedadPorOficina()
        {
            var Hoy = DateTime.Now;
            var CantidadDeEquipos = Equipos.Count;
            TimeSpan Acumulador = TimeSpan.Zero;
            foreach(Equipo equipo in Equipos)
            {
                var dif = Hoy.Subtract(equipo.Adquisicion);
                Acumulador += dif;
            }
            return TimeSpan.FromMilliseconds(Acumulador.TotalMilliseconds / CantidadDeEquipos).TotalDays;
        }
    }
}

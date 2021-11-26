using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestorInventarioBackend.Utilities
{
    public class Utils
    {
        public static double GetPromedioDeAntiguedadPorOficina(List<DateTime> Fechas)
        {
            var Hoy = DateTime.Now;
            var CantidadDeEquipos = Fechas.Count == 0 ? 1 : Fechas.Count;
            TimeSpan Acumulador = TimeSpan.Zero;
            foreach (DateTime fecha in Fechas)
            {
                var dif = Hoy.Subtract(fecha);
                Acumulador += dif;
            }
            return TimeSpan.FromMilliseconds(Acumulador.TotalMilliseconds / CantidadDeEquipos).TotalDays;
        }
    }
}
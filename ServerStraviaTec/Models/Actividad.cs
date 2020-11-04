using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerStraviaTec.Models
{
    public class Actividad
    {
        [Key]
        public string ID { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string duracion { get; set; }
        public string tipo { get; set; }
        public string kilometraje { get; set; }
        public string recorrido { get; set; }
        public string completitud { get; set; }

        public Actividad()
        {
        }

        public Actividad(string NombreActividad, string FechaActividad, string horaActividad, string duracionActividad, string tipoActividad,
            string kilometrajeActividad, string recorridoActividad, string completitudActividad)
        {
            ID = NombreActividad;
            fecha = FechaActividad;
            hora = horaActividad;
            duracion = duracionActividad;
            tipo = tipoActividad;
            kilometraje = kilometrajeActividad;
            recorrido = recorridoActividad;
            completitud = completitudActividad;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerStraviaTec.Models
{
    public class listaActividad
    {
        [Key]
        public string idDeportista { get; set; }
        public string idActividad { get; set; }
        public string kilometrosActividad { get; set; }
        public string ascensoActividad { get; set; }
        public string completitudActividad { get; set; }
        public string gpxActividad { get; set; }
        public string tiempoActividad { get; set; }


        public listaActividad()
        {
        }

        public listaActividad(string id_Deportista, string id_Actividad, string kilometros, string ascenso, string completitud, string gpx, 
            string tiempo)
        {
            idDeportista = id_Deportista;
            idActividad = id_Actividad;
            kilometrosActividad = kilometros;
            ascensoActividad = ascenso;
            completitudActividad = completitud;
            gpxActividad = gpx;
            tiempoActividad = tiempo;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerStraviaTec.Models
{
    public class listaRetos
    {
        [Key]
        public string idDeportista { get; set; }
        public string idReto { get; set; }
        public string tipoReto { get; set; }
        public string gpxReto { get; set; }
        public string tiempoReto { get; set; }
        public string kilometrosReto { get; set; }
        public string ascensoReto { get; set; }
        public string completitudReto { get; set; }


        public listaRetos()
        {
        }

        public listaRetos(string id_Deportista, string id_Retos, string tipo, string ascenso, string completitud, string gpx,
            string tiempo, string kilometros)
        {
            idDeportista = id_Deportista;
            idReto = id_Retos;
            tipoReto = tipo;
            gpxReto = gpx;
            tiempoReto = tiempo;
            kilometrosReto = kilometros;
            ascensoReto = ascenso;
            completitudReto = completitud;
        }
    }
}

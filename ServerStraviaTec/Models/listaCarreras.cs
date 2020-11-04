using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerStraviaTec.Models
{
    public class listaCarreras
    {
        [Key]
        public string idDeportista { get; set; }
        public string idCarreras { get; set; }
        public string gpxCarrera { get; set; }
        public string tiempoCarrera { get; set; }
        public string kilometrosCarrera { get; set; }
        public string ascensoCarrera { get; set; }
        public string completitudCarrera { get; set; }
        
        


        public listaCarreras()
        {
        }

        public listaCarreras(string id_Deportista, string id_Carrera, string kilometros, string ascenso, string completitud, string gpx,
            string tiempo)
        {
            idDeportista = id_Deportista;
            idCarreras = id_Carrera;
            kilometrosCarrera = kilometros;
            ascensoCarrera = ascenso;
            completitudCarrera = completitud;
            gpxCarrera = gpx;
            tiempoCarrera = tiempo;
        }
    }
}

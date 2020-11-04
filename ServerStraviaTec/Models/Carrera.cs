using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerStraviaTec.Models
{
    public class Carrera
    {
        
        public string nombre { get; set; }
        public string privacidad { get; set; }
        public string fecha { get; set; }
        public string categoria { get; set; }
        public string cuentaBancaria { get; set; }
        public string listaPatrocinadores { get; set; }
        public string recorrido { get; set; }
        public string tipoActividad { get; set; }
        public string listaParticipantes { get; set; }
        public string solicitudAfiliaciones { get; set; }
        public string reporteCarrera { get; set; }
        public string costo { get; set; }
        [Key]
        public string idCarrera { get; set; }

        public Carrera()
        {
        }

        public Carrera(string NombreCarrera, string privacidadCarrera, string fechaCarrera, string categoriaCarrera,
            string cuentaCarrera, string patrocinadores, string recorridoCarrera, string costoCarrera, string idDeCarrera, string actividadCarrera,
            string lisitaParticipantesCarrera, string solicitudesCarrera, string reporteDeCarrera)
        {
            nombre = NombreCarrera;
            privacidad = privacidadCarrera;
            fecha = fechaCarrera;
            categoria = categoriaCarrera;
            cuentaBancaria = cuentaCarrera;
            listaPatrocinadores = patrocinadores;
            recorrido = recorridoCarrera;
            costo = costoCarrera;
            tipoActividad = actividadCarrera;
            listaParticipantes = lisitaParticipantesCarrera;
            solicitudAfiliaciones = solicitudesCarrera;
            reporteCarrera = reporteDeCarrera;
            idCarrera = idDeCarrera;
        }
    }
}

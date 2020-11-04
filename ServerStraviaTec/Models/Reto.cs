using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerStraviaTec.Models
{
    public class Reto
    {
        [Key]
        public string ID { get; set; }
        public string Nombre { get; set; }
        public string periodoDisponible { get; set; }
        public string listaPatrocinadores { get; set; }
        public string tipoReto { get; set; }
        public string tipoActividad { get; set; }
        public string Privacidad { get; set; }

        public Reto()
        {
        }

        public Reto(string idReto, string nombreReto, string periodoReto, string listaPatrocinadoresReto, string tipoDeReto, string tipoActividadReto,
            string privacidadReto)
        {
            ID = idReto;
            Nombre = nombreReto;
            periodoDisponible = periodoReto;
            listaPatrocinadores = listaPatrocinadoresReto;
            tipoReto = tipoDeReto;
            tipoActividad = tipoActividadReto;
            Privacidad = privacidadReto;
        }
    }
}

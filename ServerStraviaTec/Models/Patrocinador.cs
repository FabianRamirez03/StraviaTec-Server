using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerStraviaTec.Models
{
    public class Patrocinador
    {
        [Key]
        public string nombreComercio { get; set; }
        public string representanteLegal { get; set; }
        public string numeroTelefono { get; set; }
        public string logo { get; set; }

        public Patrocinador()
        {
        }

        public Patrocinador(string NombreComercio, string RepresentanteLegal, string NumeroTelefono, string Logo)
        {
            nombreComercio = NombreComercio;
            representanteLegal = RepresentanteLegal;
            numeroTelefono = NumeroTelefono;
            logo = Logo;
        }
    }
}

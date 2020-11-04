using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerStraviaTec.Models
{
    public class solicitudAfiliacion
    {
        [Key]
        public string idAfiliacion { get; set; }
        public string deportista { get; set; }
        public string comprobante { get; set; }
        public string idCarrera { get; set; }

        public solicitudAfiliacion()
        {
        }

        public solicitudAfiliacion(string Deportista, string id_Afiliacion, string comprobanteDeportista, string idCarreraDeportista)
        {
            idAfiliacion = id_Afiliacion;
            deportista = Deportista;
            comprobante = comprobanteDeportista;
            idCarrera = idCarreraDeportista;
        }
    }
}

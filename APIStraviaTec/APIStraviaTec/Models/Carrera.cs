using System;
using System.Collections.Generic;

namespace APIStraviaTec.Models
{
    public partial class Carrera
    {
        public int Idcarrera { get; set; }
        public int Idorganizador { get; set; }
        public string Nombrecarrera { get; set; }
        public DateTime Fechacarrera { get; set; }
        public string Tipoactividad { get; set; }
        public string Recorrido { get; set; }
        public bool? Privada { get; set; }
        public int Costo { get; set; }
        public string Cuentabancaria { get; set; }
        public string Categoria { get; set; }

        public virtual Usuario IdorganizadorNavigation { get; set; }
    }
}

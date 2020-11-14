using System;
using System.Collections.Generic;

namespace APIStraviaTec.Models
{
    public partial class Reto
    {
        public int Idreto { get; set; }
        public int Idorganizador { get; set; }
        public string Nombrereto { get; set; }
        public string Objetivoreto { get; set; }
        public string Tipoactividad { get; set; }
        public string Tiporeto { get; set; }
        public bool? Privada { get; set; }
        public DateTime? Fechainicio { get; set; }
        public DateTime? Fechafinaliza { get; set; }

        public virtual Usuario IdorganizadorNavigation { get; set; }
    }
}

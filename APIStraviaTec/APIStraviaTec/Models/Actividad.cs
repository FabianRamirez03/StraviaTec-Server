using System;
using System.Collections.Generic;

namespace APIStraviaTec.Models
{
    public partial class Actividad
    {
        public int Idactividad { get; set; }
        public string Nombreactividad { get; set; }
        public string Tipoactividad { get; set; }
        public DateTime? Fecha { get; set; }
    }
}

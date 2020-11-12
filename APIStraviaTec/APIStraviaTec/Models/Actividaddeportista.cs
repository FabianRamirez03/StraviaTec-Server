using System;
using System.Collections.Generic;

namespace APIStraviaTec.Models
{
    public partial class Actividaddeportista
    {
        public int Idactividad { get; set; }
        public int Iddeportista { get; set; }
        public string Kilometraje { get; set; }
        public string Altura { get; set; }
        public string Recorrido { get; set; }
        public string Duracion { get; set; }
    }
}

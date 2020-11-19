using System;
using System.Collections.Generic;

namespace APIStraviaTec.Models
{
    public partial class Actividaddeportista
    {
        public string Nombreactividad { get; set; }
        public DateTime? Fecha { get; set; }
        public string Tipoactividad { get; set; }
        public int Idactividad { get; set; }
        public int Iddeportista { get; set; }
        public string Kilometraje { get; set; }
        public string Altura { get; set; }
        public string Recorrido { get; set; }
        public string Duracion { get; set; }
        public string Mapa { get; set; }
    }
}

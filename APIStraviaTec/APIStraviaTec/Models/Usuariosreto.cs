using System;
using System.Collections.Generic;

namespace APIStraviaTec.Models
{
    public partial class Usuariosreto
    {
        public int Iddeportista { get; set; }
        public int Idreto { get; set; }
        public string Duracion { get; set; }
        public string Kilometraje { get; set; }
        public string Altura { get; set; }
        public bool? Completitud { get; set; }
        public string Recorrido { get; set; }
    }
}

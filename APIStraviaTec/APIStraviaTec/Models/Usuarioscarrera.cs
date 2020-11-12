using System;
using System.Collections.Generic;

namespace APIStraviaTec.Models
{
    public partial class Usuarioscarrera
    {
        public int Iddeportista { get; set; }
        public int Idcarrera { get; set; }
        public byte[] Recibo { get; set; }
        public string Tiemporegistrado { get; set; }
        public string Kilometraje { get; set; }
        public string Altura { get; set; }
        public bool? Completitud { get; set; }
        public string Recorrido { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace APIStraviaTec.Models
{
    public partial class Solicitudescarrera
    {
        public int Idcarrera { get; set; }
        public int Idusuario { get; set; }
        public string Recibo { get; set; }
        public string Categoria { get; set; }
    }
}

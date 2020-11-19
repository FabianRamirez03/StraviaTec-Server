using System;
using System.Collections.Generic;

namespace APIStraviaTec.Models
{
    public partial class Solicitudescarrera
    {
        public int Idcarrera { get; set; }
        public int Idusuario { get; set; }
        public byte[] Recibo { get; set; }
        public string Categoria { get; set; }
    }
}

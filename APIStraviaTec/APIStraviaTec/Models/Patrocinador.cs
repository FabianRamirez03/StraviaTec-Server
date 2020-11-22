using System;
using System.Collections.Generic;

namespace APIStraviaTec.Models
{
    public partial class Patrocinador
    {
        public string Nombrecomercial { get; set; }
        public string Representante { get; set; }
        public string Numerotelefono { get; set; }
        public byte[] Logo { get; set; }
    }
}

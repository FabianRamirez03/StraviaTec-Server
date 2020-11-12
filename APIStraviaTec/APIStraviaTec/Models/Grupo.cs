using System;
using System.Collections.Generic;

namespace APIStraviaTec.Models
{
    public partial class Grupo
    {
        public string Nombre { get; set; }
        public int Idadministrador { get; set; }

        public virtual Usuario IdadministradorNavigation { get; set; }
    }
}

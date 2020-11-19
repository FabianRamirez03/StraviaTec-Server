using System;
using System.Collections.Generic;

namespace APIStraviaTec.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Carrera = new HashSet<Carrera>();
            Grupo = new HashSet<Grupo>();
            Reto = new HashSet<Reto>();
        }

        public int Idusuario { get; set; }
        public string Nombreusuario { get; set; }
        public string Contrasena { get; set; }
        public string Primernombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime Fechanacimiento { get; set; }
        public string Nacionalidad { get; set; }
        public string Foto { get; set; }
        public int? Edad { get; set; }
        public string Categoria { get; set; }
        public int Administra { get; set; }

        public virtual ICollection<Carrera> Carrera { get; set; }
        public virtual ICollection<Grupo> Grupo { get; set; }
        public virtual ICollection<Reto> Reto { get; set; }
    }
}

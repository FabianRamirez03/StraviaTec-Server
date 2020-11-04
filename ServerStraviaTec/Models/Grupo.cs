using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerStraviaTec.Models
{
    public class Grupo
    {
        [Key]
        public string nombreGrupo { get; set; }
        public string listaDeportistas { get; set; }
        public string administrador { get; set; }

        public Grupo()
        {
        }

        public Grupo(string NombreGrupo, string ListaDeportistas, string Administrador)
        {
            nombreGrupo = NombreGrupo;
            listaDeportistas = ListaDeportistas;
            administrador = Administrador;
        }
    }
}

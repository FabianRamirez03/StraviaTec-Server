using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerStraviaTec.Models
{
    public class Deportista
    {
        [Key]
        public int Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }

        public Deportista()
        {
        }
        
        public Deportista(string NombreDeportista, string ApellidoDeportista, string UsuarioDeportista, string PasswordDeportista, int CedulaDeportista)
        {
            Cedula = CedulaDeportista;
            Apellido = ApellidoDeportista;
            Usuario = UsuarioDeportista;
            Password = PasswordDeportista;
            Cedula = CedulaDeportista;

        }
    }
}

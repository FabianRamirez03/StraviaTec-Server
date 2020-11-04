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
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
        public string FechaNacimiento { get; set; }
        public string Nacionalidad { get; set; }
        public string Foto { get; set; }
        public string ListaAmigos { get; set; }
        public string Categorias { get; set; }

        public Deportista()
        {
        }
        
        public Deportista(string NombreDeportista, string ApellidoDeportista, string UsuarioDeportista, string PasswordDeportista, 
            string fechaDeportista, string nacionalidadDeportista, string fotoDeportista, string amigosDeportista, string categoriasDeportista)
        {
            Nombre = NombreDeportista;
            Apellido = ApellidoDeportista;
            Usuario = UsuarioDeportista;
            Password = PasswordDeportista;
            FechaNacimiento = fechaDeportista;
            Nacionalidad = nacionalidadDeportista;
            Foto = fotoDeportista;
            ListaAmigos = amigosDeportista;
            Categorias = categoriasDeportista;
        }
    }
}

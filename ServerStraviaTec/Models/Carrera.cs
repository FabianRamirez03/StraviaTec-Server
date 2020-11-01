using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerStraviaTec.Models
{
    public class Carrera
    {
        
        public string nombre { get; set; }
        public string privacidad { get; set; }
        public string fecha { get; set; }
        public string categoria { get; set; }
        public string cuentaBancarias { get; set; }
        public string listaPatrocinadores { get; set; }
        public int costo { get; set; }
        [Key]
        public int id { get; set; }

        public Carrera()
        {
        }

        public Carrera(string NombreCarrera, string privacidadCarrera, string fechaCarrera, string categoriaCarrera,
            string cuentaCarrera, string patrocinadores, int costoCarrera, int idCarrera)
        {
            nombre = NombreCarrera;
            privacidad = privacidadCarrera;
            fecha = fechaCarrera;
            categoria = categoriaCarrera;
            cuentaBancarias = cuentaCarrera;
            listaPatrocinadores = patrocinadores;
            costo = costoCarrera;
            id = idCarrera;
        }
    }
}

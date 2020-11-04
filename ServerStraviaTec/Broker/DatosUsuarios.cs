using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerStraviaTec.Models;

namespace ServerStraviaTec.Clases
{
    public class DatosUsuarios : DbContext
    {
        public DatosUsuarios (DbContextOptions <DatosUsuarios> options) : base(options) => this.Database.Migrate();

        public DbSet<Deportista> Deportista { get; set; }

        public DbSet<ServerStraviaTec.Models.Administrador> Administrador { get; set; }

        public DbSet<ServerStraviaTec.Models.Carrera> Carrera { get; set; }

        public DbSet<ServerStraviaTec.Models.Grupo> Grupo { get; set; }

        public DbSet<ServerStraviaTec.Models.solicitudAfiliacion> solicitudAfiliacion { get; set; }

        public DbSet<ServerStraviaTec.Models.Actividad> Actividad { get; set; }

        public DbSet<ServerStraviaTec.Models.Patrocinador> Patrocinador { get; set; }

        public DbSet<ServerStraviaTec.Models.Reto> Reto { get; set; }

        public DbSet<ServerStraviaTec.Models.listaActividad> listaActividad { get; set; }

        public DbSet<ServerStraviaTec.Models.listaCarreras> listaCarreras { get; set; }

        public DbSet<ServerStraviaTec.Models.listaRetos> listaRetos { get; set; }

    }
}

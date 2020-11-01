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
    }
}

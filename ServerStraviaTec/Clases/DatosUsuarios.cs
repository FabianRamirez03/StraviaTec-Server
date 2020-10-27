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
        public DatosUsuarios (DbContextOptions <DatosUsuarios> options) : base(options) { }

        public DbSet<Deportista> Deportista { get; set; }
    }
}

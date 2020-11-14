using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIStraviaTec.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace APIStraviaTec.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private basedatosstraviatecContext db = new basedatosstraviatecContext();
        /*
                [HttpGet]
                public IEnumerable<Usuario> Get()
                {
                    using (var context = new basedatosstraviatecContext())
                    {
                        //Obtener todos los usuarios
                        return context.Usuario.ToList();
                    }
                }
        */
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            basedatosstraviatecContext db = new basedatosstraviatecContext();
            db.

        }
    }
}

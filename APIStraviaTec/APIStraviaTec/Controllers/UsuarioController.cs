using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIStraviaTec.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace APIStraviaTec.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            using (var context = new basedatosstraviatecContext())
            {
                //Obtener todos los usuarios
                return context.Usuario.ToList();
            }
        }
    }
}

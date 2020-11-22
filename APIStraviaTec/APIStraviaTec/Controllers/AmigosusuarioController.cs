using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIStraviaTec.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIStraviaTec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmigosusuarioController : ControllerBase
    {
        private basedatosstraviatecContext db = new basedatosstraviatecContext();
        // GET: api/<FriendsSearchController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FriendsSearchController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FriendsSearchController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FriendsSearchController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FriendsSearchController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

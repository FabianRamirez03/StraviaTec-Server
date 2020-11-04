using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerStraviaTec.Clases;
using ServerStraviaTec.Models;

namespace ServerStraviaTec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatrocinadorsController : ControllerBase
    {
        private readonly DatosUsuarios _context;

        public PatrocinadorsController(DatosUsuarios context)
        {
            _context = context;
        }

        // GET: api/Patrocinadors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patrocinador>>> GetPatrocinador()
        {
            return await _context.Patrocinador.ToListAsync();
        }

        // GET: api/Patrocinadors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patrocinador>> GetPatrocinador(string id)
        {
            var patrocinador = await _context.Patrocinador.FindAsync(id);

            if (patrocinador == null)
            {
                return NotFound();
            }

            return patrocinador;
        }

        // PUT: api/Patrocinadors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatrocinador(string id, Patrocinador patrocinador)
        {
            if (id != patrocinador.nombreComercio)
            {
                return BadRequest();
            }

            _context.Entry(patrocinador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatrocinadorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Patrocinadors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Patrocinador>> PostPatrocinador(Patrocinador patrocinador)
        {
            _context.Patrocinador.Add(patrocinador);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PatrocinadorExists(patrocinador.nombreComercio))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPatrocinador", new { id = patrocinador.nombreComercio }, patrocinador);
        }

        // DELETE: api/Patrocinadors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Patrocinador>> DeletePatrocinador(string id)
        {
            var patrocinador = await _context.Patrocinador.FindAsync(id);
            if (patrocinador == null)
            {
                return NotFound();
            }

            _context.Patrocinador.Remove(patrocinador);
            await _context.SaveChangesAsync();

            return patrocinador;
        }

        private bool PatrocinadorExists(string id)
        {
            return _context.Patrocinador.Any(e => e.nombreComercio == id);
        }
    }
}

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
    public class listaRetosController : ControllerBase
    {
        private readonly DatosUsuarios _context;

        public listaRetosController(DatosUsuarios context)
        {
            _context = context;
        }

        // GET: api/listaRetos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<listaRetos>>> GetlistaRetos()
        {
            return await _context.listaRetos.ToListAsync();
        }

        // GET: api/listaRetos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<listaRetos>> GetlistaRetos(string id)
        {
            var listaRetos = await _context.listaRetos.FindAsync(id);

            if (listaRetos == null)
            {
                return NotFound();
            }

            return listaRetos;
        }

        // PUT: api/listaRetos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutlistaRetos(string id, listaRetos listaRetos)
        {
            if (id != listaRetos.idDeportista)
            {
                return BadRequest();
            }

            _context.Entry(listaRetos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!listaRetosExists(id))
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

        // POST: api/listaRetos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<listaRetos>> PostlistaRetos(listaRetos listaRetos)
        {
            _context.listaRetos.Add(listaRetos);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (listaRetosExists(listaRetos.idDeportista))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetlistaRetos", new { id = listaRetos.idDeportista }, listaRetos);
        }

        // DELETE: api/listaRetos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<listaRetos>> DeletelistaRetos(string id)
        {
            var listaRetos = await _context.listaRetos.FindAsync(id);
            if (listaRetos == null)
            {
                return NotFound();
            }

            _context.listaRetos.Remove(listaRetos);
            await _context.SaveChangesAsync();

            return listaRetos;
        }

        private bool listaRetosExists(string id)
        {
            return _context.listaRetos.Any(e => e.idDeportista == id);
        }
    }
}

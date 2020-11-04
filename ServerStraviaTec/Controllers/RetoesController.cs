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
    public class RetoesController : ControllerBase
    {
        private readonly DatosUsuarios _context;

        public RetoesController(DatosUsuarios context)
        {
            _context = context;
        }

        // GET: api/Retoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reto>>> GetReto()
        {
            return await _context.Reto.ToListAsync();
        }

        // GET: api/Retoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reto>> GetReto(string id)
        {
            var reto = await _context.Reto.FindAsync(id);

            if (reto == null)
            {
                return NotFound();
            }

            return reto;
        }

        // PUT: api/Retoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReto(string id, Reto reto)
        {
            if (id != reto.ID)
            {
                return BadRequest();
            }

            _context.Entry(reto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RetoExists(id))
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

        // POST: api/Retoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Reto>> PostReto(Reto reto)
        {
            _context.Reto.Add(reto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RetoExists(reto.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetReto", new { id = reto.ID }, reto);
        }

        // DELETE: api/Retoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reto>> DeleteReto(string id)
        {
            var reto = await _context.Reto.FindAsync(id);
            if (reto == null)
            {
                return NotFound();
            }

            _context.Reto.Remove(reto);
            await _context.SaveChangesAsync();

            return reto;
        }

        private bool RetoExists(string id)
        {
            return _context.Reto.Any(e => e.ID == id);
        }
    }
}

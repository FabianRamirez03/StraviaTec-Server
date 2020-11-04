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
    public class listaCarrerasController : ControllerBase
    {
        private readonly DatosUsuarios _context;

        public listaCarrerasController(DatosUsuarios context)
        {
            _context = context;
        }

        // GET: api/listaCarreras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<listaCarreras>>> GetlistaCarreras()
        {
            return await _context.listaCarreras.ToListAsync();
        }

        // GET: api/listaCarreras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<listaCarreras>> GetlistaCarreras(string id)
        {
            var listaCarreras = await _context.listaCarreras.FindAsync(id);

            if (listaCarreras == null)
            {
                return NotFound();
            }

            return listaCarreras;
        }

        // PUT: api/listaCarreras/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutlistaCarreras(string id, listaCarreras listaCarreras)
        {
            if (id != listaCarreras.idDeportista)
            {
                return BadRequest();
            }

            _context.Entry(listaCarreras).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!listaCarrerasExists(id))
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

        // POST: api/listaCarreras
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<listaCarreras>> PostlistaCarreras(listaCarreras listaCarreras)
        {
            _context.listaCarreras.Add(listaCarreras);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (listaCarrerasExists(listaCarreras.idDeportista))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetlistaCarreras", new { id = listaCarreras.idDeportista }, listaCarreras);
        }

        // DELETE: api/listaCarreras/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<listaCarreras>> DeletelistaCarreras(string id)
        {
            var listaCarreras = await _context.listaCarreras.FindAsync(id);
            if (listaCarreras == null)
            {
                return NotFound();
            }

            _context.listaCarreras.Remove(listaCarreras);
            await _context.SaveChangesAsync();

            return listaCarreras;
        }

        private bool listaCarrerasExists(string id)
        {
            return _context.listaCarreras.Any(e => e.idDeportista == id);
        }
    }
}

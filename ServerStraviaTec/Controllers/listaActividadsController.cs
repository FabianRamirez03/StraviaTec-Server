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
    public class listaActividadsController : ControllerBase
    {
        private readonly DatosUsuarios _context;

        public listaActividadsController(DatosUsuarios context)
        {
            _context = context;
        }

        // GET: api/listaActividads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<listaActividad>>> GetlistaActividad()
        {
            return await _context.listaActividad.ToListAsync();
        }

        // GET: api/listaActividads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<listaActividad>> GetlistaActividad(string id)
        {
            var listaActividad = await _context.listaActividad.FindAsync(id);

            if (listaActividad == null)
            {
                return NotFound();
            }

            return listaActividad;
        }

        // PUT: api/listaActividads/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutlistaActividad(string id, listaActividad listaActividad)
        {
            if (id != listaActividad.idDeportista)
            {
                return BadRequest();
            }

            _context.Entry(listaActividad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!listaActividadExists(id))
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

        // POST: api/listaActividads
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<listaActividad>> PostlistaActividad(listaActividad listaActividad)
        {
            _context.listaActividad.Add(listaActividad);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (listaActividadExists(listaActividad.idDeportista))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetlistaActividad", new { id = listaActividad.idDeportista }, listaActividad);
        }

        // DELETE: api/listaActividads/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<listaActividad>> DeletelistaActividad(string id)
        {
            var listaActividad = await _context.listaActividad.FindAsync(id);
            if (listaActividad == null)
            {
                return NotFound();
            }

            _context.listaActividad.Remove(listaActividad);
            await _context.SaveChangesAsync();

            return listaActividad;
        }

        private bool listaActividadExists(string id)
        {
            return _context.listaActividad.Any(e => e.idDeportista == id);
        }
    }
}

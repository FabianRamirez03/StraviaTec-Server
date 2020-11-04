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
    public class ActividadController : ControllerBase
    {
        private readonly DatosUsuarios _context;

        public ActividadController(DatosUsuarios context)
        {
            _context = context;
        }

        // GET: api/Actividad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actividad>>> GetActividad()
        {
            return await _context.Actividad.ToListAsync();
        }

        // GET: api/Actividad/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Actividad>> GetActividad(string id)
        {
            var actividad = await _context.Actividad.FindAsync(id);

            if (actividad == null)
            {
                return NotFound();
            }

            return actividad;
        }

        // PUT: api/Actividad/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActividad(string id, Actividad actividad)
        {
            if (id != actividad.ID)
            {
                return BadRequest();
            }

            _context.Entry(actividad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActividadExists(id))
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

        // POST: api/Actividad
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Actividad>> PostActividad(Actividad actividad)
        {
            _context.Actividad.Add(actividad);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ActividadExists(actividad.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetActividad", new { id = actividad.ID }, actividad);
        }

        // DELETE: api/Actividad/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Actividad>> DeleteActividad(string id)
        {
            var actividad = await _context.Actividad.FindAsync(id);
            if (actividad == null)
            {
                return NotFound();
            }

            _context.Actividad.Remove(actividad);
            await _context.SaveChangesAsync();

            return actividad;
        }

        private bool ActividadExists(string id)
        {
            return _context.Actividad.Any(e => e.ID == id);
        }
    }
}

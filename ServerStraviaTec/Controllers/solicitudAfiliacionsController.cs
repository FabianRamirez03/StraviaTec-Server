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
    public class solicitudAfiliacionsController : ControllerBase
    {
        private readonly DatosUsuarios _context;

        public solicitudAfiliacionsController(DatosUsuarios context)
        {
            _context = context;
        }

        // GET: api/solicitudAfiliacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<solicitudAfiliacion>>> GetsolicitudAfiliacion()
        {
            return await _context.solicitudAfiliacion.ToListAsync();
        }

        // GET: api/solicitudAfiliacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<solicitudAfiliacion>> GetsolicitudAfiliacion(string id)
        {
            var solicitudAfiliacion = await _context.solicitudAfiliacion.FindAsync(id);

            if (solicitudAfiliacion == null)
            {
                return NotFound();
            }

            return solicitudAfiliacion;
        }

        // PUT: api/solicitudAfiliacions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutsolicitudAfiliacion(string id, solicitudAfiliacion solicitudAfiliacion)
        {
            if (id != solicitudAfiliacion.idAfiliacion)
            {
                return BadRequest();
            }

            _context.Entry(solicitudAfiliacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!solicitudAfiliacionExists(id))
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

        // POST: api/solicitudAfiliacions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<solicitudAfiliacion>> PostsolicitudAfiliacion(solicitudAfiliacion solicitudAfiliacion)
        {
            _context.solicitudAfiliacion.Add(solicitudAfiliacion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (solicitudAfiliacionExists(solicitudAfiliacion.idAfiliacion))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetsolicitudAfiliacion", new { id = solicitudAfiliacion.idAfiliacion }, solicitudAfiliacion);
        }

        // DELETE: api/solicitudAfiliacions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<solicitudAfiliacion>> DeletesolicitudAfiliacion(string id)
        {
            var solicitudAfiliacion = await _context.solicitudAfiliacion.FindAsync(id);
            if (solicitudAfiliacion == null)
            {
                return NotFound();
            }

            _context.solicitudAfiliacion.Remove(solicitudAfiliacion);
            await _context.SaveChangesAsync();

            return solicitudAfiliacion;
        }

        private bool solicitudAfiliacionExists(string id)
        {
            return _context.solicitudAfiliacion.Any(e => e.idAfiliacion == id);
        }
    }
}

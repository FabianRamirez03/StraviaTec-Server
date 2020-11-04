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
    public class GruposController : ControllerBase
    {
        private readonly DatosUsuarios _context;

        public GruposController(DatosUsuarios context)
        {
            _context = context;
        }

        // GET: api/Grupos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grupo>>> GetGrupo()
        {
            return await _context.Grupo.ToListAsync();
        }

        // GET: api/Grupos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Grupo>> GetGrupo(string id)
        {
            var grupo = await _context.Grupo.FindAsync(id);

            if (grupo == null)
            {
                return NotFound();
            }

            return grupo;
        }

        // PUT: api/Grupos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrupo(string id, Grupo grupo)
        {
            if (id != grupo.nombreGrupo)
            {
                return BadRequest();
            }

            _context.Entry(grupo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GrupoExists(id))
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

        // POST: api/Grupos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Grupo>> PostGrupo(Grupo grupo)
        {
            _context.Grupo.Add(grupo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GrupoExists(grupo.nombreGrupo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGrupo", new { id = grupo.nombreGrupo }, grupo);
        }

        // DELETE: api/Grupos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Grupo>> DeleteGrupo(string id)
        {
            var grupo = await _context.Grupo.FindAsync(id);
            if (grupo == null)
            {
                return NotFound();
            }

            _context.Grupo.Remove(grupo);
            await _context.SaveChangesAsync();

            return grupo;
        }

        private bool GrupoExists(string id)
        {
            return _context.Grupo.Any(e => e.nombreGrupo == id);
        }
    }
}

﻿using System;
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
    public class DeportistasController : ControllerBase
    {
        private readonly DatosUsuarios _context;

        public DeportistasController(DatosUsuarios context)
        {
            _context = context;
        }

        // GET: api/Deportistas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Deportista>>> GetDeportista()
        {
            return await _context.Deportista.ToListAsync();
        }

        // GET: api/Deportistas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Deportista>> GetDeportista(string id)
        {
            var deportista = await _context.Deportista.FindAsync(id);

            if (deportista == null)
            {
                return NotFound();
            }

            return deportista;
        }

        // PUT: api/Deportistas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeportista(string id, Deportista deportista)
        {
            if (id != deportista.Usuario)
            {
                return BadRequest();
            }

            _context.Entry(deportista).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeportistaExists(id))
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

        // POST: api/Deportistas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Deportista>> PostDeportista(Deportista deportista)
        {
            _context.Deportista.Add(deportista);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DeportistaExists(deportista.Usuario))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDeportista", new { id = deportista.Usuario }, deportista);
        }

        // DELETE: api/Deportistas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Deportista>> DeleteDeportista(string id)
        {
            var deportista = await _context.Deportista.FindAsync(id);
            if (deportista == null)
            {
                return NotFound();
            }

            _context.Deportista.Remove(deportista);
            await _context.SaveChangesAsync();

            return deportista;
        }

        private bool DeportistaExists(string id)
        {
            return _context.Deportista.Any(e => e.Usuario == id);
        }
    }
}

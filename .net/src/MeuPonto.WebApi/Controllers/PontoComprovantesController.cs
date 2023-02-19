using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Data;
using MeuPonto.Models;

namespace MeuPonto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PontoComprovantesController : ControllerBase
    {
        private readonly MeuPontoDbContext _db;

        public PontoComprovantesController(MeuPontoDbContext db)
        {
            _db = db;
        }

        // GET: api/PontoComprovantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PontoComprovante>>> GetPontoComprovantes()
        {
            return await _db.PontoComprovantes.ToListAsync();
        }

        // GET: api/PontoComprovantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PontoComprovante>> GetPontoComprovante(int? id)
        {
            var pontoComprovante = await _db.PontoComprovantes.FindAsync(id);

            if (pontoComprovante == null)
            {
                return NotFound();
            }

            return pontoComprovante;
        }

        // PUT: api/PontoComprovantes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPontoComprovante(int? id, PontoComprovante pontoComprovante)
        {
            if (id != pontoComprovante.Id)
            {
                return BadRequest();
            }

            _db.Entry(pontoComprovante).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PontoComprovanteExists(id))
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

        // POST: api/PontoComprovantes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PontoComprovante>> PostPontoComprovante(PontoComprovante pontoComprovante)
        {
            _db.PontoComprovantes.Add(pontoComprovante);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetPontoComprovante", new { id = pontoComprovante.Id }, pontoComprovante);
        }

        // DELETE: api/PontoComprovantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePontoComprovante(int? id)
        {
            var pontoComprovante = await _db.PontoComprovantes.FindAsync(id);
            if (pontoComprovante == null)
            {
                return NotFound();
            }

            _db.PontoComprovantes.Remove(pontoComprovante);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool PontoComprovanteExists(int? id)
        {
            return _db.PontoComprovantes.Any(e => e.Id == id);
        }
    }
}

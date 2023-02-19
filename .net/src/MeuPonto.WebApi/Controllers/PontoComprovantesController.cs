using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Data;
using MeuPonto.Models;

namespace MeuPonto.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PontoComprovantesController : ControllerBase
{
    private readonly MeuPontoDbContext _db;

    public PontoComprovantesController(MeuPontoDbContext db)
    {
        _db = db;
    }

    // GET: api/Comprovantes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comprovante>>> GetPontoComprovantes()
    {
        return await _db.Comprovantes.ToListAsync();
    }

    // GET: api/Comprovantes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Comprovante>> GetPontoComprovante(int? id)
    {
        var comprovante = await _db.Comprovantes.FindAsync(id);

        if (comprovante == null)
        {
            return NotFound();
        }

        return comprovante;
    }

    // PUT: api/Comprovantes/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPontoComprovante(int? id, Comprovante comprovante)
    {
        if (id != comprovante.Id)
        {
            return BadRequest();
        }

        _db.Entry(comprovante).State = EntityState.Modified;

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

    // POST: api/Comprovantes
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Comprovante>> PostPontoComprovante(Comprovante comprovante)
    {
        _db.Comprovantes.Add(comprovante);
        await _db.SaveChangesAsync();

        return CreatedAtAction("GetPontoComprovante", new { id = comprovante.Id }, comprovante);
    }

    // DELETE: api/Comprovantes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePontoComprovante(int? id)
    {
        var comprovante = await _db.Comprovantes.FindAsync(id);
        if (comprovante == null)
        {
            return NotFound();
        }

        _db.Comprovantes.Remove(comprovante);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    private bool PontoComprovanteExists(int? id)
    {
        return _db.Comprovantes.Any(e => e.Id == id);
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Data;
using MeuPonto.Models;

namespace MeuPonto.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PontosController : ControllerBase
{
    private readonly MeuPontoDbContext _db;

    public PontosController(MeuPontoDbContext db)
    {
        _db = db;
    }

    // GET: api/Pontos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ponto>>> GetPontos()
    {
        return await _db.Pontos.ToListAsync();
    }

    // GET: api/Pontos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Ponto>> GetPonto(int? id)
    {
        var ponto = await _db.Pontos.FindAsync(id);

        if (ponto == null)
        {
            return NotFound();
        }

        return ponto;
    }

    // PUT: api/Pontos/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPonto(int? id, Ponto ponto)
    {
        if (id != ponto.Id)
        {
            return BadRequest();
        }

        _db.Entry(ponto).State = EntityState.Modified;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PontoExists(id))
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

    // POST: api/Pontos
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Ponto>> PostPonto(Ponto ponto)
    {
        _db.Pontos.Add(ponto);
        await _db.SaveChangesAsync();

        return CreatedAtAction("GetPonto", new { id = ponto.Id }, ponto);
    }

    // DELETE: api/Pontos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePonto(int? id)
    {
        var ponto = await _db.Pontos.FindAsync(id);
        if (ponto == null)
        {
            return NotFound();
        }

        _db.Pontos.Remove(ponto);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    private bool PontoExists(int? id)
    {
        return _db.Pontos.Any(e => e.Id == id);
    }
}

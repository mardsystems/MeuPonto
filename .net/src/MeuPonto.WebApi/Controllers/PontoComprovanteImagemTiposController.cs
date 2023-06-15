using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Data;
using MeuPonto.Models;
using MeuPonto.Modules.Pontos.Comprovantes;

namespace MeuPonto.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PontoComprovanteImagemTiposController : ControllerBase
{
    private readonly MeuPontoDbContext _db;

    public PontoComprovanteImagemTiposController(MeuPontoDbContext db)
    {
        _db = db;
    }

    // GET: api/PontoComprovanteImagemTipos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoImagem>>> GetPontoComprovanteImagemTipos()
    {
        return await _db.PontoComprovanteImagemTipos.ToListAsync();
    }

    // GET: api/PontoComprovanteImagemTipos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TipoImagem>> GetPontoComprovanteImagemTipo(int id)
    {
        var pontoComprovanteImagemTipo = await _db.PontoComprovanteImagemTipos.FindAsync(id);

        if (pontoComprovanteImagemTipo == null)
        {
            return NotFound();
        }

        return pontoComprovanteImagemTipo;
    }

    // PUT: api/PontoComprovanteImagemTipos/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPontoComprovanteImagemTipo(TipoImagemEnum id, TipoImagem pontoComprovanteImagemTipo)
    {
        if (id != pontoComprovanteImagemTipo.Id)
        {
            return BadRequest();
        }

        _db.Entry(pontoComprovanteImagemTipo).State = EntityState.Modified;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PontoComprovanteImagemTipoExists(id))
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

    // POST: api/PontoComprovanteImagemTipos
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TipoImagem>> PostPontoComprovanteImagemTipo(TipoImagem pontoComprovanteImagemTipo)
    {
        _db.PontoComprovanteImagemTipos.Add(pontoComprovanteImagemTipo);
        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (PontoComprovanteImagemTipoExists(pontoComprovanteImagemTipo.Id))
            {
                return Conflict();
            }
            else
            {
                throw;
            }
        }

        return CreatedAtAction("GetPontoComprovanteImagemTipo", new { id = pontoComprovanteImagemTipo.Id }, pontoComprovanteImagemTipo);
    }

    // DELETE: api/PontoComprovanteImagemTipos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePontoComprovanteImagemTipo(int id)
    {
        var pontoComprovanteImagemTipo = await _db.PontoComprovanteImagemTipos.FindAsync(id);
        if (pontoComprovanteImagemTipo == null)
        {
            return NotFound();
        }

        _db.PontoComprovanteImagemTipos.Remove(pontoComprovanteImagemTipo);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    private bool PontoComprovanteImagemTipoExists(TipoImagemEnum id)
    {
        return _db.PontoComprovanteImagemTipos.Any(e => e.Id == id);
    }
}

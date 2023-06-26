using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Data;
using MeuPonto.Models;

namespace MeuPonto.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PerfisController : ControllerBase
{
    private readonly MeuPontoDbContext _db;

    public PerfisController(MeuPontoDbContext db)
    {
        _db = db;
    }

    // GET: api/Perfis
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Perfil>>> GetPerfis()
    {
        return await _db.Perfis.ToListAsync();
    }

    // GET: api/Perfis/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Perfil>> GetPerfil(int? id)
    {
        var perfil = await _db.Perfis.FindAsync(id);

        if (perfil == null)
        {
            return NotFound();
        }

        return perfil;
    }

    // PUT: api/Perfis/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPerfil(int? id, Perfil perfil)
    {
        if (id != perfil.Id)
        {
            return BadRequest();
        }

        _db.Entry(perfil).State = EntityState.Modified;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PerfilExists(id))
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

    // POST: api/Perfis
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Perfil>> PostPerfil(Perfil perfil)
    {
        _db.Perfis.Add(perfil);
        await _db.SaveChangesAsync();

        return CreatedAtAction("GetPerfil", new { id = perfil.Id }, perfil);
    }

    // DELETE: api/Perfis/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerfil(int? id)
    {
        var perfil = await _db.Perfis.FindAsync(id);
        if (perfil == null)
        {
            return NotFound();
        }

        _db.Perfis.Remove(perfil);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    private bool PerfilExists(int? id)
    {
        return _db.Perfis.Any(e => e.Id == id);
    }
}

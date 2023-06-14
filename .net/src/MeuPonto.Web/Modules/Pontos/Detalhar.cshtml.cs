using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Modules.Pontos.Comprovantes;

namespace MeuPonto.Modules.Pontos;

public class DetalharModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public DetalharModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    public Ponto Ponto { get; set; }

    public IList<Comprovante> Comprovantes { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Pontos == null)
        {
            return NotFound();
        }

        var ponto = await _db.Pontos.FirstOrDefaultAsync(m => m.Id == id);
        if (ponto == null)
        {
            return NotFound();
        }
        else
        {
            Ponto = ponto;
        }

        if (_db.Comprovantes != null)
        {
            Comprovantes = await _db.Comprovantes
                .Include(p => p.Ponto)
                .Where(p => p.PontoId == id)
                .ToListAsync();
        }

        return Page();
    }
}

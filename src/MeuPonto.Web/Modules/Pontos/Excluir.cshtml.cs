using MeuPonto.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MeuPonto.Modules.Pontos;

public class ExcluirModel : PageModel
{
    private readonly MeuPontoDbContext _db;

    public ExcluirModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public Ponto Ponto { get; set; }

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
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null || _db.Pontos == null)
        {
            return NotFound();
        }
        var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);

        var ponto = await _db.Pontos.FirstOrDefaultAsync(m => m.Id == id);

        if (ponto != null)
        {
            Ponto = ponto;
            _db.Pontos.Remove(Ponto);
            await _db.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}

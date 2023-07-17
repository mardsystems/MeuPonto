using MeuPonto.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MeuPonto.Modules.Pontos.Folhas;

public class ExcluirFolhaModel : PageModel
{
    private readonly MeuPontoDbContext _db;

    public ExcluirFolhaModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public Folha Folha { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Folhas == null)
        {
            return NotFound();
        }

        var folha = await _db.Folhas.FirstOrDefaultAsync(m => m.Id == id);

        if (folha == null)
        {
            return NotFound();
        }
        else
        {
            Folha = folha;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null || _db.Folhas == null)
        {
            return NotFound();
        }
        var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);

        var folha = await _db.Folhas.FindByIdAsync(id, nameIdentifier.Value);

        if (folha != null)
        {
            Folha = folha;
            _db.Folhas.Remove(Folha);
            await _db.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}

using MeuPonto.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using MeuPonto.Models.Pontos;

namespace MeuPonto.Pages.Pontos;

public class ExcluirModel : FormPageModel
{
    private readonly MeuPontoDbContext _db;

    [BindProperty]
    public Ponto Ponto { get; set; }

    public ExcluirModel(MeuPontoDbContext db)
    {
        _db = db;
    }

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

        HoldRefererUrl();

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

        AddTempSuccessMessage("Ponto excluído com sucesso");

        if (ShouldRedirectToRefererPage())
        {
            return RedirectToRefererPage();
        }
        else
        {
            return RedirectToPage("./Index");
        }
    }
}

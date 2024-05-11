using MeuPonto.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Models.Folhas;

namespace MeuPonto.Pages.Pontos.Folhas;

public class ExcluirModel : FormPageModel
{
    private readonly MeuPontoDbContext _db;

    [BindProperty]
    public Folha Folha { get; set; }

    public ExcluirModel(MeuPontoDbContext db)
    {
        _db = db;
    }

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

        HoldRefererUrl();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null || _db.Folhas == null)
        {
            return NotFound();
        }

        var folha = await _db.Folhas.FirstOrDefaultAsync(m => m.Id == id);

        if (folha != null)
        {
            Folha = folha;

            _db.Folhas.Remove(Folha);

            await _db.SaveChangesAsync();
        }

        AddTempSuccessMessage("Folha excluída com sucesso");

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

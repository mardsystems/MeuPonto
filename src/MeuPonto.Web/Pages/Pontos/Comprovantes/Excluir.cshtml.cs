using MeuPonto.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Models.Pontos;

namespace MeuPonto.Pages.Pontos.Comprovantes;

public class ExcluirModel : FormPageModel
{
    private readonly MeuPontoDbContext _db;

    [BindProperty]
    public Comprovante Comprovante { get; set; }

    public ExcluirModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Comprovantes == null)
        {
            return NotFound();
        }

        var comprovante = await _db.Comprovantes
            .Include(x => x.Ponto)
                .ThenInclude(x => x.Contrato)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (comprovante == null)
        {
            return NotFound();
        }
        else
        {
            Comprovante = comprovante;
        }

        HoldRefererUrl();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null || _db.Comprovantes == null)
        {
            return NotFound();
        }

        var comprovante = await _db.Comprovantes.FirstOrDefaultAsync(m => m.Id == id);

        if (comprovante != null)
        {
            Comprovante = comprovante;

            _db.Comprovantes.Remove(Comprovante);

            await _db.SaveChangesAsync();
        }

        AddTempSuccessMessage("Comprovante excluído com sucesso");

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

using MeuPonto.Data;
using MeuPonto.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Models.Contratos;

namespace MeuPonto.Pages.Contratos;

public class ExcluirModel : FormPageModel
{
    private readonly MeuPontoDbContext _db;

    [BindProperty]
    public Contrato Contrato { get; set; }

    public ExcluirModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Contratos == null)
        {
            return NotFound();
        }

        var contrato = await _db.Contratos.FirstOrDefaultAsync(m => m.Id == id);

        if (contrato == null)
        {
            return NotFound();
        }
        else
        {
            Contrato = contrato;
        }

        HoldRefererUrl();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null || _db.Contratos == null)
        {
            return NotFound();
        }

        var contrato = await _db.Contratos.FindByIdAsync(id, User.GetUserId());

        if (contrato != null)
        {
            Contrato = contrato;

            _db.Contratos.Remove(Contrato);
            
            await _db.SaveChangesAsync();
        }

        AddTempSuccessMessage("Contrato excluído com sucesso");

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

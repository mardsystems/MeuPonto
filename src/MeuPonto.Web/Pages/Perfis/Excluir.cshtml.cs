using MeuPonto.Data;
using MeuPonto.Extensions;
using MeuPonto.Models.Timesheet.Perfis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Pages.Perfis;

public class ExcluirModel : FormPageModel
{
    private readonly MeuPontoDbContext _db;

    [BindProperty]
    public Perfil Perfil { get; set; }

    public ExcluirModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Perfis == null)
        {
            return NotFound();
        }

        var perfil = await _db.Perfis.FirstOrDefaultAsync(m => m.Id == id);

        if (perfil == null)
        {
            return NotFound();
        }
        else
        {
            Perfil = perfil;
        }

        HoldRefererUrl();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null || _db.Perfis == null)
        {
            return NotFound();
        }

        var perfil = await _db.Perfis.FindByIdAsync(id, User.GetUserId());

        if (perfil != null)
        {
            Perfil = perfil;

            _db.Perfis.Remove(Perfil);
            
            await _db.SaveChangesAsync();
        }

        AddTempSuccessMessage("Perfil excluído com sucesso");

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

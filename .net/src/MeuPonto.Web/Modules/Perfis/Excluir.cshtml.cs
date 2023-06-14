using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Modules.Perfis;

public class ExcluirModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public ExcluirModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public Perfil Perfil { get; set; }

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
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null || _db.Perfis == null)
        {
            return NotFound();
        }

        try
        {
            var perfil = await _db.Perfis.FindAsync(id, User.Identity.Name);

            if (perfil != null)
            {
                Perfil = perfil;
                _db.Perfis.Remove(Perfil);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
        catch (Exception _)
        {
            throw;
        }
    }
}

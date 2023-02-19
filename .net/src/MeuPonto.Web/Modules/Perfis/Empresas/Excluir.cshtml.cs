using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Modules.Perfis.Empresas;

public class ExcluirModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public ExcluirModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public Empresa Empresa { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Perfis == null)
        {
            return NotFound();
        }

        var empresa = await _db.Empresas.FirstOrDefaultAsync(m => m.Id == id);

        if (empresa == null)
        {
            return NotFound();
        }
        else
        {
            Empresa = empresa;
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
            var empresa = await _db.Empresas.FindAsync(id, User.Identity.Name);

            if (empresa != null)
            {
                Empresa = empresa;
                _db.Empresas.Remove(Empresa);
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

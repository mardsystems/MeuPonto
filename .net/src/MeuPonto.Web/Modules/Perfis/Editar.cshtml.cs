using MeuPonto.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Modules.Perfis;

public class EditarModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public EditarModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public Perfil Perfil { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Perfis == null)
        {
            return NotFound();
        }

        var perfil =  await _db.Perfis.FirstOrDefaultAsync(m => m.Id == id);
        if (perfil == null)
        {
            return NotFound();
        }
        Perfil = perfil;
        
        ViewData["EmpresaId"] = new SelectList(_db.Empresas, "Id", "Nome").AddEmptyValue();

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var empresa = await _db.Empresas.FindAsync(Perfil.EmpresaId, User.Identity.Name);

        Perfil.Empresa = new EmpresaRef
        {
            Nome = empresa?.Nome,
            Cnpj = empresa?.Cnpj
        };

        _db.Attach(Perfil).State = EntityState.Modified;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PerfilExists(Perfil.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Detalhar", new { id = Perfil.Id });
    }

    private bool PerfilExists(Guid? id)
    {
      return _db.Perfis.Any(e => e.Id == id);
    }
}

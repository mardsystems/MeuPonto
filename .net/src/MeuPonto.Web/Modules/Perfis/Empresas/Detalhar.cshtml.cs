using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Modules.Perfis.Empresas;

public class DetalharModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public DetalharModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

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
}

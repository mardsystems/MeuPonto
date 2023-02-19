using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Modules.Pontos.Comprovantes;

public class DetalharComprovanteModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public DetalharComprovanteModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

  public Comprovante Comprovante { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Comprovantes == null)
        {
            return NotFound();
        }

        var comprovante = await _db.Comprovantes.FirstOrDefaultAsync(m => m.Id == id);
        if (comprovante == null)
        {
            return NotFound();
        }
        else 
        {
            Comprovante = comprovante;
        }
        return Page();
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Models.Contratos;

namespace MeuPonto.Pages.Contratos;

public class DetalharModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public DetalharModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

  public Contrato Contrato { get; set; }

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
        return Page();
    }
}

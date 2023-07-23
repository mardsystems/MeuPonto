using MeuPonto.Modules.Empregadores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Modules.Empregadores;

public class DetalharModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public DetalharModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

  public Empregador Empregador { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Perfis == null)
        {
            return NotFound();
        }

        var empregador = await _db.Empregadores.FirstOrDefaultAsync(m => m.Id == id);
        if (empregador == null)
        {
            return NotFound();
        }
        else 
        {
            Empregador = empregador;
        }
        return Page();
    }
}

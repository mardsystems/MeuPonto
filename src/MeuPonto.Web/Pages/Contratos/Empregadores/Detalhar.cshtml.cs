using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Models.Contratos;

namespace MeuPonto.Pages.Contratos.Empregadores;

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
        if (id == null || _db.Contratos == null)
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

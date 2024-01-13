using MeuPonto.Models.Timesheet.Trabalhadores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Pages.Trabalhadores;

public class DetalharModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public Trabalhador Trabalhador { get; set; }

    public DetalharModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Trabalhadores == null)
        {
            return NotFound();
        }

        var trabalhador = await _db.Trabalhadores.FirstOrDefaultAsync(m => m.Id == id);
        if (trabalhador == null)
        {
            return NotFound();
        }
        else
        {
            Trabalhador = trabalhador;
        }
        return Page();
    }
}

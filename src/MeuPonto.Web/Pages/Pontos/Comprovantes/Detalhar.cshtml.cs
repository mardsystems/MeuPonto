using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Models.Pontos;

namespace MeuPonto.Pages.Pontos.Comprovantes;

public class DetalharModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public Comprovante Comprovante { get; set; }

    public DetalharModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Comprovantes == null)
        {
            return NotFound();
        }

        var comprovante = await _db.Comprovantes
            .Include(x => x.Ponto)
                .ThenInclude(x => x.Contrato)
            .FirstOrDefaultAsync(x => x.Id == id);

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

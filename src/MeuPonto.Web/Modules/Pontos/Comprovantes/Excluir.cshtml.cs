using MeuPonto.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Modules.Pontos.Comprovantes;

public class ExcluirComprovanteModel : PageModel
{
    private readonly MeuPontoDbContext _db;

    public ExcluirComprovanteModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null || _db.Comprovantes == null)
        {
            return NotFound();
        }

        var comprovante = await _db.Comprovantes.FirstOrDefaultAsync(m => m.Id == id);

        if (comprovante != null)
        {
            Comprovante = comprovante;
            _db.Comprovantes.Remove(Comprovante);
            await _db.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}

using MeuPonto.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MeuPonto.Modules.Perfis.Empregadores;

public class ExcluirModel : PageModel
{
    private readonly MeuPontoDbContext _db;

    public ExcluirModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null || _db.Perfis == null)
        {
            return NotFound();
        }

        try
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);

            var empregador = await _db.Empregadores.FindByIdAsync(id, nameIdentifier.Value);

            if (empregador != null)
            {
                Empregador = empregador;
                _db.Empregadores.Remove(Empregador);
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

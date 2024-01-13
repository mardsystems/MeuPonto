using MeuPonto.Data;
using MeuPonto.Extensions;
using MeuPonto.Models.Timesheet.Empregadores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Pages.Empregadores;

public class ExcluirModel : FormPageModel
{
    private readonly MeuPontoDbContext _db;

    [BindProperty]
    public Empregador Empregador { get; set; }

    public ExcluirModel(MeuPontoDbContext db)
    {
        _db = db;
    }

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

        HoldRefererUrl();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null || _db.Contratos == null)
        {
            return NotFound();
        }

        var empregador = await _db.Empregadores.FindByIdAsync(id, User.GetUserId());

        if (empregador != null)
        {
            Empregador = empregador;

            _db.Empregadores.Remove(Empregador);

            await _db.SaveChangesAsync();
        }

        AddTempSuccessMessage("Empregador excluído com sucesso");

        if (ShouldRedirectToRefererPage())
        {
            return RedirectToRefererPage();
        }
        else
        {
            return RedirectToPage("./Index");
        }
    }
}

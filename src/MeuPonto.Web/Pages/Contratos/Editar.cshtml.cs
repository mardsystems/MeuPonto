using MeuPonto.Data;
using MeuPonto.Extensions;
using MeuPonto.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timesheet.Features.GestaoContratos;
using Timesheet.Models.Contratos;

namespace MeuPonto.Pages.Contratos;

public class EditarModel : FormPageModel
{
    private readonly Data.MeuPontoDbContext _db;

    [BindProperty]
    public Contrato EdicaoContrato { get; set; } = default!;

    public EditarModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

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

        EdicaoContrato = contrato;

        ViewData["EmpregadorId"] = new SelectList(_db.Empregadores.Where(x => x.UserId == User.GetUserId()), "Id", "Nome").AddEmptyValue();

        HoldRefererUrl();

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        var transaction = User.CreateTransaction();

        EdicaoContrato.RecontextualizaContrato(transaction, id);

        if (!ModelState.IsValid)
        {
            return Page();
        }

        Empregador empregador;

        if (EdicaoContrato.EmpregadorId != null)
        {
            empregador = await _db.Empregadores.FindByIdAsync(EdicaoContrato.EmpregadorId, User.GetUserId());
        }
        else
        {
            empregador = null;
        }

        var contrato = EdicaoContrato.AlterarContrato(empregador);

        try
        {
            _db.Attach(contrato).State = EntityState.Modified;

            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ContratoExists(contrato.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        var detalharPage = Url.Page("Detalhar", new { id = contrato.Id });

        AddTempSuccessMessage("Contrato editado com sucesso");

        if (ShouldRedirectToRefererPage())
        {
            return RedirectToRefererPage();
        }
        else
        {
            return Redirect(detalharPage);
        }
    }

    private bool ContratoExists(Guid? id)
    {
        return _db.Contratos.Any(e => e.Id == id);
    }
}

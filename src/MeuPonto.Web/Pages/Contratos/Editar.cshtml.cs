using MeuPonto.Data;
using MeuPonto.Extensions;
using MeuPonto.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timesheet.Models.Contratos;
using Timesheet.Models.Contratos.GestaoContratos;

namespace MeuPonto.Pages.Contratos;

public class EditarModel : FormPageModel
{
    private readonly Data.MeuPontoDbContext _db;

    [BindProperty]
    public Contrato Contrato { get; set; } = default!;

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

        Contrato = contrato;

        ViewData["EmpregadorId"] = new SelectList(_db.Empregadores.Where(x => x.UserId == User.GetUserId()), "Id", "Nome").AddEmptyValue();

        HoldRefererUrl();

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        var transaction = User.CreateTransaction();

        Contrato.RecontextualizaContrato(transaction, id);

        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Contrato.EmpregadorId.HasValue)
        {
            var empregador = await _db.Empregadores.FindByIdAsync(Contrato.EmpregadorId, User.GetUserId());

            Contrato.VinculaEmpregador(empregador);
        }

        try
        {
            _db.Attach(Contrato).State = EntityState.Modified;

            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ContratoExists(Contrato.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        var detalharPage = Url.Page("Detalhar", new { id = Contrato.Id });

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

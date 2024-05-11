using MeuPonto.Data;
using MeuPonto.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeuPonto.Features.RegistroPontos;
using MeuPonto.Models.Pontos;

namespace MeuPonto.Pages.Pontos;

public class EditarModel : FormPageModel
{
    private readonly MeuPontoDbContext _db;

    [BindProperty]
    public Ponto Ponto { get; set; } = default!;

    public EditarModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Pontos == null)
        {
            return NotFound();
        }

        var ponto = await _db.Pontos.FirstOrDefaultAsync(m => m.Id == id);

        if (ponto == null)
        {
            return NotFound();
        }
        
        Ponto = ponto;

        ViewData["ContratoId"] = new SelectList(_db.Contratos.Where(x => x.UserId == User.GetUserId()), "Id", "Nome");

        HoldRefererUrl();

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        var transaction = User.CreateTransaction();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var contrato = await _db.Contratos.FindByIdAsync(Ponto.ContratoId, User.GetUserId());

        contrato.QualificaPonto(Ponto);

        Ponto.RecontextualizaPonto(transaction, id);

        _db.Attach(Ponto).State = EntityState.Modified;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PontoExists(Ponto.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        var detalharPage = Url.Page("Detalhar", new { id = Ponto.Id });

        AddTempSuccessMessage("Ponto editado com sucesso");

        if (ShouldRedirectToRefererPage())
        {
            return RedirectToRefererPage();
        }
        else
        {
            return Redirect(detalharPage);
        }
    }

    private bool PontoExists(Guid? id)
    {
        return _db.Pontos.Any(e => e.Id == id);
    }
}

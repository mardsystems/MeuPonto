using MeuPonto.Data;
using MeuPonto.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Timesheet.Models.Pontos;

namespace MeuPonto.Pages.Pontos;

public class CriarModel : FormPageModel
{
    private readonly MeuPontoDbContext _db;

    [BindProperty]
    public Ponto Ponto { get; set; }

    public CriarModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    public IActionResult OnGet()
    {
        ViewData["ContratoId"] = new SelectList(_db.Contratos.Where(x => x.UserId == User.GetUserId()), "Id", "Nome");

        HoldRefererUrl();

        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        var transaction = User.CreateTransaction();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        Ponto.RecontextualizaPonto(transaction);

        var contrato = await _db.Contratos.FindByIdAsync(Ponto.ContratoId, User.GetUserId());

        contrato.QualificaPonto(Ponto);

        _db.Pontos.Add(Ponto);

        await _db.SaveChangesAsync();

        var detalharPage = Url.Page("Detalhar", new { id = Ponto.Id });

        AddTempSuccessMessageWithDetailLink("Ponto criado com sucesso", detalharPage);

        if (ShouldRedirectToRefererPage())
        {
            return RedirectToRefererPage();
        }
        else
        {
            return Redirect(detalharPage);
        }
    }
}

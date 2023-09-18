using MeuPonto.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MeuPonto.Modules.Pontos;

public class MarcarModel : FormPageModel
{
    private readonly MeuPontoDbContext _db;

    private readonly DateTimeSnapshot _dateTimeSnapshot;

    [BindProperty]
    public Ponto Ponto { get; set; }

    public MarcarModel(
        MeuPontoDbContext db,
        DateTimeSnapshot dateTimeSnapshot)
    {
        _db = db;

        _dateTimeSnapshot = dateTimeSnapshot;
    }

    public IActionResult OnGet()
    {
        var transaction = User.CreateTransaction();

        Ponto = PontoFactory.CriaPonto(transaction);

        Ponto.DataHora = _dateTimeSnapshot.GetDateTimeUntilMinutes();

        ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.Ativo && x.TrabalhadorId == User.GetUserId()), "Id", "Nome");

        HoldRefererUrl();

        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        var transaction = User.CreateTransaction();

        if (!ModelState.IsValid)
        {
            ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.Ativo && x.TrabalhadorId == User.GetUserId()), "Id", "Nome");

            return Page();
        }

        Ponto.RecontextualizaPonto(transaction);

        var perfil = await _db.Perfis.FindByIdAsync(Ponto.PerfilId, User.GetUserId());

        perfil.QualificaPonto(Ponto);

        _db.Pontos.Add(Ponto);

        await _db.SaveChangesAsync();

        var detalharPage = Url.Page("Detalhar", new { id = Ponto.Id });

        AddTempSuccessMessageWithDetailLink("Ponto marcado com sucesso", detalharPage);

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

using MeuPonto.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace MeuPonto.Modules.Pontos;

public class MarcarModel : PageModel
{
    private readonly MeuPontoDbContext _db;

    private readonly DateTimeSnapshot _dateTimeSnapshot;

    public MarcarModel(
        MeuPontoDbContext db,
        DateTimeSnapshot dateTimeSnapshot)
    {
        _db = db;

        _dateTimeSnapshot = dateTimeSnapshot;
    }

    public IActionResult OnGet()
    {
        var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);

        var userId = Guid.Parse(nameIdentifier.Value);

        var transaction = new TransactionContext(userId);

        Ponto = PontoFactory.CriaPonto(transaction);

        Ponto.DataHora = _dateTimeSnapshot.GetDateTimeUntilMinutes();

        ViewData["PerfilId"] = new SelectList(_db.Perfis, "Id", "Nome");

        return Page();
    }

    [BindProperty]
    public Ponto Ponto { get; set; }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);

        var userId = Guid.Parse(nameIdentifier.Value);

        var transaction = new TransactionContext(userId);

        if (!ModelState.IsValid)
        {
            ViewData["PerfilId"] = new SelectList(_db.Perfis, "Id", "Nome");

            return Page();
        }

        Ponto.RecontextualizaPonto(transaction);

        var perfil = await _db.Perfis.FindByIdAsync(Ponto.PerfilId, nameIdentifier.Value);

        perfil.QualificaPonto(Ponto);

        _db.Pontos.Add(Ponto);
        await _db.SaveChangesAsync();

        return RedirectToPage("./Detalhar", new { id = Ponto.Id });
    }
}

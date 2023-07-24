using MeuPonto.Data;
using MeuPonto.Modules.Trabalhadores;
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
        var transaction = User.CreateTransaction();

        Ponto = Trabalhador.Default.CriaPonto(transaction);

        Ponto.DataHora = _dateTimeSnapshot.GetDateTimeUntilMinutes();

        ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.Ativo && x.TrabalhadorId == Trabalhador.Default.Id), "Id", "Nome");

        return Page();
    }

    [BindProperty]
    public Ponto Ponto { get; set; }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        var transaction = User.CreateTransaction();

        if (!ModelState.IsValid)
        {
            ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.Ativo && x.TrabalhadorId == Trabalhador.Default.Id), "Id", "Nome");

            return Page();
        }

        Trabalhador.Default.RecontextualizaPonto(Ponto, transaction);

        var perfil = await _db.Perfis.FindByIdAsync(Ponto.PerfilId, Trabalhador.Default);

        perfil.QualificaPonto(Ponto);

        _db.Pontos.Add(Ponto);
        await _db.SaveChangesAsync();

        return RedirectToPage("./Detalhar", new { id = Ponto.Id });
    }
}

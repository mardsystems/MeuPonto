using MeuPonto.Data;
using MeuPonto.Modules.Trabalhadores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace MeuPonto.Modules.Pontos;

public class CriarModel : PageModel
{
    private readonly MeuPontoDbContext _db;

    public CriarModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    public IActionResult OnGet()
    {
        ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.TrabalhadorId == Trabalhador.Default.Id), "Id", "Nome");
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

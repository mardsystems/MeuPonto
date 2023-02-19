using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MeuPonto.Modules.Pontos;

public class MarcarModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    private readonly DateTimeSnapshot _dateTimeSnapshot;

    public MarcarModel(
        Data.MeuPontoDbContext db,
        DateTimeSnapshot dateTimeSnapshot)
    {
        _db = db;

        _dateTimeSnapshot = dateTimeSnapshot;
    }

    public IActionResult OnGet()
    {
        Ponto = new Ponto();

        Ponto.DataHora = _dateTimeSnapshot.GetDateTimeUntilMinutes();

        ViewData["PerfilId"] = new SelectList(_db.Perfis, "Id", "Nome");

        return Page();
    }

    [BindProperty]
    public Ponto Ponto { get; set; }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ViewData["PerfilId"] = new SelectList(_db.Perfis, "Id", "Nome");

            return Page();
        }

        Ponto.Id = Guid.NewGuid();

        Ponto.PartitionKey = User.Identity.Name; //Ponto.Data.ToString();

        Ponto.CreationDate = DateTime.Now;

        var perfil = await _db.Perfis.FindAsync(Ponto.PerfilId, User.Identity.Name);

        Ponto.Perfil = new PerfilRef
        {
            Nome = perfil?.Nome
        };

        _db.Pontos.Add(Ponto);
        await _db.SaveChangesAsync();

        return RedirectToPage("./Detalhar", new { id = Ponto.Id });
    }
}

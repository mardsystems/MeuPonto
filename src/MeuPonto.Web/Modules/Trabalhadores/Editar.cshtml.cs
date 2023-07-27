using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Modules.Trabalhadores;

[Authorize(Policy = "Admin")]
public class EditarModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public EditarModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public Trabalhador Trabalhador { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        if (id == null || _db.Trabalhadores == null)
        {
            return NotFound();
        }

        var trabalhador = await _db.Trabalhadores.FirstOrDefaultAsync(m => m.Id == id);
        if (trabalhador == null)
        {
            return NotFound();
        }
        Trabalhador = trabalhador;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        var transaction = User.CreateTransaction();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        //Trabalhador.Default.RecontextualizaTrabalhador(Trabalhador, transaction, id);

        var trabalhador = await _db.Trabalhadores.FirstOrDefaultAsync(m => m.Id == id);

        trabalhador.CustomerSubscription = Trabalhador.CustomerSubscription;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TrabalhadorExists(Trabalhador.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Detalhar", new { id });
    }

    private bool TrabalhadorExists(Guid? id)
    {
        return _db.Trabalhadores.Any(e => e.Id == id);
    }
}

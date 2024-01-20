using MeuPonto.Extensions;
using MeuPonto.Models.Trabalhadores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Pages.Trabalhadores;

[Authorize(Policy = "Admin")]
public class EditarModel : FormPageModel
{
    private readonly Data.MeuPontoDbContext _db;

    [BindProperty]
    public Trabalhador Trabalhador { get; set; } = default!;

    public EditarModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

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

        HoldRefererUrl();

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

        var detalharPage = Url.Page("Detalhar", new { id = Trabalhador.Id });

        AddTempSuccessMessage("Trabalhador editado com sucesso");

        if (ShouldRedirectToRefererPage())
        {
            return RedirectToRefererPage();
        }
        else
        {
            return Redirect(detalharPage);
        }
    }

    private bool TrabalhadorExists(Guid? id)
    {
        return _db.Trabalhadores.Any(e => e.Id == id);
    }
}

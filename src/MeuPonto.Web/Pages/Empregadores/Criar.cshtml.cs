using MeuPonto.Extensions;
using MeuPonto.Models.Timesheet.Empregadores;
using Microsoft.AspNetCore.Mvc;

namespace MeuPonto.Pages.Empregadores;

public class CriarModel : FormPageModel
{
    private readonly Data.MeuPontoDbContext _db;

    [BindProperty]
    public Empregador Empregador { get; set; }

    public CriarModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    public IActionResult OnGet()
    {
        HoldRefererUrl();

        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        var transaction = User.CreateTransaction();

        Empregador.RecontextualizaEmpregador(transaction);

        if (!ModelState.IsValid)
        {
            return Page();
        }

        _db.Empregadores.Add(Empregador);

        await _db.SaveChangesAsync();

        var detalharPage = Url.Page("Detalhar", new { id = Empregador.Id });

        AddTempSuccessMessageWithDetailLink("Empregador criado com sucesso", detalharPage);

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

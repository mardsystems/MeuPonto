using MeuPonto.Extensions;
using Microsoft.AspNetCore.Mvc;
using MeuPonto.Features.CadastroEmpregadores;
using MeuPonto.Models.Contratos;

namespace MeuPonto.Pages.Contratos.Empregadores;

public class CriarModel : FormPageModel
{
    private readonly Data.MeuPontoDbContext _db;

    [BindProperty]
    public Empregador CadastroEmpregador { get; set; }

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

        CadastroEmpregador.RecontextualizaEmpregador(transaction);

        if (!ModelState.IsValid)
        {
            return Page();
        }

        _db.Empregadores.Add(CadastroEmpregador);

        await _db.SaveChangesAsync();

        var detalharPage = Url.Page("Detalhar", new { id = CadastroEmpregador.Id });

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

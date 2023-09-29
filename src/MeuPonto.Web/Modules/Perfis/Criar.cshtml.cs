using MeuPonto.Data;
using MeuPonto.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MeuPonto.Modules.Perfis;

public class CriarModel : FormPageModel
{
    private readonly MeuPontoDbContext _db;

    [BindProperty]
    public Perfil Perfil { get; set; }

    public CriarModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    public IActionResult OnGet()
    {
        var transaction = User.CreateTransaction();

        Perfil = PerfilFactory.CriaPerfil(transaction);

        ViewData["EmpregadorId"] = new SelectList(_db.Empregadores.Where(x => x.UserId == User.GetUserId()), "Id", "Nome").AddEmptyValue();

        var daysOfWeek = Enum.GetValues<DayOfWeek>();

        foreach (var dayOfWeek in daysOfWeek)
        {
            var jornadaTrabalhoDiaria = new JornadaTrabalhoDiaria
            {
                DiaSemana = dayOfWeek,
                Tempo = new TimeSpan(8, 0, 0)
            };

            Perfil.JornadaTrabalhoSemanalPrevista.Semana.Add(jornadaTrabalhoDiaria);
        }

        HoldRefererUrl();

        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        var transaction = User.CreateTransaction();

        Perfil.RecontextualizaPerfil(transaction);

        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Perfil.EmpregadorId.HasValue)
        {
            var empregador = await _db.Empregadores.FindByIdAsync(Perfil.EmpregadorId, User.GetUserId());

            Perfil.VinculaEmpregador(empregador);
        }

        _db.Perfis.Add(Perfil);

        await _db.SaveChangesAsync();

        var detalharPage = Url.Page("Detalhar", new { id = Perfil.Id });

        AddTempSuccessMessageWithDetailLink("Perfil criado com sucesso", detalharPage);

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

using MeuPonto.Data;
using MeuPonto.Extensions;
using MeuPonto.Helpers;
using MeuPonto.Models.Timesheet.Contratos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MeuPonto.Pages.Contratos;

public class CriarModel : FormPageModel
{
    private readonly MeuPontoDbContext _db;

    [BindProperty]
    public Contrato Contrato { get; set; }

    public CriarModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    public IActionResult OnGet()
    {
        var transaction = User.CreateTransaction();

        Contrato = ContratoFactory.CriaContrato(transaction);

        ViewData["EmpregadorId"] = new SelectList(_db.Empregadores.Where(x => x.UserId == User.GetUserId()), "Id", "Nome").AddEmptyValue();

        var daysOfWeek = Enum.GetValues<DayOfWeek>();

        foreach (var dayOfWeek in daysOfWeek)
        {
            var jornadaTrabalhoDiaria = new JornadaTrabalhoDiaria
            {
                DiaSemana = dayOfWeek,
                Tempo = new TimeSpan(8, 0, 0)
            };

            Contrato.JornadaTrabalhoSemanalPrevista.Semana.Add(jornadaTrabalhoDiaria);
        }

        HoldRefererUrl();

        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        var transaction = User.CreateTransaction();

        Contrato.RecontextualizaContrato(transaction);

        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Contrato.EmpregadorId.HasValue)
        {
            var empregador = await _db.Empregadores.FindByIdAsync(Contrato.EmpregadorId, User.GetUserId());

            Contrato.VinculaEmpregador(empregador);
        }

        _db.Contratos.Add(Contrato);

        await _db.SaveChangesAsync();

        var detalharPage = Url.Page("Detalhar", new { id = Contrato.Id });

        AddTempSuccessMessageWithDetailLink("Contrato criado com sucesso", detalharPage);

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

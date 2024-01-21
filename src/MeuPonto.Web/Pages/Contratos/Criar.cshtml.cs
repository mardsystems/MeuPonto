using MeuPonto.Data;
using MeuPonto.Extensions;
using MeuPonto.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Timesheet.Features.GestaoContratos;
using Timesheet.Models.Contratos;

namespace MeuPonto.Pages.Contratos;

public class CriarModel : FormPageModel
{
    private readonly MeuPontoDbContext _db;

    [BindProperty]
    public Contrato AberturaContrato { get; set; }

    public CriarModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    public IActionResult OnGet()
    {
        var transaction = User.CreateTransaction();

        AberturaContrato = GestaoContratosFacade.InciarAberturaContrato(transaction);

        ViewData["EmpregadorId"] = new SelectList(_db.Empregadores.Where(x => x.UserId == User.GetUserId()), "Id", "Nome").AddEmptyValue();

        var daysOfWeek = Enum.GetValues<DayOfWeek>();

        foreach (var dayOfWeek in daysOfWeek)
        {
            var jornadaTrabalhoDiaria = new JornadaTrabalhoDiaria
            {
                DiaSemana = dayOfWeek,
                Tempo = new TimeSpan(8, 0, 0)
            };

            AberturaContrato.JornadaTrabalhoSemanalPrevista.Semana.Add(jornadaTrabalhoDiaria);
        }

        HoldRefererUrl();

        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        var transaction = User.CreateTransaction();

        AberturaContrato.RecontextualizaContrato(transaction);

        if (!ModelState.IsValid)
        {
            return Page();
        }

        Empregador empregador;

        if (AberturaContrato.EmpregadorId != null)
        {
            empregador = await _db.Empregadores.FindByIdAsync(AberturaContrato.EmpregadorId, User.GetUserId());
        }
        else
        {
            empregador = null;
        }

        var contrato = AberturaContrato.AbrirContrato(empregador);

        _db.Contratos.Add(contrato);

        await _db.SaveChangesAsync();

        var detalharPage = Url.Page("Detalhar", new { id = AberturaContrato.Id });

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

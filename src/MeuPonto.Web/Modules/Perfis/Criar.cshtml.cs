using MeuPonto.Data;
using MeuPonto.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MeuPonto.Modules.Perfis;

public class CriarModel : PageModel
{
    private readonly MeuPontoDbContext _db;

    public CriarModel(MeuPontoDbContext db)
    {
        _db = db;
    }

    public IActionResult OnGet()
    {
        var transaction = User.CreateTransaction();

        Perfil = PerfilFactory.CriaPerfil(transaction);

        ViewData["EmpregadorId"] = new SelectList(_db.Empregadores.Where(x => x.TrabalhadorId == User.GetUserId()), "Id", "Nome").AddEmptyValue();

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

        return Page();
    }

    [BindProperty]
    public Perfil Perfil { get; set; }

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

        try
        {
            _db.Perfis.Add(Perfil);
            await _db.SaveChangesAsync();
        }
        catch (Exception _)
        {
            throw;
        }

        return RedirectToPage("./Detalhar", new { id = Perfil.Id });
    }
}

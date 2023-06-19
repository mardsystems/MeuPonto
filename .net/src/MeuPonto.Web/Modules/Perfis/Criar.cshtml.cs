using MeuPonto.Helpers;
using MeuPonto.Modules.Pontos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MeuPonto.Modules.Perfis;

public class CriarModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public CriarModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    public IActionResult OnGet()
    {
        Perfil = new Perfil
        {

        };

        ViewData["EmpregadorId"] = new SelectList(_db.Empregadores, "Id", "Nome").AddEmptyValue();

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
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Perfil.Id = Guid.NewGuid();

        Perfil.PartitionKey = User.Identity.Name;

        Perfil.CreationDate = DateTime.Now;

        var empregador = await _db.Empregadores.FindAsync(Perfil.EmpregadorId, User.Identity.Name);

        Perfil.Empregador = new Empregador
        {
            Nome = empregador?.Nome,
        };

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

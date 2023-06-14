using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MeuPonto.Modules.Perfis;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Modules.Pontos.Folhas;

public class AbrirFolhaModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public AbrirFolhaModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    public IActionResult OnGet()
    {
        ViewData["PerfilId"] = new SelectList(_db.Perfis, "Id", "Nome");

        Folha = new Folha();

        Folha.Status = StatusEnum.Aberta;

        return Page();
    }

    [BindProperty]
    public Folha Folha { get; set; }

    [BindProperty]
    [Required]
    [DisplayName("Ano")]
    public int? CompetenciaAno { get; set; }

    [BindProperty]
    [Required]
    [DisplayName("Mês")]
    public int? CompetenciaMes { get; set; }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync(string? command)
    {
        if (ModelState.ContainsKey($"{nameof(Folha)}.{nameof(Folha.Competencia)}")) ModelState.Remove($"{nameof(Folha)}.{nameof(Folha.Competencia)}");

        if (!ModelState.IsValid)
        {
            return Page();
        }

        ViewData["PerfilId"] = new SelectList(_db.Perfis, "Id", "Nome");

        Folha.Status = StatusEnum.Aberta;

        var perfil = await _db.Perfis.FindAsync(Folha.PerfilId, User.Identity.Name);

        Folha.Perfil = new PerfilRef
        {
            Nome = perfil?.Nome
        };

        if (command == "ConfirmarCompetencia")
        {
            var states = ModelState.Where(state => state.Key.Contains($"{nameof(Folha.ApuracaoMensal)}"));

            foreach (var state in states)
            {
                if (ModelState.ContainsKey(state.Key)) ModelState.Remove(state.Key);
            }

            ConfirmarCompetencia(perfil);

            return Page();
        }
        else
        {
            Folha.Id = Guid.NewGuid();

            Folha.PartitionKey = User.Identity.Name;

            Folha.CreationDate = DateTime.Now;

            ConfirmarCompetencia(perfil);

            _db.Folhas.Add(Folha);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Detalhar", new { id = Folha.Id });
        }
    }

    private void ConfirmarCompetencia(Perfil? perfil)
    {
        Folha.ApuracaoMensal.Dias.Clear();

        var competenciaAtual = new DateTime(CompetenciaAno.Value, CompetenciaMes.Value, 1);

        Folha.Competencia = competenciaAtual;

        var competenciaPosterior = competenciaAtual.AddMonths(1);

        var dias = (competenciaPosterior - competenciaAtual).Days;

        Folha.ApuracaoMensal.TempoTotalPrevisto = TimeSpan.Zero;

        for (int dia = 1; dia <= dias; dia++)
        {
            var data = competenciaAtual.AddDays(dia - 1);

            var apuracaoDiaria = new ApuracaoDiaria
            {
                Dia = dia,
                TempoPrevisto = perfil.JornadaTrabalhoSemanalPrevista.Semana.Single(x => x.DiaSemana == data.DayOfWeek).Tempo,
                TempoApurado = null,
                DiferencaTempo = null,
                Feriado = false,
                Falta = false
            };

            Folha.ApuracaoMensal.Dias.Add(apuracaoDiaria);

            Folha.ApuracaoMensal.TempoTotalPrevisto += apuracaoDiaria.TempoPrevisto;
        }

        Folha.ApuracaoMensal.TempoTotalPeriodoAnterior = TimeSpan.Zero;
    }
}

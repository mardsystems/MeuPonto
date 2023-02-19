using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MeuPonto.Modules.Pontos.Folhas;

public class FecharFolhaModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public FecharFolhaModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public Folha Folha { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _db.Folhas == null)
        {
            return NotFound();
        }

        var folha = await _db.Folhas.FirstOrDefaultAsync(m => m.Id == id);

        if (folha == null)
        {
            return NotFound();
        }
        else
        {
            await Apurar(folha);

            Folha = folha;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null || _db.Folhas == null)
        {
            return NotFound();
        }

        try
        {
            var folha = await _db.Folhas.FindAsync(id, User.Identity.Name);

            await Apurar(folha);

            folha.Status = StatusEnum.Fechada;

            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PontoFolhaExists(Folha.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Detalhar", new { id = Folha.Id });
    }

    private async Task Apurar(Folha folha)
    {
        var competenciaFolha = folha.Competencia.Value;

        var competenciaFolhaPosterior = competenciaFolha.AddMonths(1);

        var pontos = await _db.Pontos
            .Where(x => x.DataHora >= competenciaFolha && x.DataHora < competenciaFolhaPosterior)
            .OrderByDescending(x => x.DataHora)
        .ToListAsync();

        folha.ApuracaoMensal.TempoTotalApurado = TimeSpan.Zero;

        for (int diaIndex = 0; diaIndex < folha.ApuracaoMensal.Dias.Count; diaIndex++)
        {
            var apuracaoDiaria = folha.ApuracaoMensal.Dias[diaIndex];

            DateTime? horaEntrada = null;

            bool? tempoApuradoIndeterminado = null;

            TimeSpan? tempoApurado = TimeSpan.Zero;

            var pontosDoDia = pontos
                .Where(x => x.DataHora.Value.Day == apuracaoDiaria.Dia.Value)
                .OrderBy(x => x.DataHora);

            foreach (var pontoDoDia in pontosDoDia)
            {
                if (horaEntrada == null)
                {
                    if (pontoDoDia.Momento == MomentoEnum.Entrada)
                    {
                        horaEntrada = pontoDoDia.DataHora;
                    }
                    else
                    {
                        tempoApuradoIndeterminado = true;

                        break;
                    }
                }
                else
                {
                    if (pontoDoDia.Momento == MomentoEnum.Saida)
                    {
                        var tempoRealizado = pontoDoDia.DataHora - horaEntrada;

                        if (tempoApurado == null)
                        {
                            tempoApurado = tempoRealizado;
                        }
                        else
                        {
                            tempoApurado += tempoRealizado;
                        }

                        horaEntrada = null;
                    }
                    else
                    {
                        tempoApuradoIndeterminado = true;

                        break;
                    }
                }
            }

            folha.ApuracaoMensal.TempoTotalApurado += tempoApurado + (apuracaoDiaria.TempoAbonado ?? TimeSpan.Zero);
        }
    }

    private bool PontoFolhaExists(Guid? id)
    {
        return _db.Folhas.Any(e => e.Id == id);
    }
}

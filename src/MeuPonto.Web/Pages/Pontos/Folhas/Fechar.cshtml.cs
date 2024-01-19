using MeuPonto.Data;
using MeuPonto.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Timesheet.Models.Pontos;
using Timesheet.Models.Pontos.Folhas;

namespace MeuPonto.Pages.Pontos.Folhas;

public class FecharModel : FormPageModel
{
    private readonly MeuPontoDbContext _db;

    [BindProperty]
    public Folha Folha { get; set; }

    public FecharModel(MeuPontoDbContext db)
    {
        _db = db;
    }

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

        HoldRefererUrl();

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

            Folha.StatusId = StatusFolhaEnum.Fechada;

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

        var detalharPage = Url.Page("Detalhar", new { id = Folha.Id });

        AddTempSuccessMessage("Folha fechada com sucesso");

        if (ShouldRedirectToRefererPage())
        {
            return RedirectToRefererPage();
        }
        else
        {
            return Redirect(detalharPage);
        }
    }

    private async Task Apurar(Folha folha)
    {
        var competenciaFolha = folha.Competencia.Value;

        var competenciaFolhaPosterior = competenciaFolha.AddMonths(1);

        var pontos = await _db.Pontos
            .Where(x => true
            && x.DataHora >= competenciaFolha && x.DataHora < competenciaFolhaPosterior
            && x.UserId == User.GetUserId())
            .OrderByDescending(x => x.DataHora)
        .ToListAsync();

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
                    if (pontoDoDia.MomentoId == MomentoEnum.Entrada)
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
                    if (pontoDoDia.MomentoId == MomentoEnum.Saida)
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

                apuracaoDiaria.TempoApurado = tempoApurado;
            }
        }
    }

    private bool PontoFolhaExists(Guid? id)
    {
        return _db.Folhas.Any(e => e.Id == id);
    }
}

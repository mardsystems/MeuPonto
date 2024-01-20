using MeuPonto.Extensions;
using MeuPonto.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Timesheet.Models.Folhas;
using Timesheet.Models.Pontos;

namespace MeuPonto.Pages.Pontos.Folhas;

public static class ApuracaoFacade
{
    public static async Task<ApuracaoMensalViewModel> ApurarFolha(this Data.MeuPontoDbContext db, Folha folha, ClaimsPrincipal user, DateTime hoje, DateTime competenciaAtual, DateTime competenciaFolha, DateTime competenciaFolhaPosterior)
    {
        var apuracaoMensal = new ApuracaoMensalViewModel();

        var pontos = await db.Pontos
            .Where(x => true
                && x.DataHora >= competenciaFolha
                && x.DataHora < competenciaFolhaPosterior
                && x.UserId == user.GetUserId())
            .OrderByDescending(x => x.DataHora)
            .ToListAsync();

        apuracaoMensal.PrimeiroDiaSemanaMes = competenciaFolha.DayOfWeek;

        var totalInteiroSemanas = (int)(competenciaFolhaPosterior - competenciaFolha).TotalDays / 7;

        var restoDiasSemana = (competenciaFolhaPosterior - competenciaFolha).TotalDays % 7;

        int totalSemanas = totalInteiroSemanas + 2;

        apuracaoMensal.TempoTotalPrevisto = TimeSpan.Zero;

        apuracaoMensal.TempoTotalApurado = TimeSpan.Zero;

        apuracaoMensal.DiferencaTempoTotal = TimeSpan.Zero;

        if (competenciaFolha < competenciaAtual)
        {
            apuracaoMensal.TempoPeriodo.Atual = false;
            apuracaoMensal.TempoPeriodo.Passado = true;
            apuracaoMensal.TempoPeriodo.Futuro = false;
        }
        else if (competenciaFolha == competenciaAtual)
        {
            apuracaoMensal.TempoPeriodo.Atual = true;
            apuracaoMensal.TempoPeriodo.Passado = false;
            apuracaoMensal.TempoPeriodo.Futuro = false;
        }
        else
        {
            apuracaoMensal.TempoPeriodo.Atual = false;
            apuracaoMensal.TempoPeriodo.Passado = false;
            apuracaoMensal.TempoPeriodo.Futuro = true;
        }

        int diaIndex = -1;

        for (int semanaIndex = 0; semanaIndex < totalSemanas; semanaIndex++)
        {
            var numeroSemanaAtual = hoje.GetWeekNumber();

            var apuracaoSemanalModel = new ApuracaoSemanalViewModel
            {
                NumeroSemana = competenciaFolha.GetWeekNumber() + semanaIndex,
                TempoTotalPrevisto = TimeSpan.Zero,
                TempoTotalApurado = TimeSpan.Zero,
                DiferencaTempoTotal = TimeSpan.Zero,
            };

            if (apuracaoMensal.TempoPeriodo.Passado)
            {
                apuracaoSemanalModel.TempoPeriodo.Atual = false;
                apuracaoSemanalModel.TempoPeriodo.Passado = true;
                apuracaoSemanalModel.TempoPeriodo.Futuro = false;
            }
            else if (apuracaoMensal.TempoPeriodo.Atual)
            {
                if (apuracaoSemanalModel.NumeroSemana < numeroSemanaAtual)
                {
                    apuracaoSemanalModel.TempoPeriodo.Atual = false;
                    apuracaoSemanalModel.TempoPeriodo.Passado = true;
                    apuracaoSemanalModel.TempoPeriodo.Futuro = false;
                }
                else if (apuracaoSemanalModel.NumeroSemana == numeroSemanaAtual)
                {
                    apuracaoSemanalModel.TempoPeriodo.Atual = true;
                    apuracaoSemanalModel.TempoPeriodo.Passado = false;
                    apuracaoSemanalModel.TempoPeriodo.Futuro = false;
                }
                else
                {
                    apuracaoSemanalModel.TempoPeriodo.Atual = false;
                    apuracaoSemanalModel.TempoPeriodo.Passado = false;
                    apuracaoSemanalModel.TempoPeriodo.Futuro = true;
                }
            }
            else
            {
                apuracaoSemanalModel.TempoPeriodo.Atual = false;
                apuracaoSemanalModel.TempoPeriodo.Passado = false;
                apuracaoSemanalModel.TempoPeriodo.Futuro = true;
            }

            int ultimoDiaSemanaIndex;

            if (semanaIndex == 0)
            {
                ultimoDiaSemanaIndex = 7 - (int)apuracaoMensal.PrimeiroDiaSemanaMes;
            }
            else
            {
                ultimoDiaSemanaIndex = 7;
            }

            for (int diaSemanaIndex = 0; diaSemanaIndex < ultimoDiaSemanaIndex; diaSemanaIndex++)
            {
                diaIndex++;

                if (diaIndex >= folha.ApuracaoMensal.TotalDias)
                {
                    break;
                }

                //

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
                }

                var diferencaTempo = tempoApurado - (apuracaoDiaria.TempoPrevisto ?? TimeSpan.Zero);

                var data = competenciaFolha.AddDays(apuracaoDiaria.Dia.Value - 1);

                var apuracaoDiariaModel = new ApuracaoDiariaViewModel
                {
                    Dia = apuracaoDiaria.Dia.Value,
                    DiaSemana = data.DayOfWeek,
                    DescricaoDia = data.DayOfWeek.Translate(),
                    TempoPrevisto = apuracaoDiaria.TempoPrevisto ?? TimeSpan.Zero,
                    TempoApurado = tempoApurado ?? TimeSpan.Zero,
                    TempoApuradoIdeterminado = tempoApuradoIndeterminado ?? false,
                    DiferencaTempo = diferencaTempo ?? TimeSpan.Zero,
                    TempoAbonado = apuracaoDiaria.TempoAbonado ?? TimeSpan.Zero,
                    Hoje = data == hoje,
                    Feriado = apuracaoDiaria.Feriado,
                    Falta = apuracaoDiaria.Falta,
                    Observacao = apuracaoDiaria.Observacao,
                    DataHoraInicio = pontosDoDia.FirstOrDefault()?.DataHora,
                    DataHoraFim = pontosDoDia.LastOrDefault()?.DataHora,
                    Pontos = pontosDoDia.ToArray()
                };

                apuracaoDiaria.TempoApurado = apuracaoDiaria.TempoApurado ?? TimeSpan.Zero;

                apuracaoDiaria.DiferencaTempo = apuracaoDiaria.DiferencaTempo ?? TimeSpan.Zero;

                //

                apuracaoSemanalModel.TempoTotalPrevisto += apuracaoDiariaModel.TempoPrevisto;

                apuracaoSemanalModel.TempoTotalApurado += apuracaoDiariaModel.TempoApurado + apuracaoDiariaModel.TempoAbonado;

                apuracaoSemanalModel.DiferencaTempoTotal += apuracaoDiariaModel.DiferencaTempo + apuracaoDiariaModel.TempoAbonado;

                if (apuracaoDiaria.TempoPrevisto != TimeSpan.Zero)
                {
                }

                //

                apuracaoMensal.Dias.Add(apuracaoDiariaModel);

                //


                if (data < hoje)
                {
                    apuracaoDiariaModel.TempoPeriodo.Atual = false;
                    apuracaoDiariaModel.TempoPeriodo.Passado = true;
                    apuracaoDiariaModel.TempoPeriodo.Futuro = false;
                }
                else if (data == hoje)
                {
                    apuracaoDiariaModel.TempoPeriodo.Atual = true;
                    apuracaoDiariaModel.TempoPeriodo.Passado = false;
                    apuracaoDiariaModel.TempoPeriodo.Futuro = false;
                }
                else
                {
                    apuracaoDiariaModel.TempoPeriodo.Atual = false;
                    apuracaoDiariaModel.TempoPeriodo.Passado = false;
                    apuracaoDiariaModel.TempoPeriodo.Futuro = true;
                }
            }

            apuracaoMensal.TempoTotalPrevisto += apuracaoSemanalModel.TempoTotalPrevisto;

            apuracaoMensal.TempoTotalApurado += apuracaoSemanalModel.TempoTotalApurado;

            apuracaoMensal.DiferencaTempoTotal += apuracaoSemanalModel.DiferencaTempoTotal;

            apuracaoMensal.Semanas.Add(apuracaoSemanalModel);
        }

        return apuracaoMensal;
    }

}

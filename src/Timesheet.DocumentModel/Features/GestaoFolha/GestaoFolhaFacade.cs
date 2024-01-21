using System.Transactions;
using Timesheet.Models.Contratos;
using Timesheet.Models.Folhas;
using Timesheet.Models.Pontos;

namespace Timesheet.Features.GestaoFolha;

public static class GestaoFolhaFacade
{
    public static Folha IniciarAberturaFolha(this TransactionContext transaction, Guid? id = null)
    {
        var folha = new Folha
        {
            Id = id ?? Guid.NewGuid(),
            UserId = transaction.UserId,
            PartitionKey = $"{transaction.UserId}",
            CreationDate = transaction.DateTime
        };

        return folha;
    }

    public static void RecontextualizaFolha(this TransactionContext transaction, Folha folha, Guid? id = null)
    {
        folha.Id ??= id ?? Guid.NewGuid();
        folha.UserId = transaction.UserId;
        folha.PartitionKey = $"{folha.UserId}|{folha.Competencia:yyyy}";
        folha.CreationDate ??= transaction.DateTime;
    }

    public static void AssociarAo(this Folha folha, Contrato contrato)
    {
        folha.Contrato = new ContratoRef
        {
            Nome = contrato.Nome
        };

        folha.ContratoId = contrato.Id;
    }

    public static void ConfirmarCompetencia(this Folha folha, Contrato? contrato)
    {
        var competenciaAtual = folha.Competencia.Value;

        var competenciaPosterior = competenciaAtual.AddMonths(1);

        var dias = (competenciaPosterior - competenciaAtual).Days;

        if (folha.ApuracaoMensal.Dias.Count == 0)
        {
            for (int dia = 1; dia <= dias; dia++)
            {
                var data = competenciaAtual.AddDays(dia - 1);

                var apuracaoDiaria = new ApuracaoDiaria
                {
                    Dia = dia,
                    TempoPrevisto = contrato.JornadaTrabalhoSemanalPrevista.Semana.Single(x => x.DiaSemana == data.DayOfWeek).Tempo,
                    TempoApurado = null,
                    DiferencaTempo = null,
                    Feriado = false,
                    Falta = false
                };

                folha.ApuracaoMensal.Dias.Add(apuracaoDiaria);
            }

            folha.ApuracaoMensal.TempoTotalPeriodoAnterior = TimeSpan.Zero;
        }
        else
        {
            for (int dia = 1; dia <= dias; dia++)
            {
                var data = competenciaAtual.AddDays(dia - 1);

                if (folha.ApuracaoMensal.Dias.Any(x => x.Dia == dia))
                {
                    var apuracaoDiaria = folha.ApuracaoMensal.Dias.First(x => x.Dia == dia);

                    apuracaoDiaria.TempoPrevisto = contrato.JornadaTrabalhoSemanalPrevista.Semana.Single(x => x.DiaSemana == data.DayOfWeek).Tempo;
                    apuracaoDiaria.TempoApurado = null;
                    apuracaoDiaria.DiferencaTempo = null;
                    apuracaoDiaria.DiferencaTempo = null;
                    apuracaoDiaria.Feriado = false;
                }
                else
                {
                    var apuracaoDiaria = new ApuracaoDiaria
                    {
                        Dia = dia,
                        TempoPrevisto = contrato.JornadaTrabalhoSemanalPrevista.Semana.Single(x => x.DiaSemana == data.DayOfWeek).Tempo,
                        TempoApurado = null,
                        DiferencaTempo = null,
                        Feriado = false,
                        Falta = false,
                        Observacao = null
                    };

                    folha.ApuracaoMensal.Dias.Add(apuracaoDiaria);
                }
            }

            folha.ApuracaoMensal.TempoTotalPeriodoAnterior = TimeSpan.Zero;
        }
    }
}

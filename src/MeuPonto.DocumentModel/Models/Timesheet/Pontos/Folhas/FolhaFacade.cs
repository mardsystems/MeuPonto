using MeuPonto.Models.Timesheet.Contratos;

namespace MeuPonto.Models.Timesheet.Pontos.Folhas;

public static class FolhaFacade
{
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

using BoDi;
using MeuPonto.Models.Trabalhadores;
using System.Transactions;
using Timesheet.Models.Contratos;
using Timesheet.Models.Folhas;
using Timesheet.Models.Pontos;
using Timesheet.Models.Pontos;

namespace MeuPonto.Support;

[Binding]
public class SeedHook
{
    public SeedHook(IObjectContainer objectContainer)
    {

    }

    [BeforeScenario]
    public void SetupTestUsers(
        ScenarioContext scenario,
        GestaoContratosContext gestaoContratos,
        RegistroPontosContext registroPontos,
        BackupComprovantesContext backupComprovantes,
        GestaoFolhasContext gestaoFolhas)
    {
        var userId = Guid.Parse("d2fc8313-9bdc-455c-bf29-ccf709a2a692").ToString();

        var userName = "Test user";

        scenario.Set(userId, "UserId");

        var transaction = new TransactionContext(userId);

        var trabalhador = TrabalhadorFactory.CriaTrabalhador(transaction);

        var contrato = GestaoContratos.CriaContrato(transaction);

        contrato.Nome = userName;
        contrato.Ativo = true;
        contrato.JornadaTrabalhoSemanalPrevista = new JornadaTrabalhoSemanal
        {
            Semana = new List<JornadaTrabalhoDiaria>(new[]{
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Monday,
                        Tempo = new TimeSpan(8,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Tuesday,
                        Tempo = new TimeSpan(8,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Wednesday,
                        Tempo = new TimeSpan(8,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Thursday,
                        Tempo = new TimeSpan(8,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Friday,
                        Tempo = new TimeSpan(8,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Saturday,
                        Tempo = new TimeSpan(0,0,0)
                    },
                    new JornadaTrabalhoDiaria
                    {
                        DiaSemana = DayOfWeek.Sunday,
                        Tempo = new TimeSpan(0,0,0)
                    }
                })
        };

        gestaoContratos.Inicia(contrato);

        var ponto = RegistroPontos.CriaPonto(transaction);

        ponto.MomentoId = MomentoEnum.Entrada;

        registroPontos.Inicia(ponto);

        var comprovante = BackupComprovantes.CriaComprovante(transaction);

        backupComprovantes.Inicia(comprovante);

        backupComprovantes.Inicia(ponto);

        var hoje = DateTime.Today;

        var competencia = new DateTime(hoje.Year, hoje.Month, 1);

        var folha = GestaoFolha.CriaFolha(transaction);

        contrato.QualificaFolha(folha);

        folha.Competencia = competencia;

        var competenciaAtual = competencia;

        var competenciaPosterior = competenciaAtual.AddMonths(1);

        var dias = (competenciaPosterior - competenciaAtual).Days;

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

        gestaoFolhas.Inicia(folha);
    }
}

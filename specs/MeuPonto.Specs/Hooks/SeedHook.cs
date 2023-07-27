using BoDi;
using MeuPonto.Modules.Perfis;
using MeuPonto.Modules.Pontos;
using MeuPonto.Modules.Pontos.Comprovantes;
using MeuPonto.Modules.Pontos.Folhas;
using MeuPonto.Modules.Trabalhadores;

namespace MeuPonto.Hooks;

[Binding]
public class SeedHook
{
    public SeedHook(IObjectContainer objectContainer)
    {

    }

    [BeforeScenario]
    public void SetupTestUsers(
        CadastroPerfisContext cadastroPerfis,
        RegistroPontosContext registroPontos,
        BackupComprovantesContext backupComprovantes,
        GestaoFolhasContext gestaoFolhas)
    {
        var userId = Guid.Parse("d2fc8313-9bdc-455c-bf29-ccf709a2a692");

        var userName = "Test user";

        var transaction = new TransactionContext(userId);

        var trabalhador = TrabalhadorFactory.CriaTrabalhador(transaction);

        Trabalhador.Default = trabalhador;

        var perfil = trabalhador.CriaPerfil(transaction);

        perfil.Nome = userName;
        perfil.Ativo = true;
        perfil.JornadaTrabalhoSemanalPrevista = new JornadaTrabalhoSemanal
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

        cadastroPerfis.Inicia(perfil);

        var ponto = trabalhador.CriaPonto(transaction);

        ponto.MomentoId = MomentoEnum.Entrada;

        registroPontos.Inicia(ponto);

        var comprovante = trabalhador.CriaComprovante(transaction);

        backupComprovantes.Inicia(comprovante);

        backupComprovantes.Inicia(ponto);

        var hoje = DateTime.Today;

        var competencia = new DateTime(hoje.Year, hoje.Month, 1);

        var folha = trabalhador.CriaFolha(transaction);

        perfil.QualificaFolha(folha);

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
                TempoPrevisto = perfil.JornadaTrabalhoSemanalPrevista.Semana.Single(x => x.DiaSemana == data.DayOfWeek).Tempo,
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

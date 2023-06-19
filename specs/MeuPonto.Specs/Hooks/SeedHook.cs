using BoDi;
using MeuPonto.Modules.Perfis;
using MeuPonto.Modules.Pontos;
using MeuPonto.Modules.Pontos.Comprovantes;
using MeuPonto.Modules.Pontos.Folhas;

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
        var transaction = new TransactionContext("Test user");

        var perfil = PerfilFactory.CriaPerfil(transaction);

        perfil.Nome = "Test user";
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

        var ponto = PontoFactory.CriaPonto(transaction);

        ponto.MomentoId = MomentoEnum.Entrada;

        registroPontos.Inicia(ponto);

        var comprovante = new Comprovante();

        backupComprovantes.Inicia(comprovante);

        backupComprovantes.Inicia(ponto);

        var hoje = DateTime.Today;

        var folhaNova = new Folha
        {
            Competencia = new DateTime(hoje.Year, hoje.Month, 1)
        };

        gestaoFolhas.Inicia(folhaNova);
    }
}

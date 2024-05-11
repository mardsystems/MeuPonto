using BoDi;
using System.Transactions;
using TechTalk.SpecFlow.Infrastructure;

namespace MeuPonto.Support;

[Binding]
public class SeedHook
{
    private readonly IObjectContainer _objectContainer;

    public SeedHook(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }

    [BeforeScenario(Order = 100)]
    public void SetupTestUsers(
        ScenarioContext scenario,
        ISpecFlowOutputHelper specFlowOutputHelper,
        CadastroEmpregadoresContext cadastroEmpregadores,
        GestaoContratosContext gestaoContratos,
        RegistroPontosContext registroPontos,
        BackupComprovantesContext backupComprovantes,
        GestaoFolhasContext gestaoFolhas,
        DateTimeSnapshot dateTimeSnapshot)
    {
        var userId = Guid.Parse("d2fc8313-9bdc-455c-bf29-ccf709a2a692").ToString();

        var userName = "Test user";

        scenario.Set(userId, "UserId");

        var transaction = new TransactionContext(userId);

        _objectContainer.RegisterInstanceAs(transaction);

        //var ponto = RegistroPontosFacade.CriaPonto(transaction);

        //specFlowOutputHelper.WriteLine($"SetupTest --> dateTimeSnapshot");

        //ponto.DataHora = dateTimeSnapshot.GetDateTimeUntilMinutes();
        //ponto.MomentoId = MomentoEnum.Entrada;
        //ponto.PausaId = null;

        //contrato.QualificaPonto(ponto);

        //registroPontos.Inicia(ponto);

        //var comprovante = BackupComprovantesFacade.CriaComprovante(transaction);

        //backupComprovantes.Inicia(comprovante);

        //backupComprovantes.Inicia(ponto);

        //var hoje = DateTime.Today;

        //var competencia = new DateTime(hoje.Year, hoje.Month, 1);

        //var folha = GestaoFolhaFacade.IniciarAberturaFolha(transaction);

        //folha.AssociarAo(contrato);

        //folha.Competencia = competencia;

        //var competenciaAtual = competencia;

        //var competenciaPosterior = competenciaAtual.AddMonths(1);

        //var dias = (competenciaPosterior - competenciaAtual).Days;

        //for (int dia = 1; dia <= dias; dia++)
        //{
        //    var data = competenciaAtual.AddDays(dia - 1);

        //    var apuracaoDiaria = new ApuracaoDiaria
        //    {
        //        Dia = dia,
        //        TempoPrevisto = contrato.JornadaTrabalhoSemanalPrevista.Semana.Single(x => x.DiaSemana == data.DayOfWeek).Tempo,
        //        TempoApurado = null,
        //        DiferencaTempo = null,
        //        Feriado = false,
        //        Falta = false
        //    };

        //    folha.ApuracaoMensal.Dias.Add(apuracaoDiaria);
        //}

        //folha.ApuracaoMensal.TempoTotalPeriodoAnterior = TimeSpan.Zero;

        //gestaoFolhas.Inicia(folha);
    }
}

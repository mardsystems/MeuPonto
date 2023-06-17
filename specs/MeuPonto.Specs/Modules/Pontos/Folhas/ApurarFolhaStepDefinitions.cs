using MeuPonto.Data;

namespace MeuPonto.Modules.Pontos.Folhas;

[Binding]
public class ApurarFolhaStepDefinitions
{
    private readonly ScenarioContext _scenario;

    private readonly GestaoFolhasContext _gestaoFolhas;

    private readonly HomeContext _home;

    private readonly HomeInterface _homeInterface;

    private readonly MeuPontoDbContext _db;

    public ApurarFolhaStepDefinitions(
        ScenarioContext scenario,
        GestaoFolhasContext gestaoFolhas,
        HomeContext home,
        HomeInterface homeInterface,
        MeuPontoDbContext db)
    {
        _scenario = scenario;

        _gestaoFolhas = gestaoFolhas;

        _home = home;

        _homeInterface = homeInterface;

        _db = db;
    }

    [Given(@"que o trabalhador registrou a entrada no expediente às '([^']*)'")]
    public async Task GivenQueOTrabalhadorRegistrouAEntradaNoExpedienteAs(DateTime entrada)
    {
        var transaction = new TransactionContext("Test user");

        var perfil = _db.Perfis.FirstOrDefault();

        var pontoEntrada = PontoFactory.CriaPonto(transaction);

        perfil.QualificaPonto(pontoEntrada);

        pontoEntrada.DataHora = entrada;
        pontoEntrada.MomentoId = MomentoEnum.Entrada;

        _db.Pontos.Add(pontoEntrada);
        await _db.SaveChangesAsync();
    }

    [Given(@"que o trabalhador registrou a saída no expediente às '([^']*)'")]
    public async Task GivenQueOTrabalhadorRegistrouASaidaNoExpedienteAs(DateTime saida)
    {
        var transaction = new TransactionContext("Test user");

        var perfil = _db.Perfis.FirstOrDefault();

        var pontoSaida = PontoFactory.CriaPonto(transaction);

        perfil.QualificaPonto(pontoSaida);

        pontoSaida.DataHora = saida;
        pontoSaida.MomentoId = MomentoEnum.Saida;

        _db.Pontos.Add(pontoSaida);
        await _db.SaveChangesAsync();
    }

    [Given(@"que o trabalhador tem uma folha de ponto aberta na competência '([^']*)'")]
    public async Task GivenQueOTrabalhadorTemUmaFolhaDePontoAbertaNaCompetencia(DateTime competencia)
    {
        var perfil = _db.Perfis.FirstOrDefault();

        var folhaAberta = GestaoFolhasStub.ObtemFolhaAbertaFrom(perfil, competencia);

        _db.Folhas.Add(folhaAberta);
        await _db.SaveChangesAsync();

        _gestaoFolhas.ConsideraQueExiste(folhaAberta);
    }

    [When(@"o trabalhador apurar a folha de ponto")]
    public void WhenOTrabalhadorApurarAFolhaDePonto()
    {
        var folhaApurada = _homeInterface.ApurarFolha(_gestaoFolhas.Folha);

        _gestaoFolhas.Define(folhaApurada);
    }

    [Then(@"o tempo total realizado deverá ser de '([^']*)'")]
    public void ThenOTempoTotalRealizadoDeveraSerDe(TimeSpan tempoTotalRealizado)
    {
        var apuracaoMensal = _gestaoFolhas.FolhaAberta.Guarda();

        apuracaoMensal.TempoTotalApurado.Should().Be(tempoTotalRealizado);
    }

    [Then(@"o tempo total apurado da folha de ponto deverá ser de '([^']*)'")]
    public void ThenOTempoTotalApuradoDaFolhaDePontoDeveraSerDe(TimeSpan tempoTotalApurado)
    {
        var apuracaoMensal = _gestaoFolhas.FolhaAberta.Guarda();

        apuracaoMensal.TempoTotalApurado.Should().Be(tempoTotalApurado);
    }
}

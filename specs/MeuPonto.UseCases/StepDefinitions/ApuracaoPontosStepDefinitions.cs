using MeuPonto.Data;
using MeuPonto.Drivers;
using MeuPonto.Support;

namespace MeuPonto.StepDefinitions;

[Binding]
public class ApuracaoPontosStepDefinitions
{
    private readonly ScenarioContext _scenario;

    private readonly GestaoFolhasContext _gestaoFolhas;

    private readonly GestaoFolhasDriver _gestaoFolhasDriver;

    private readonly GestaoContratosContext _gestaoContratos;

    private readonly HomeContext _home;

    private readonly HomeDriver _homeDriver;

    private readonly MeuPontoDbContext _db;

    public ApuracaoPontosStepDefinitions(
        ScenarioContext scenario,
        GestaoFolhasContext gestaoFolhas,
        GestaoFolhasDriver gestaoFolhasDriver,
        GestaoContratosContext gestaoContratos,
        HomeContext home,
        HomeDriver homeDriver,
        MeuPontoDbContext db)
    {
        _scenario = scenario;

        _gestaoFolhas = gestaoFolhas;

        _gestaoFolhasDriver = gestaoFolhasDriver;

        _gestaoContratos = gestaoContratos;

        _home = home;

        _homeDriver = homeDriver;

        _db = db;
    }

    [When(@"o trabalhador apurar a folha de ponto")]
    public void WhenOTrabalhadorApurarAFolhaDePonto()
    {
        var folhaApurada = _homeDriver.ApurarFolha(_gestaoFolhas.Folha);

        _gestaoFolhas.Define(folhaApurada);
    }

    [Then(@"o tempo total realizado deverá ser de '([^']*)'")]
    public void ThenOTempoTotalRealizadoDeveraSerDe(TimeSpan tempoTotalRealizado)
    {
        _gestaoFolhas.FolhaAberta.ApuracaoMensal.TempoTotalApurado.Should().Be(tempoTotalRealizado);
    }

    [Then(@"o tempo total apurado da folha de ponto deverá ser de '([^']*)'")]
    public void ThenOTempoTotalApuradoDaFolhaDePontoDeveraSerDe(TimeSpan tempoTotalApurado)
    {
        _gestaoFolhas.FolhaAberta.ApuracaoMensal.TempoTotalApurado.Should().Be(tempoTotalApurado);
    }

    [Then(@"o tempo total apurado deverá ser '([^']*)'")]
    public void ThenOTempoTotalApuradoDeveraSer(TimeSpan tempoTotalApurado)
    {
        _gestaoFolhas.FolhaAberta.ApuracaoMensal.TempoTotalApurado.Should().Be(tempoTotalApurado);
    }

    [Then(@"o tempo total período anterior deverá ser nulo")]
    public void ThenOTempoTotalPeriodoAnteriorDeveraSerNulo()
    {
        _gestaoFolhas.FolhaAberta.ApuracaoMensal.TempoTotalPeriodoAnterior.Should().BeNull();
    }

    [Then(@"o tempo total período anterior deverá ser '([^']*)'")]
    public void ThenOTempoTotalPeriodoAnteriorDeveraSer(TimeSpan tempoTotalPeriodoAnterior)
    {
        _gestaoFolhas.FolhaAberta.ApuracaoMensal.TempoTotalPeriodoAnterior.Should().Be(tempoTotalPeriodoAnterior);
    }
}

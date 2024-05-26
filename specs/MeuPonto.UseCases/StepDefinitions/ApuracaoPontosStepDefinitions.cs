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

    private readonly ApuracaoPontosDriver _apuracaoPontosDriver;

    private readonly MeuPontoDbContext _db;

    public ApuracaoPontosStepDefinitions(
        ScenarioContext scenario,
        GestaoFolhasContext gestaoFolhas,
        GestaoFolhasDriver gestaoFolhasDriver,
        MeuPontoDbContext db)
    {
        _scenario = scenario;

        _gestaoFolhas = gestaoFolhas;

        _gestaoFolhasDriver = gestaoFolhasDriver;

        _db = db;
    }

    [When(@"o trabalhador apurar a folha de ponto")]
    public void WhenOTrabalhadorApurarAFolhaDePonto()
    {
        var folhaApurada = _apuracaoPontosDriver.ApurarFolha(_gestaoFolhas.Folha);

        _gestaoFolhas.Contextualizar(folhaApurada);
    }

    [Then(@"o tempo total realizado deverá ser de '([^']*)'")]
    public void ThenOTempoTotalRealizadoDeveraSerDe(TimeSpan tempoTotalRealizado)
    {
        _gestaoFolhas.Folha.ApuracaoMensal.TempoTotalApurado.Should().Be(tempoTotalRealizado);
    }

    [Then(@"o tempo total apurado da folha de ponto deverá ser de '([^']*)'")]
    public void ThenOTempoTotalApuradoDaFolhaDePontoDeveraSerDe(TimeSpan tempoTotalApurado)
    {
        _gestaoFolhas.Folha.ApuracaoMensal.TempoTotalApurado.Should().Be(tempoTotalApurado);
    }

    [Then(@"o tempo total apurado deverá ser '([^']*)'")]
    public void ThenOTempoTotalApuradoDeveraSer(TimeSpan tempoTotalApurado)
    {
        _gestaoFolhas.Folha.ApuracaoMensal.TempoTotalApurado.Should().Be(tempoTotalApurado);
    }

    [Then(@"o tempo total período anterior deverá ser nulo")]
    public void ThenOTempoTotalPeriodoAnteriorDeveraSerNulo()
    {
        _gestaoFolhas.Folha.ApuracaoMensal.TempoTotalPeriodoAnterior.Should().BeNull();
    }

    [Then(@"o tempo total período anterior deverá ser '([^']*)'")]
    public void ThenOTempoTotalPeriodoAnteriorDeveraSer(TimeSpan tempoTotalPeriodoAnterior)
    {
        _gestaoFolhas.Folha.ApuracaoMensal.TempoTotalPeriodoAnterior.Should().Be(tempoTotalPeriodoAnterior);
    }
}

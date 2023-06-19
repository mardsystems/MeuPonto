using MeuPonto.Data;
using MeuPonto.Modules.Perfis;
using System.ComponentModel;

namespace MeuPonto.Modules.Pontos.Folhas;

[Binding]
public class FecharFolhaStepDefinitions
{
    private readonly ScenarioContext _scenario;

    private readonly GestaoFolhasContext _gestaoFolhas;

    private readonly GestaoFolhasInterface _gestaoFolhasInterface;

    private readonly CadastroPerfisContext _cadastroPerfis;

    private readonly MeuPontoDbContext _db;

    public FecharFolhaStepDefinitions(
        ScenarioContext scenario,
        GestaoFolhasContext gestaoFolhas,
        GestaoFolhasInterface gestaoFolhasInterface,
        CadastroPerfisContext cadastroPerfis,
        MeuPontoDbContext db)
    {
        _scenario = scenario;

        _gestaoFolhas = gestaoFolhas;

        _gestaoFolhasInterface = gestaoFolhasInterface;

        _cadastroPerfis = cadastroPerfis;

        _db = db;
    }

    [Given(@"que o trabalhador tem uma folha de ponto aberta")]
    public async Task GivenQueOTrabalhadorTemUmaFolhaDePontoAberta()
    {
        _db.Folhas.Add(_gestaoFolhas.Folha);
        await _db.SaveChangesAsync();
    }

    [Given(@"que o trabalhador tem uma folha de ponto aberta na competência")]
    public async Task GivenQueOTrabalhadorTemUmaFolhaDePontoAbertaNaCompetencia(DateTime competencia)
    {
        _gestaoFolhas.Folha.Competencia = competencia;

        _db.Folhas.Add(_gestaoFolhas.Folha);
        await _db.SaveChangesAsync();
    }

    [Given(@"que o ano/mês é '([^']*)'")]
    public void GivenQueOAnoMesE(string anoMes)
    {

    }

    [Given(@"que os pontos registrados foram:")]
    public async Task GivenQueOsPontosRegistradosForam(Table table)
    {
        var transaction = new TransactionContext("Test user");

        var pontos = table.Rows.Select(row =>
        {
            var dataHora = DateTime.Parse(row["data/hora"]);

            var momento = (MomentoEnum)Enum.Parse(typeof(MomentoEnum), row["momento"]);

            var ponto = PontoFactory.CriaPonto(transaction, Guid.NewGuid());

            _cadastroPerfis.Perfil.QualificaPonto(ponto);

            ponto.DataHora = dataHora;
            ponto.MomentoId = momento;

            return ponto;
        });

        _db.Pontos.AddRange(pontos);
        await _db.SaveChangesAsync();
    }

    [When(@"o trabalhador fechar a folha de ponto")]
    public void WhenOTrabalhadorFecharAFolhaDePonto()
    {
        var folhaFechada = _gestaoFolhasInterface.FecharFolha(_gestaoFolhas.Folha);

        _gestaoFolhas.Define(folhaFechada);
    }

    [Then(@"a folha de ponto deverá ser fechada")]
    public void ThenAFolhaDePontoDeveraSerFechada()
    {
        _gestaoFolhas.FolhaAberta.Status.Nome.Should().Be(StatusEnum.Fechada.GetDisplayName());
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

using MeuPonto.Data;
using MeuPonto.Drivers;
using MeuPonto.Support;
using System.Transactions;
using MeuPonto.Features.GestaoFolha;
using MeuPonto.Features.RegistroPontos;
using MeuPonto.Models.Folhas;
using MeuPonto.Models.Pontos;

namespace MeuPonto.StepDefinitions;

[Binding]
public class GestaoFolhasStepDefinitions
{
    private readonly ScenarioContext _scenario;

    private readonly GestaoFolhasContext _gestaoFolhas;

    private readonly GestaoFolhasDriver _gestaoFolhasDriver;

    private readonly GestaoContratosContext _gestaoContratos;

    private readonly HomeContext _home;

    private readonly HomeDriver _homeDriver;

    private readonly MeuPontoDbContext _db;

    public GestaoFolhasStepDefinitions(
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

    [Given(@"que o trabalhador qualifica a folha com o contrato '([^']*)'")]
    public void GivenQueOTrabalhadorQualificaAFolhaComOContrato(string nome)
    {
        var contrato = _db.Contratos.FirstOrDefault(x => x.Nome == nome);

        _gestaoFolhas.Folha.AssociarAo(contrato);
    }

    [Given(@"que o trabalhador deseja apurar a folha de ponto da competência '([^']*)'")]
    public void GivenQueOTrabalhadorDesejaApurarAFolhaDePontoDaCompetencia(DateTime competencia)
    {
        _gestaoFolhas.Folha.Competencia = competencia;
    }

    [Given(@"que o trabalhador anota a seguinte observação na folha de ponto:")]
    public void GivenQueOTrabalhadorAnotaASeguinteObservacaoNaFolhaDePonto(string observacao)
    {
        _gestaoFolhas.Folha.Observacao = observacao;
    }

    [Given(@"que o trabalhador tem uma folha de ponto aberta na competência '([^']*)'")]
    public async Task GivenQueOTrabalhadorTemUmaFolhaDePontoAbertaNaCompetencia(DateTime competencia)
    {
        _gestaoFolhas.Folha.StatusId = StatusFolhaEnum.Aberta;
        _gestaoFolhas.Folha.Competencia = competencia;

        _db.Folhas.Add(_gestaoFolhas.Folha);
        await _db.SaveChangesAsync();
    }

    [Given(@"que o trabalhador tem uma folha de ponto aberta")]
    public async Task GivenQueOTrabalhadorTemUmaFolhaDePontoAberta()
    {
        _db.Folhas.Add(_gestaoFolhas.Folha);
        await _db.SaveChangesAsync();
    }

    [Given(@"que o trabalhador tem uma folha de ponto aberta na competência")]
    public async Task GivenQueOTrabalhadorTemUmaFolhaDePontoAbertaNaCompetencia()
    {
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
        var userId = Guid.Parse("d2fc8313-9bdc-455c-bf29-ccf709a2a692");

        var transaction = new TransactionContext(userId.ToString());

        var pontos = table.Rows.Select(row =>
        {
            var dataHora = DateTime.Parse(row["data/hora"]);

            var momento = (MomentoEnum)Enum.Parse(typeof(MomentoEnum), row["momento"]);

            var ponto = RegistroPontosFacade.CriaPonto(transaction);

            _gestaoContratos.Contrato.QualificaPonto(ponto);

            ponto.DataHora = dataHora;
            ponto.MomentoId = momento;

            return ponto;
        });

        _db.Pontos.AddRange(pontos);
        await _db.SaveChangesAsync();
    }

    #region Abrir Folha

    [When(@"o trabalhador abrir uma folha de ponto")]
    public void WhenOTrabalhadorAbrirUmaFolhaDePonto()
    {
        if (_gestaoFolhas.Folha.Contrato == null)
        {
            var contrato = _db.Contratos.FirstOrDefault();

            if (contrato == default)
            {
                contrato = _gestaoContratos.Contrato;

                _db.Contratos.Add(contrato);
                _db.SaveChanges();
            }

            _gestaoFolhas.Folha.AssociarAo(contrato);
        }


        var folhaAberta = _gestaoFolhasDriver.AbrirFolha(_gestaoFolhas.Folha);

        _gestaoFolhas.Define(folhaAberta);
    }

    [Then(@"uma folha de ponto deverá ser aberta")]
    public void ThenUmaFolhaDePontoDeveraSerAberta()
    {
        _gestaoFolhas.FolhaAberta.Should().NotBeNull();
    }

    #endregion

    #region Fechar Folha

    [When(@"o trabalhador fechar a folha de ponto")]
    public void WhenOTrabalhadorFecharAFolhaDePonto()
    {
        var folhaFechada = _gestaoFolhasDriver.FecharFolha(_gestaoFolhas.Folha);

        _gestaoFolhas.Define(folhaFechada);
    }

    [Then(@"a folha de ponto deverá ser fechada")]
    public void ThenAFolhaDePontoDeveraSerFechada()
    {
        _gestaoFolhas.FolhaAberta.StatusId.Should().Be(StatusFolhaEnum.Fechada);
    }

    #endregion

    [Then(@"o contrato da folha de ponto deverá deverá ser '([^']*)'")]
    public void ThenOContratoDaFolhaDePontoDeveraDeveraSer(string nome)
    {
        var contrato = _gestaoFolhas.FolhaAberta.Contrato;

        contrato.Nome.Should().Be(nome);
    }

    [Then(@"o status da folha de ponto deverá ser '([^']*)'")]
    public void ThenOStatusDaFolhaDePontoDeveraSer(StatusFolhaEnum status)
    {
        _gestaoFolhas.FolhaAberta.StatusId.Should().Be(status);
    }

    [Then(@"a folha de ponto deverá ter '([^']*)' dias")]
    public void ThenAFolhaDePontoDeveraTerDias(int dias)
    {
        // TODO: _gestaoFolhas.FolhaAberta.ApuracaoMensal.QuantidadeDiaria.Should().Be(dias);
    }

    [Then(@"a folha de ponto não deverá ter tempo total apurado")]
    public void ThenAFolhaDePontoNaoDeveraTerTempoTotalApurado()
    {
        _gestaoFolhas.FolhaAberta.ApuracaoMensal.TempoTotalApurado.Should().BeNull();
    }

    [Then(@"a folha de ponto não deverá ter tempo total período anterior")]
    public void ThenAFolhaDePontoNaoDeveraTerTempoTotalPeriodoAnterior()
    {
        _gestaoFolhas.FolhaAberta.ApuracaoMensal.TempoTotalPeriodoAnterior.Should().BeNull();
    }

    [Then(@"a folha de ponto deverá ter uma observação")]
    public void ThenAFolhaDePontoDeveraTerUmaObservacao()
    {
        _gestaoFolhas.FolhaAberta.Observacao.Should().NotBeNullOrEmpty();
    }

    [Then(@"a folha de ponto não deverá ter uma observação")]
    public void ThenAFolhaDePontoNaoDeveraTerUmaObservacao()
    {
        _gestaoFolhas.FolhaAberta.Observacao.Should().BeNullOrEmpty();
    }
}

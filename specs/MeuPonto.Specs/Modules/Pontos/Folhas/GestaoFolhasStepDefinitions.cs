using MeuPonto.Data;
using MeuPonto.Modules.Perfis;
using System.ComponentModel;

namespace MeuPonto.Modules.Pontos.Folhas;

[Binding]
public class GestaoFolhasStepDefinitions
{
    private readonly ScenarioContext _scenario;

    private readonly GestaoFolhasContext _gestaoFolhas;

    private readonly GestaoFolhasInterface _gestaoFolhasInterface;

    private readonly CadastroPerfisContext _cadastroPerfis;

    private readonly HomeContext _home;

    private readonly HomeInterface _homeInterface;

    private readonly MeuPontoDbContext _db;

    public GestaoFolhasStepDefinitions(
        ScenarioContext scenario,
        GestaoFolhasContext gestaoFolhas,
        GestaoFolhasInterface gestaoFolhasInterface,
        CadastroPerfisContext cadastroPerfis,
        HomeContext home,
        HomeInterface homeInterface,
        MeuPontoDbContext db)
    {
        _scenario = scenario;

        _gestaoFolhas = gestaoFolhas;

        _gestaoFolhasInterface = gestaoFolhasInterface;

        _cadastroPerfis = cadastroPerfis;

        _home = home;

        _homeInterface = homeInterface;

        _db = db;
    }

    [Given(@"que o trabalhador qualifica a folha com o perfil '([^']*)'")]
    public void GivenQueOTrabalhadorQualificaAFolhaComOPerfil(string nome)
    {
        var perfil = _db.Perfis.FirstOrDefault(x => x.Nome == nome);

        perfil.QualificaFolha(_gestaoFolhas.Folha);
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
        _gestaoFolhas.Folha.StatusId = StatusEnum.Aberta;
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
        var transaction = new TransactionContext("Test user");

        var pontos = table.Rows.Select(row =>
        {
            var dataHora = DateTime.Parse(row["data/hora"]);

            var momento = (MomentoEnum)Enum.Parse(typeof(MomentoEnum), row["momento"]);

            var ponto = PontoFactory.CriaPonto(transaction);

            _cadastroPerfis.Perfil.QualificaPonto(ponto);

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
        if (_gestaoFolhas.Folha.Perfil == null)
        {
            var perfil = _db.Perfis.FirstOrDefault();

            if (perfil == default)
            {
                perfil = _cadastroPerfis.Perfil;

                _db.Perfis.Add(perfil);
                _db.SaveChanges();
            }

            perfil.QualificaFolha(_gestaoFolhas.Folha);
        }


        var folhaAberta = _gestaoFolhasInterface.AbrirFolha(_gestaoFolhas.Folha);

        _gestaoFolhas.Define(folhaAberta);
    }

    [Then(@"uma folha de ponto deverá ser aberta")]
    public void ThenUmaFolhaDePontoDeveraSerAberta()
    {
        _gestaoFolhas.FolhaAberta.Should().NotBeNull();
    }

    #endregion

    #region Apurar Folha

    [When(@"o trabalhador apurar a folha de ponto")]
    public void WhenOTrabalhadorApurarAFolhaDePonto()
    {
        var folhaApurada = _homeInterface.ApurarFolha(_gestaoFolhas.Folha);

        _gestaoFolhas.Define(folhaApurada);
    }

    #endregion

    #region Fechar Folha

    [When(@"o trabalhador fechar a folha de ponto")]
    public void WhenOTrabalhadorFecharAFolhaDePonto()
    {
        var folhaFechada = _gestaoFolhasInterface.FecharFolha(_gestaoFolhas.Folha);

        _gestaoFolhas.Define(folhaFechada);
    }

    [Then(@"a folha de ponto deverá ser fechada")]
    public void ThenAFolhaDePontoDeveraSerFechada()
    {
        _gestaoFolhas.FolhaAberta.Status.Should().Be(StatusEnum.Fechada.GetDisplayName());
    }

    #endregion

    [Then(@"o perfil da folha de ponto deverá deverá ser '([^']*)'")]
    public void ThenOPerfilDaFolhaDePontoDeveraDeveraSer(string nome)
    {
        var perfil = _gestaoFolhas.FolhaAberta.EQualificadaPelo();

        perfil.Nome.Should().Be(nome);
    }

    [Then(@"o status da folha de ponto deverá ser '([^']*)'")]
    public void ThenOStatusDaFolhaDePontoDeveraSer(string status)
    {
        _gestaoFolhas.FolhaAberta.Status.Should().Be(status);
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

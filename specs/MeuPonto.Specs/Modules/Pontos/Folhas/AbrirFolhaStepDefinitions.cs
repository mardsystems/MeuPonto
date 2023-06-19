using MeuPonto.Data;
using MeuPonto.Modules.Perfis;

namespace MeuPonto.Modules.Pontos.Folhas;

[Binding]
public class AbrirFolhaStepDefinitions
{
    private readonly ScenarioContext _scenario;

    private readonly GestaoFolhasContext _gestaoFolhas;

    private readonly GestaoFolhasInterface _gestaoFolhasInterface;

    private readonly CadastroPerfisContext _cadastroPerfis;

    private readonly MeuPontoDbContext _db;

    public AbrirFolhaStepDefinitions(
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

    [Given(@"que o trabalhador qualifica a folha com o perfil '([^']*)'")]
    public void GivenQueOTrabalhadorQualificaAFolhaComOPerfil(string nome)
    {
        var perfil = _db.Perfis.FirstOrDefault(x => x.Nome == nome);

        _gestaoFolhas.Folha.QualificaCom(perfil);
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

            _gestaoFolhas.Folha.QualificaCom(perfil);
        }


        var folhaAberta = _gestaoFolhasInterface.AbrirFolha(_gestaoFolhas.Folha);

        _gestaoFolhas.Define(folhaAberta);
    }

    [Then(@"uma folha de ponto deverá ser aberta")]
    public void ThenUmaFolhaDePontoDeveraSerAberta()
    {
        _gestaoFolhas.FolhaAberta.Should().NotBeNull();
    }

    [Then(@"o perfil da folha de ponto deverá deverá ser '([^']*)'")]
    public void ThenOPerfilDaFolhaDePontoDeveraDeveraSer(string nome)
    {
        var perfil = _gestaoFolhas.FolhaAberta.EQualificadaPelo();

        perfil.Nome.Should().Be(nome);
    }

    [Then(@"o status da folha de ponto deverá ser '([^']*)'")]
    public void ThenOStatusDaFolhaDePontoDeveraSer(string status)
    {
        _gestaoFolhas.FolhaAberta.Status.Nome.Should().Be(status);
    }

    [Then(@"a folha de ponto deverá ter '([^']*)' dias")]
    public void ThenAFolhaDePontoDeveraTerDias(int dias)
    {
        // TODO: _gestaoFolhas.FolhaAberta.ApuracaoMensal.QuantidadeDiaria.Should().Be(dias);
    }

    [Then(@"a folha de ponto não deverá ter tempo total apurado")]
    public void ThenAFolhaDePontoNaoDeveraTerTempoTotalApurado()
    {
        var apuracaoMensal = _gestaoFolhas.FolhaAberta.Guarda();

        apuracaoMensal.TempoTotalApurado.Should().BeNull();
    }

    [Then(@"a folha de ponto não deverá ter tempo total período anterior")]
    public void ThenAFolhaDePontoNaoDeveraTerTempoTotalPeriodoAnterior()
    {
        var apuracaoMensal = _gestaoFolhas.FolhaAberta.Guarda();

        apuracaoMensal.TempoTotalPeriodoAnterior.Should().BeNull();
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

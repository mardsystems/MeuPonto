using MeuPonto.Data;
using System.ComponentModel;

namespace MeuPonto.Modules.Pontos;

[Binding]
public class RegistroPontosStepDefinitions
{
    private readonly ScenarioContext _scenario;

    private readonly RegistroPontosContext _registroPontos;

    private readonly MeuPontoDbContext _db;

    public RegistroPontosStepDefinitions(
        ScenarioContext scenario,
        RegistroPontosContext registroPontos,
        MeuPontoDbContext db)
    {
        _scenario = scenario;

        _registroPontos = registroPontos;

        _db = db;
    }

    [Given(@"que é o momento de '([^']*)' do expediente")]
    public void GivenQueEOMomentoDeDoExpediente(MomentoEnum momento)
    {
        _registroPontos.Ponto.MomentoId = momento;
    }

    [Given(@"que é o momento de '([^']*)' do expediente para o almoço")]
    public void GivenQueEOMomentoDeDoExpedienteParaOAlmoco(MomentoEnum momento)
    {
        _registroPontos.Ponto.MomentoId = momento;

        _registroPontos.Ponto.PausaId = PausaEnum.Almoco;
    }

    [Given(@"que é o momento de '([^']*)' do expediente da volta do almoço")]
    public void GivenQueEOMomentoDeDoExpedienteDaVoltaDoAlmoco(MomentoEnum momento)
    {
        _registroPontos.Ponto.MomentoId = momento;

        _registroPontos.Ponto.PausaId = PausaEnum.Almoco;
    }

    [Given(@"que o trabalhador anota a seguinte observação no ponto:")]
    public void GivenQueOTrabalhadorAnotaASeguinteObservacaoNoPonto(string observacao)
    {
        _registroPontos.Ponto.Observacao = observacao;
    }

    [Then(@"o perfil do ponto deverá deverá ser '([^']*)'")]
    public void ThenOPerfilDoPontoDeveraDeveraSer(string nome)
    {
        var perfil = _registroPontos.PontoRegistrado.EQualificadoPelo();

        perfil.Nome.Should().Be(nome);
    }

    [Then(@"a data do ponto deverá ser '([^']*)'")]
    public void ThenADataDoPontoDeveraSer(DateTime data)
    {
        //TODO: _registroPontos.PontoRegistrado.Data.Should().Be(data);
    }

    [Then(@"o momento do ponto deverá ser de '([^']*)'")]
    public void ThenOMomentoDoPontoDeveraSerDe(string momento)
    {
        _registroPontos.PontoRegistrado.Momento.Nome.Should().Be(momento);
    }

    [Then(@"o ponto deverá indicar que é almoço")]
    public void ThenOPontoDeveraIndicarQueEAlmoco()
    {
        _registroPontos.PontoRegistrado.Pausa.Nome.Should().Be(PausaEnum.Almoco.GetDisplayName());
    }

    [Then(@"o ponto deverá indicar que não é almoço")]
    public void ThenOPontoDeveraIndicarQueNaoEAlmoco()
    {
        _registroPontos.PontoRegistrado.Pausa.Should().BeNull();
    }

    [Then(@"o ponto deverá indicar que não foi estimado")]
    public void ThenOPontoDeveraIndicarQueNaoFoiEstimado()
    {
        _registroPontos.PontoRegistrado.Estimado.Should().BeFalse();
    }

    [Then(@"o ponto deverá ter uma observação")]
    public void ThenOPontoDeveraTerUmaObservacao()
    {
        _registroPontos.PontoRegistrado.Observacao.Should().NotBeNullOrEmpty();
    }

    [Then(@"o ponto não deverá ter uma observação")]
    public void ThenOPontoNaoDeveraTerUmaObservacao()
    {
        _registroPontos.PontoRegistrado.Observacao.Should().BeNullOrEmpty();
    }
}

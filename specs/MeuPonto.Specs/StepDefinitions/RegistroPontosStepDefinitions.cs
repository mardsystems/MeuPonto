using MeuPonto.Data;
using MeuPonto.Drivers;
using MeuPonto.Support;
using Timesheet.Models.Pontos;

namespace MeuPonto.StepDefinitions;

[Binding]
public class RegistroPontosStepDefinitions
{
    private readonly ScenarioContext _scenario;

    private readonly RegistroPontosContext _registroPontos;

    private readonly RegistroPontosDriver _registroPontosDriver;

    private readonly GestaoContratosContext _gestaoContratos;

    private readonly MeuPontoDbContext _db;

    public RegistroPontosStepDefinitions(
        ScenarioContext scenario,
        RegistroPontosContext registroPontos,
        RegistroPontosDriver registroPontosDriver,
        GestaoContratosContext gestaoContratos,
        MeuPontoDbContext db)
    {
        _scenario = scenario;

        _registroPontos = registroPontos;

        _registroPontosDriver = registroPontosDriver;

        _gestaoContratos = gestaoContratos;

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

    [Given(@"que a data/hora do relógio é '([^']*)'")]
    public void GivenQueADataHoraDoRelogioE(DateTime dataHora)
    {
        _registroPontos.DataHora = dataHora;
    }

    [Given(@"que o trabalhador qualifica o ponto com o contrato '([^']*)'")]
    public void GivenQueOTrabalhadorQualificaOPontoComOContrato(string nome)
    {
        var contrato = _db.Contratos.FirstOrDefault(x => x.Nome == nome);

        contrato.QualificaPonto(_registroPontos.Ponto);

        _db.SaveChanges();
    }

    [When(@"o trabalhador marcar o ponto")]
    public void WhenOTrabalhadorMarcarOPonto()
    {
        if (_registroPontos.Ponto.EstaSemQualificacao())
        {
            var contrato = _db.Contratos.FirstOrDefault();

            if (contrato == default)
            {
                contrato = _gestaoContratos.Contrato;

                _db.Contratos.Add(contrato);
                _db.SaveChanges();
            }

            contrato.QualificaPonto(_registroPontos.Ponto);
        }

        var pontoMarcado = _registroPontosDriver.MarcarPonto(_registroPontos.Ponto);

        _registroPontos.Define(pontoMarcado);
    }

    [Then(@"o ponto deverá ser marcado")]
    public void ThenOPontoDeveraSerMarcado()
    {
        _registroPontos.PontoRegistrado.Should().NotBeNull();
    }

    [Then(@"o contrato do ponto deverá deverá ser '([^']*)'")]
    public void ThenOContratoDoPontoDeveraDeveraSer(string nome)
    {
        _registroPontos.PontoRegistrado.Contrato.Nome.Should().Be(nome);
    }

    [Then(@"a data do ponto deverá ser '([^']*)'")]
    public void ThenADataDoPontoDeveraSer(DateTime data)
    {
        //TODO: _registroPontos.PontoRegistrado.Data.Should().Be(data);
    }

    [Then(@"o momento do ponto deverá ser de '([^']*)'")]
    public void ThenOMomentoDoPontoDeveraSerDe(MomentoEnum momento)
    {
        _registroPontos.PontoRegistrado.MomentoId.Should().Be(momento);
    }

    [Then(@"o ponto deverá indicar que é almoço")]
    public void ThenOPontoDeveraIndicarQueEAlmoco()
    {
        _registroPontos.PontoRegistrado.PausaId.Should().Be(PausaEnum.Almoco);
    }

    [Then(@"o ponto deverá indicar que não é almoço")]
    public void ThenOPontoDeveraIndicarQueNaoEAlmoco()
    {
        _registroPontos.PontoRegistrado.PausaId.Should().BeNull();
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

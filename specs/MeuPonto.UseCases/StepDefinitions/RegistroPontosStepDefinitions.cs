using MeuPonto.Data;
using MeuPonto.Drivers;
using MeuPonto.Support;
using Microsoft.EntityFrameworkCore;
using TechTalk.SpecFlow.Assist;
using MeuPonto.Features.RegistroPontos;
using MeuPonto.Models.Pontos;
using System.Transactions;

namespace MeuPonto.StepDefinitions;

[Binding]
public class RegistroPontosStepDefinitions
{
    private readonly ScenarioContext _scenario;
    private readonly RegistroPontosContext _registroPontos;
    private readonly RegistroPontosDriver _registroPontosInterface;
    private readonly MeuPontoDbContext _db;

    public RegistroPontosStepDefinitions(
        ScenarioContext scenario,
        RegistroPontosContext registroPontos,
        RegistroPontosDriver registroPontosInterface,
        MeuPontoDbContext db)
    {
        _scenario = scenario;
        _registroPontos = registroPontos;
        _registroPontosInterface = registroPontosInterface;
        _db = db;
    }

    [Given(@"que existe um registro de ponto em andamento")]
    public void GivenQueExisteUmRegistroDePontoEmAndamento()
    {
        var ponto = _registroPontosInterface.SolicitarRegistroPonto();

        var contrato = _db.Contratos.FirstOrDefault();

        contrato.QualificaPonto(ponto);

        var datetime = DateTime.Now;

        ponto.DataHora = new DateTime(datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, 0);
        ponto.MomentoId = MomentoEnum.Entrada;

        _registroPontos.Contextualizar(ponto);
    }

    [When(@"o trabalhador solicitar um registro de ponto")]
    public void WhenOTrabalhadorSolicitarUmRegistroDePonto()
    {
        var ponto = _registroPontosInterface.SolicitarRegistroPonto();

        _registroPontos.Contextualizar(ponto);
    }

    [When(@"o trabalhador registrar o ponto como:")]
    public void WhenOTrabalhadorRegistrarOPontoComo(Table especificacao)
    {
        _registroPontos.Especificar(especificacao);

        var ponto = _registroPontos.Ponto;

        var data = especificacao.CreateInstance(() => new RegistroPontoData
        {
            DataHora = ponto.DataHora,
            Contrato = ponto.Contrato?.Nome,
            MomentoId = ponto.MomentoId,
            PausaId = ponto.PausaId,
            Estimado = ponto.Estimado
        });

        var contrato = _db.Contratos.FirstOrDefault(x => x.Nome == data.Contrato);

        contrato.QualificaPonto(ponto);

        ponto.DataHora = data.DataHora;
        ponto.MomentoId = data.MomentoId;
        ponto.PausaId = data.PausaId;
        ponto.Estimado = data.Estimado;

        _registroPontosInterface.RegistrarPonto(ponto);

        var pontoRegistrado = _db.Pontos
            .Include(x => x.Contrato)
            .FirstOrDefault(x => x.DataHora == ponto.DataHora);

        _registroPontos.Contextualizar(pontoRegistrado);
    }

    [When(@"o trabalhador tentar registrar o ponto como:")]
    public void WhenOTrabalhadorTentarRegistrarOPontoComo(Table especificacao)
    {
        _registroPontos.Especificar(especificacao);

        var ponto = _registroPontos.Ponto;

        var data = especificacao.CreateInstance(() => new RegistroPontoData
        {
            Contrato = ponto.Contrato?.Nome,
            MomentoId = ponto.MomentoId,
            PausaId = ponto.PausaId,
            Estimado = ponto.Estimado
        });

        var contrato = _db.Contratos.FirstOrDefault(x => x.Nome == data.Contrato);

        if (contrato == null)
        {
            ponto.DesqualificaPonto();
        }
        else
        {
            contrato.QualificaPonto(ponto);
        }

        ponto.MomentoId = data.MomentoId;
        ponto.PausaId = data.PausaId;
        ponto.Estimado = data.Estimado;

        try
        {
            _registroPontosInterface.RegistrarPonto(ponto);
        }
        catch (Exception ex)
        {
            _registroPontos.CapturarErro(ex.Message);
        }
    }

    [When(@"o trabalhador registrar o ponto com a seguinte observação:")]
    public void WhenOTrabalhadorRegistrarOPontoComASeguinteObservacao(string observacao)
    {
        var ponto = _registroPontos.Ponto;

        var contrato = _db.Contratos.FirstOrDefault();

        contrato.QualificaPonto(ponto);

        ponto.Observacao = observacao;

        _registroPontosInterface.RegistrarPonto(ponto);

        var pontoRegistrado = _db.Pontos
            .Include(x => x.Contrato)
            .FirstOrDefault(x => x.DataHora == ponto.DataHora);

        _registroPontos.Contextualizar(pontoRegistrado);
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

    [Given(@"que o trabalhador registrou a entrada no expediente às '([^']*)'")]
    public async Task GivenQueOTrabalhadorRegistrouAEntradaNoExpedienteAs(DateTime entrada)
    {
        var userId = Guid.Parse("d2fc8313-9bdc-455c-bf29-ccf709a2a692").ToString();

        var transaction = new TransactionContext(userId);

        var contrato = _db.Contratos.FirstOrDefault();

        var pontoEntrada = RegistroPontosFacade.CriaPonto(transaction);

        contrato.QualificaPonto(pontoEntrada);

        pontoEntrada.DataHora = entrada;

        pontoEntrada.MomentoId = MomentoEnum.Entrada;

        _db.Pontos.Add(pontoEntrada);

        await _db.SaveChangesAsync();
    }

    [Given(@"que o trabalhador registrou a saída no expediente às '([^']*)'")]
    public async Task GivenQueOTrabalhadorRegistrouASaidaNoExpedienteAs(DateTime saida)
    {
        var userId = Guid.Parse("d2fc8313-9bdc-455c-bf29-ccf709a2a692").ToString();

        var transaction = new TransactionContext(userId);

        var contrato = _db.Contratos.FirstOrDefault();

        var pontoSaida = RegistroPontosFacade.CriaPonto(transaction);

        contrato.QualificaPonto(pontoSaida);

        pontoSaida.DataHora = saida;

        pontoSaida.MomentoId = MomentoEnum.Saida;

        _db.Pontos.Add(pontoSaida);

        await _db.SaveChangesAsync();
    }

    [Given(@"que existe um ponto qualificado com o contrato '([^']*)'")]
    public async Task GivenQueExisteUmPontoQualificadoComOContrato(string nomeContrato)
    {
        var userId = Guid.Parse("d2fc8313-9bdc-455c-bf29-ccf709a2a692").ToString();

        var transaction = new TransactionContext(userId);

        var contrato = _db.Contratos.FirstOrDefault(x => x.Nome == nomeContrato);

        var pontoEntrada = RegistroPontosFacade.CriaPonto(transaction);

        contrato.QualificaPonto(pontoEntrada);

        pontoEntrada.DataHora = DateTime.Now;

        pontoEntrada.MomentoId = MomentoEnum.Entrada;

        _db.Pontos.Add(pontoEntrada);

        await _db.SaveChangesAsync();
    }

    [When(@"o trabalhador iniciar um registro de ponto")]
    public void WhenOTrabalhadorIniciarUmRegistroDePonto()
    {
        var ponto = _registroPontosInterface.SolicitarRegistroPonto();

        _registroPontos.Contextualizar(ponto);
    }

    [Then(@"o sistema deverá apresentar um ponto novo")]
    public void ThenOSistemaDeveraApresentarUmPontoNovo()
    {
        _registroPontos.Ponto.Should().NotBeNull();
    }

    [When(@"o trabalhador registrar o ponto")]
    public void WhenOTrabalhadorRegistrarOPonto()
    {
        var contrato = _db.Contratos.FirstOrDefault();

        contrato.QualificaPonto(_registroPontos.Ponto);

        _registroPontosInterface.RegistrarPonto(_registroPontos.Ponto);

        var pontoRegistrado = _db.Pontos
            .Include(x => x.Contrato)
            .FirstOrDefault(x => x.DataHora == _registroPontos.Ponto.DataHora);

        _registroPontos.Contextualizar(pontoRegistrado);
    }

    [Then(@"o sistema deverá registrar o ponto como esperado")]
    public void ThenOSistemaDeveraRegistrarOPontoComoEsperado()
    {
        _registroPontos.Especificacao.CompareToSet(_db.Pontos);
    }

    [Then(@"o ponto deverá ser registrado como esperado")]
    public void ThenOPontoDeveraSerRegistradoComoEsperado()
    {
        _registroPontos.Especificacao.CompareToSet(_db.Pontos);
    }

    [Then(@"a tentativa de registrar o ponto deverá falhar com um erro ""([^""]*)""")]
    public void ThenATentativaDeRegistrarOPontoDeveraFalharComUmErro(string erro)
    {
        _registroPontos.Erro.Should().Be(erro);
    }

    [Then(@"o ponto deverá ser qualificado pelo contrato '([^']*)'")]
    public void ThenOPontoDeveraSerQualificadoPeloContrato(string nomeContrato)
    {
        _registroPontos.Ponto.Contrato.Should().NotBeNull();

        _registroPontos.Ponto.Contrato.Nome.Should().Be(nomeContrato);
    }

    [Then(@"a data do ponto deverá ser '([^']*)'")]
    public void ThenADataDoPontoDeveraSer(DateTime dataHora)
    {
        //TODO: _registroPontos.Ponto.DataHora.Should().Be(dataHora);
    }

    [Then(@"a pausa do ponto deverá ser '([^']*)'")]
    public void ThenAPausaDoPontoDeveraSer(PausaEnum pausa)
    {
        _registroPontos.Ponto.PausaId.Should().Be(pausa);
    }

    [Then(@"o momento do ponto deverá ser de '([^']*)'")]
    public void ThenOMomentoDoPontoDeveraSerDe(MomentoEnum momento)
    {
        _registroPontos.Ponto.MomentoId.Should().Be(momento);
    }

    [Then(@"o ponto não deverá indicar que foi uma pausa")]
    public void ThenOPontoNaoDeveraIndicarQueFoiUmaPausa()
    {
        _registroPontos.Ponto.PausaId.Should().BeNull();
    }

    [Then(@"o ponto não deverá indicar que foi estimado")]
    public void ThenOPontoNaoDeveraIndicarQueFoiEstimado()
    {
        _registroPontos.Ponto.Estimado.Should().BeFalse();
    }

    [Then(@"a observação do ponto deverá ser:")]
    [Then(@"o ponto deverá ter uma observação como:")]
    public void ThenAObservacaoDoPontoDeveraSer(string observacao)
    {
        _registroPontos.Ponto.Observacao.Should().Be(observacao);
    }

    [Then(@"o ponto não deverá ter uma observação")]
    public void ThenOPontoNaoDeveraTerUmaObservacao()
    {
        _registroPontos.Ponto.Observacao.Should().BeNull();
    }
}

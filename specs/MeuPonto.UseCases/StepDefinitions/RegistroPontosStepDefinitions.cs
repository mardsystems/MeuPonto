using MeuPonto.Data;
using MeuPonto.Drivers;
using MeuPonto.Support;
using Microsoft.EntityFrameworkCore;
using TechTalk.SpecFlow.Assist;
using Timesheet.Features.RegistroPontos;
using Timesheet.Models.Pontos;

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
        var ponto = _registroPontosInterface.IniciarRegistroPonto();

        var contrato = _db.Contratos.FirstOrDefault();

        contrato.QualificaPonto(ponto);

        var datetime = DateTime.Now;

        ponto.DataHora = new DateTime(datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, 0);
        ponto.MomentoId = MomentoEnum.Entrada;

        _registroPontos.Inicia(ponto);
    }

    [When(@"o trabalhador iniciar um registro de ponto")]
    public void WhenOTrabalhadorIniciarUmRegistroDePonto()
    {
        var ponto = _registroPontosInterface.IniciarRegistroPonto();

        _registroPontos.Inicia(ponto);
    }

    [When(@"o trabalhador registrar o ponto como:")]
    public void WhenOTrabalhadorRegistrarOPontoComo(Table table)
    {
        _registroPontos.Especificacao = table;

        var ponto = _registroPontos.Ponto;

        var data = table.CreateInstance(() => new RegistroPontoData
        {
            DataHora = ponto.DataHora,
            Contrato = ponto.Contrato?.Nome,
            MomentoId = ponto.MomentoId,
            PausaId = ponto.PausaId,
            Estimado = ponto.Estimado
        });

        var contrato = _db.Contratos.First(x => x.Nome == data.Contrato);

        contrato.QualificaPonto(ponto);

        ponto.DataHora = data.DataHora;
        ponto.MomentoId = data.MomentoId;
        ponto.PausaId = data.PausaId;
        ponto.Estimado = data.Estimado;

        _registroPontosInterface.RegistrarPonto(ponto);

        var pontoRegistrado = _db.Pontos
            .Include(x => x.Contrato)
            .FirstOrDefault(x => x.DataHora == ponto.DataHora);

        _registroPontos.Define(pontoRegistrado);
    }

    [When(@"o trabalhador tentar registrar o ponto como:")]
    public void WhenOTrabalhadorTentarRegistrarOPontoComo(Table table)
    {
        _registroPontos.Especificacao = table;

        var ponto = _registroPontos.Ponto;

        var data = table.CreateInstance(() => new RegistroPontoData
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
            _registroPontosInterface.MarcarPonto(ponto);
        }
        catch (Exception ex)
        {
            _registroPontos.Erro = ex.Message;
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

        _registroPontos.Define(pontoRegistrado);
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

    [Given(@"que a data/hora do relógio é '([^']*)'")]
    public void GivenQueADataHoraDoRelogioE(DateTime dataHora)
    {
        _registroPontos.DataHora = dataHora;
    }

    [When(@"o trabalhador iniciar uma marcação de ponto")]
    public void WhenOTrabalhadorIniciarUmaMarcacaoDePonto()
    {
        var ponto = _registroPontosInterface.IniciarMarcacaoPonto();

        _registroPontos.Inicia(ponto);
    }

    [Given(@"que existe uma marcação de ponto em andamento")]
    public void GivenQueExisteUmaMarcacaoDePontoEmAndamento()
    {
        var ponto = _registroPontosInterface.IniciarMarcacaoPonto();

        var contrato = _db.Contratos.FirstOrDefault();

        contrato.QualificaPonto(ponto);

        ponto.MomentoId = MomentoEnum.Entrada;

        _registroPontos.Inicia(ponto);
    }

    [Then(@"um ponto deverá ser criado")]
    public void ThenUmPontoDeveraSerCriado()
    {
        _registroPontos.Ponto.Should().NotBeNull();
    }

    [When(@"o trabalhador marcar o ponto")]
    public void WhenOTrabalhadorMarcarOPonto()
    {
        var contrato = _db.Contratos.FirstOrDefault();

        contrato.QualificaPonto(_registroPontos.Ponto);

        _registroPontosInterface.MarcarPonto(_registroPontos.Ponto);

        var pontoRegistrado = _db.Pontos
            .Include(x => x.Contrato)
            .FirstOrDefault(x => x.DataHora == _registroPontos.Ponto.DataHora);

        _registroPontos.Define(pontoRegistrado);
    }

    [When(@"o trabalhador marcar o ponto como:")]
    public void WhenOTrabalhadorMarcarOPontoComo(Table table)
    {
        _registroPontos.Especificacao = table;

        var ponto = _registroPontos.Ponto;

        var data = table.CreateInstance(() => new MarcacaoPontoData
        {
            Contrato = ponto.Contrato?.Nome,
            MomentoId = ponto.MomentoId,
            PausaId = ponto.PausaId
        });

        var contrato = _db.Contratos.First(x => x.Nome == data.Contrato);

        contrato.QualificaPonto(ponto);

        ponto.MomentoId = data.MomentoId;
        ponto.PausaId = data.PausaId;

        _registroPontosInterface.MarcarPonto(ponto);

        var pontoRegistrado = _db.Pontos
            .Include(x => x.Contrato)
            .FirstOrDefault(x => x.DataHora == ponto.DataHora);

        _registroPontos.Define(pontoRegistrado);
    }

    [When(@"o trabalhador tentar marcar o ponto como:")]
    public void WhenOTrabalhadorTentarMarcarOPontoComo(Table table)
    {
        _registroPontos.Especificacao = table;

        var ponto = _registroPontos.Ponto;

        var data = table.CreateInstance(() => new MarcacaoPontoData
        {
            Contrato = ponto.Contrato?.Nome,
            MomentoId = ponto.MomentoId,
            PausaId = ponto.PausaId
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

        try
        {
            _registroPontosInterface.MarcarPonto(ponto);
        }
        catch (Exception ex)
        {
            _registroPontos.Erro = ex.Message;
        }
    }

    [When(@"o trabalhador marcar o ponto com a seguinte observação:")]
    public void WhenOTrabalhadorMarcarOPontoComASeguinteObservacao(string observacao)
    {
        var ponto = _registroPontos.Ponto;

        ponto.Observacao = observacao;

        _registroPontosInterface.MarcarPonto(ponto);

        var pontoRegistrado = _db.Pontos
            .Include(x => x.Contrato)
            .FirstOrDefault(x => x.DataHora == ponto.DataHora);

        _registroPontos.Define(pontoRegistrado);
    }

    [Then(@"o ponto deverá ser registrado como esperado")]
    public void ThenOPontoDeveraSerRegistradoComoEsperado()
    {
        _registroPontos.Especificacao.CompareToSet(_db.Pontos);
    }

    [Then(@"a tentativa de marcar o ponto deverá falhar com um erro ""([^""]*)""")]
    public void ThenATentativaDeMarcarOPontoDeveraFalharComUmErro(string erro)
    {
        _registroPontos.Erro.Should().Be(erro);
    }

    [Then(@"o ponto deverá ser qualificado pelo contrato '([^']*)'")]
    public void ThenOPontoDeveraSerQualificadoPeloContrato(string nome)
    {
        _registroPontos.Ponto.Contrato.Should().NotBeNull();

        _registroPontos.Ponto.Contrato.Nome.Should().Be(nome);
    }

    [Then(@"a data do ponto deverá ser '([^']*)'")]
    public void ThenADataDoPontoDeveraSer(DateTime data)
    {
        //TODO: _registroPontos.PontoRegistrado.Data.Should().Be(data);
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

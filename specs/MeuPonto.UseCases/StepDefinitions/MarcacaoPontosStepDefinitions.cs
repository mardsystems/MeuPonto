using MeuPonto.Data;
using MeuPonto.Drivers;
using MeuPonto.Support;
using Microsoft.EntityFrameworkCore;
using TechTalk.SpecFlow.Assist;
using MeuPonto.Features.RegistroPontos;
using MeuPonto.Models.Pontos;

namespace MeuPonto.StepDefinitions;

[Binding]
public class MarcacaoPontosStepDefinitions
{
    private readonly ScenarioContext _scenario;
    private readonly RegistroPontosContext _registroPontos;
    private readonly MarcacaoPontosDriver _marcacaoPontosInterface;
    private readonly MeuPontoDbContext _db;

    public MarcacaoPontosStepDefinitions(
        ScenarioContext scenario,
        RegistroPontosContext registroPontos,
        MarcacaoPontosDriver marcacaoPontosInterface,
        MeuPontoDbContext db)
    {
        _scenario = scenario;
        _registroPontos = registroPontos;
        _marcacaoPontosInterface = marcacaoPontosInterface;
        _db = db;
    }

    [Given(@"que existe uma marcação de ponto em andamento")]
    public void GivenQueExisteUmaMarcacaoDePontoEmAndamento()
    {
        var ponto = _marcacaoPontosInterface.SolicitarMarcacaoPonto();

        var contrato = _db.Contratos.FirstOrDefault();

        contrato.QualificaPonto(ponto);

        var datetime = DateTime.Now;

        ponto.DataHora = new DateTime(datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, 0);
        ponto.MomentoId = MomentoEnum.Entrada;

        _registroPontos.Contextualizar(ponto);
    }

    [When(@"o trabalhador solicitar uma marcação de ponto")]
    public void WhenOTrabalhadorSolicitarUmaMarcacaoDePonto()
    {
        var ponto = _marcacaoPontosInterface.SolicitarMarcacaoPonto();

        _registroPontos.Contextualizar(ponto);
    }

    [When(@"o trabalhador marcar o ponto como:")]
    public void WhenOTrabalhadorMarcarOPontoComo(Table table)
    {
        _registroPontos.Especificar(table);

        var ponto = _registroPontos.Ponto;

        var data = table.CreateInstance(() => new MarcacaoPontoData
        {
            Contrato = ponto.Contrato?.Nome,
            MomentoId = ponto.MomentoId,
            PausaId = ponto.PausaId
        });

        var contrato = _db.Contratos.FirstOrDefault(x => x.Nome == data.Contrato);

        contrato.QualificaPonto(ponto);

        ponto.MomentoId = data.MomentoId;
        ponto.PausaId = data.PausaId;

        _marcacaoPontosInterface.MarcarPonto(ponto);

        var pontoRegistrado = _db.Pontos
            .Include(x => x.Contrato)
            .FirstOrDefault(x => x.DataHora == ponto.DataHora);

        _registroPontos.Contextualizar(pontoRegistrado);
    }

    [When(@"o trabalhador tentar marcar o ponto como:")]
    public void WhenOTrabalhadorTentarMarcarOPontoComo(Table table)
    {
        _registroPontos.Especificar(table);

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
            _marcacaoPontosInterface.MarcarPonto(ponto);
        }
        catch (Exception ex)
        {
            _registroPontos.CapturarErro(ex.Message);
        }
    }

    [When(@"o trabalhador marcar o ponto com a seguinte observação:")]
    public void WhenOTrabalhadorMarcarOPontoComASeguinteObservacao(string observacao)
    {
        var ponto = _registroPontos.Ponto;

        var contrato = _db.Contratos.FirstOrDefault();

        contrato.QualificaPonto(ponto);

        ponto.Observacao = observacao;

        _marcacaoPontosInterface.MarcarPonto(ponto);

        var pontoRegistrado = _db.Pontos
            .Include(x => x.Contrato)
            .FirstOrDefault(x => x.DataHora == ponto.DataHora);

        _registroPontos.Contextualizar(pontoRegistrado);
    }

    [When(@"o trabalhador marcar o ponto")]
    public void WhenOTrabalhadorMarcarOPonto()
    {
        var contrato = _db.Contratos.FirstOrDefault();

        contrato.QualificaPonto(_registroPontos.Ponto);

        _marcacaoPontosInterface.MarcarPonto(_registroPontos.Ponto);

        var ponto = _db.Pontos
            .Include(x => x.Contrato)
            .FirstOrDefault(x => x.DataHora == _registroPontos.Ponto.DataHora);

        _registroPontos.Contextualizar(ponto);
    }

    [Then(@"o sistema deverá marcar o ponto como esperado")]
    public void ThenOSistemaDeveraMarcarOPontoComoEsperado()
    {
        _registroPontos.Especificacao.CompareToSet(_db.Pontos);
    }

    [Then(@"a tentativa de marcar o ponto deverá falhar com um erro ""([^""]*)""")]
    public void ThenATentativaDeMarcarOPontoDeveraFalharComUmErro(string erro)
    {
        _registroPontos.Erro.Should().Be(erro);
    }
}

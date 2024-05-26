using MeuPonto.Data;
using MeuPonto.Drivers;
using MeuPonto.Support;
using System.Transactions;
using TechTalk.SpecFlow.Assist;
using MeuPonto.Features.CadastroEmpregadores;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.StepDefinitions;

[Binding]
public class CadastroDeEmpregadoresStepDefinitions
{
    private readonly ScenarioContext _scenario;
    private readonly TransactionContext _transaction;
    private readonly GestaoContratosContext _gestaoContratos;
    private readonly GestaoContratosDriver _gestaoContratosInterface;
    private readonly CadastroEmpregadoresContext _cadastroEmpregadores;
    private readonly CadastroEmpregadoresDriver _cadastroEmpregadoresInterface;
    private readonly MeuPontoDbContext _db;

    public CadastroDeEmpregadoresStepDefinitions(
        ScenarioContext scenario,
        TransactionContext transaction,
        GestaoContratosContext gestaoContratos,
        GestaoContratosDriver gestaoContratosInterface,
        CadastroEmpregadoresContext cadastroEmpregadores,
        CadastroEmpregadoresDriver cadastroEmpregadoresInterface,
        MeuPontoDbContext db)
    {
        _scenario = scenario;
        _transaction = transaction;
        _gestaoContratos = gestaoContratos;
        _gestaoContratosInterface = gestaoContratosInterface;
        _cadastroEmpregadores = cadastroEmpregadores;
        _cadastroEmpregadoresInterface = cadastroEmpregadoresInterface;
        _db = db;
    }

    [Given(@"que existe um empregador cadastrado '([^']*)'")]
    public void GivenQueExisteUmEmpregadorCadastrado(string nomeEmpregador)
    {
        var empregador = _db.Empregadores.FirstOrDefault(x => x.Nome == nomeEmpregador);

        if (empregador == null)
        {
            empregador = _transaction.CriaEmpregador();

            empregador.Nome = nomeEmpregador;

            _db.Empregadores.Add(empregador);
            _db.SaveChanges();
        }

        _cadastroEmpregadores.Contextualizar(empregador);
    }

    [Given(@"que existe um cadastro de empregador em andamento")]
    public void GivenQueExisteUmCadastroDeEmpregadorEmAndamento()
    {
        GivenQueExisteUmCadastroDeEmpregadorEmAndamento("Empregador Novo");
    }

    [Given(@"que existe um cadastro de empregador em andamento '([^']*)'")]
    public void GivenQueExisteUmCadastroDeEmpregadorEmAndamento(string nomeEmpregador)
    {
        var empregador = _cadastroEmpregadoresInterface.SolicitarCadastroEmpregador();

        empregador.Nome = nomeEmpregador;

        _cadastroEmpregadores.Contextualizar(empregador);
    }

    [When(@"o trabalhador solicitar a abertura de um contrato a partir de um empregador")]
    public void WhenOTrabalhadorSolicitarAAberturaDeUmContratoAPartirDeUmEmpregador()
    {
        var contrato = _gestaoContratosInterface.SolicitarAbrerturaContrato(); // TODO: Incluir o empregador

        _gestaoContratos.Contextualizar(contrato);
    }

    [When(@"o trabalhador solicitar o cadastro de um empregador")]
    public void WhenOTrabalhadorSolicitarOCadastroDeUmEmpregador()
    {
        var empregador = _cadastroEmpregadoresInterface.SolicitarCadastroEmpregador();

        _cadastroEmpregadores.Contextualizar(empregador);
    }

    [Then(@"o sistema deverá apresentar um empregador novo")]
    public void ThenOSistemaDeveraApresentarUmEmpregadorNovo()
    {
        _cadastroEmpregadores.Empregador.Should().NotBeNull();
    }

    [When(@"o trabahador cadastrar o empregador '([^']*)'")]
    public void WhenOTrabahadorCadastrarOEmpregador(string nome)
    {
        var empregador = _cadastroEmpregadoresInterface.SolicitarCadastroEmpregador();

        //_cadastroEmpregadores.Inicia(empregador);



        //var empregador = _cadastroEmpregadores.Empregador;

        //var data = table.CreateInstance(() => new AberturaContratoData
        //{
        //    Nome = empregador.Nome,
        //});

        //empregador.Nome = data.Nome;

        empregador.Nome = nome;

        _cadastroEmpregadoresInterface.CadastrarEmpregador(empregador);

        //var empregadorCadastrado = _db.Empregadores.FirstOrDefault(x => x.Nome == empregador.Nome);

        //_cadastroEmpregadores.Define(empregadorCadastrado);
    }

    [When(@"o trabalhador cadastrar o empregador como:")]
    public void WhenOTrabalhadorCadastrarOEmpregadorComo(Table table)
    {
        _cadastroEmpregadores.Especificar(table);

        var empregador = _cadastroEmpregadores.Empregador;

        var data = table.CreateInstance(() => new AberturaContratoData
        {
            Nome = empregador.Nome,
        });

        empregador.Nome = data.Nome;

        _cadastroEmpregadoresInterface.CadastrarEmpregador(empregador);

        var empregadorCadastrado = _db.Empregadores.FirstOrDefault(x => x.Nome == empregador.Nome);

        _cadastroEmpregadores.Contextualizar(empregadorCadastrado);
    }

    [When(@"o trabalhador abrir o contrato feito com um empregador como:")]
    public void WhenOTrabalhadorAbrirOContratoFeitoComUmEmpregadorComo(Table table)
    {
        _gestaoContratos.Especificar(table);

        var contrato = _gestaoContratos.Contrato;

        var data = table.CreateInstance(() => new AberturaContratoData
        {
            Nome = contrato.Nome ?? "Contrato Padrão",
            Ativo = contrato.Ativo,
            Empregador = contrato.Empregador?.Nome,
            Domingo = contrato.JornadaTrabalhoSemanalPrevista.Semana[0].Tempo,
            Segunda = contrato.JornadaTrabalhoSemanalPrevista.Semana[1].Tempo,
            Terca = contrato.JornadaTrabalhoSemanalPrevista.Semana[2].Tempo,
            Quarta = contrato.JornadaTrabalhoSemanalPrevista.Semana[3].Tempo,
            Quinta = contrato.JornadaTrabalhoSemanalPrevista.Semana[4].Tempo,
            Sexta = contrato.JornadaTrabalhoSemanalPrevista.Semana[5].Tempo,
            Sabado = contrato.JornadaTrabalhoSemanalPrevista.Semana[6].Tempo,
        });

        contrato.Nome = data.Nome;
        contrato.Ativo = data.Ativo;

        var empregador = _db.Empregadores.FirstOrDefault(x => x.Nome == data.Empregador);

        contrato.FeitoCom(empregador);

        contrato.JornadaTrabalhoSemanalPrevista.Semana[0].Tempo = data.Domingo;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[1].Tempo = data.Segunda;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[2].Tempo = data.Terca;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[3].Tempo = data.Quarta;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[4].Tempo = data.Quinta;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[5].Tempo = data.Sexta;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[6].Tempo = data.Sabado;

        _gestaoContratosInterface.AbrirContrato(contrato);

        //_db.ChangeTracker.Clear();

        var contratoAberto = _db.Contratos
            .Include(x => x.Empregador)
            .FirstOrDefault(x => x.Nome == contrato.Nome);

        _gestaoContratos.Contextualizar(contratoAberto);
    }

    [Then(@"o sistema deverá registrar o empregador como esperado")]
    public void ThenOSistemaDeveraRegistrarOEmpregadorComoEsperado()
    {
        _cadastroEmpregadores.Especificacao.CompareToSet(_db.Empregadores);
    }

    [Then(@"o nome do empregador deverá ser '([^']*)'")]
    public void ThenONomeDoEmpregadorDeveraSer(string nome)
    {
        _cadastroEmpregadores.Empregador.Nome.Should().Be(nome);
    }

    [Then(@"o empregador '([^']*)' deverá ser associado ao contrato")]
    public void ThenOEmpregadorDeveraSerAssociadoAoContrato(string nomeEmpregador)
    {
        _gestaoContratos.Contrato.Empregador.Should().NotBeNull();

        _gestaoContratos.Contrato.Empregador.Nome.Should().Be(nomeEmpregador);
    }

    [Then(@"o contrato deverá ser feito com o empregador '([^']*)'")]
    public void ThenOContratoDeveraSerFeitoComOEmpregador(string nomeEmpregador)
    {
        _gestaoContratos.Contrato.Empregador.Should().NotBeNull();

        _gestaoContratos.Contrato.Empregador.Nome.Should().Be(nomeEmpregador);
    }
}

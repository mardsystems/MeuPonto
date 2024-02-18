using MeuPonto.Data;
using MeuPonto.Drivers;
using MeuPonto.Support;
using System.Transactions;
using TechTalk.SpecFlow.Assist;
using Timesheet.Features.CadastroEmpregadores;

namespace MeuPonto.StepDefinitions;

[Binding]
public class CadastroDeEmpregadoresStepDefinitions
{
    private readonly ScenarioContext _scenario;
    private readonly TransactionContext _transaction;
    private readonly CadastroEmpregadoresContext _cadastroEmpregadores;
    private readonly CadastroEmpregadoresDriver _cadastroEmpregadoresInterface;
    private readonly MeuPontoDbContext _db;

    public CadastroDeEmpregadoresStepDefinitions(
        ScenarioContext scenario,
        TransactionContext transaction,
        CadastroEmpregadoresContext cadastroEmpregadores,
        CadastroEmpregadoresDriver cadastroEmpregadoresInterface,
        MeuPontoDbContext db)
    {
        _scenario = scenario;
        _transaction = transaction;
        _cadastroEmpregadores = cadastroEmpregadores;
        _cadastroEmpregadoresInterface = cadastroEmpregadoresInterface;
        _db = db;
    }

    [Given(@"que existe um empregador cadastrado '([^']*)'")]
    public void GivenQueExisteUmEmpregadorCadastrado(string nome)
    {
        var empregador = _db.Empregadores.FirstOrDefault(x => x.Nome == nome);

        if (empregador == null)
        {
            empregador = _transaction.CriaEmpregador();

            empregador.Nome = nome;

            _db.Empregadores.Add(empregador);
            _db.SaveChanges();
        }

        _cadastroEmpregadores.Define(empregador);
    }

    [When(@"o trabalhador iniciar um cadastro de empregador")]
    public void WhenOTrabalhadorIniciarUmCadastroDeEmpregador()
    {
        var empregador = _cadastroEmpregadoresInterface.IniciarCadastroEmpregador();

        _cadastroEmpregadores.Iniciar(empregador);
    }

    [Then(@"um empregador deverá ser criado")]
    public void ThenUmEmpregadorDeveraSerCriado()
    {
        _cadastroEmpregadores.Empregador.Should().NotBeNull();
    }

    [When(@"o trabahador cadastrar o empregador '([^']*)'")]
    public void WhenOTrabahadorCadastrarOEmpregador(string nome)
    {
        var empregador = _cadastroEmpregadoresInterface.IniciarCadastroEmpregador();

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
        var empregador = _cadastroEmpregadores.Empregador;

        var data = table.CreateInstance(() => new AberturaContratoData
        {
            Nome = empregador.Nome,
        });

        empregador.Nome = data.Nome;

        _cadastroEmpregadoresInterface.CadastrarEmpregador(empregador);

        var empregadorCadastrado = _db.Empregadores.FirstOrDefault(x => x.Nome == empregador.Nome);

        _cadastroEmpregadores.Define(empregadorCadastrado);
    }

    [Then(@"o nome do empregador deverá ser '([^']*)'")]
    public void ThenONomeDoEmpregadorDeveraSer(string nome)
    {
        _cadastroEmpregadores.Empregador.Nome.Should().Be(nome);
    }
}

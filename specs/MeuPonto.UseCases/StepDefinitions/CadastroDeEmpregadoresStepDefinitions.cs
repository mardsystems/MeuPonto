using MeuPonto.Data;
using MeuPonto.Drivers;
using MeuPonto.Support;
using TechTalk.SpecFlow.Assist;

namespace MeuPonto.StepDefinitions;

[Binding]
public class CadastroDeEmpregadoresStepDefinitions
{
    private readonly ScenarioContext _scenario;

    private readonly CadastroEmpregadoresContext _cadastroEmpregadores;

    private readonly CadastroEmpregadoresDriver _cadastroEmpregadoresInterface;

    private readonly MeuPontoDbContext _db;

    public CadastroDeEmpregadoresStepDefinitions(
        ScenarioContext scenario,
        CadastroEmpregadoresContext cadastroEmpregadores,
        CadastroEmpregadoresDriver cadastroEmpregadoresInterface,
        MeuPontoDbContext db)
    {
        _scenario = scenario;

        _cadastroEmpregadores = cadastroEmpregadores;

        _cadastroEmpregadoresInterface = cadastroEmpregadoresInterface;

        _db = db;
    }

    [When(@"o trabalhador iniciar um cadastro de empregador")]
    public void WhenOTrabalhadorIniciarUmCadastroDeEmpregador()
    {
        var empregador = _cadastroEmpregadoresInterface.IniciarCadastroEmpregador();

        _cadastroEmpregadores.Inicia(empregador);
    }

    [Then(@"um empregador deverá ser criado")]
    public void ThenUmEmpregadorDeveraSerCriado()
    {
        _cadastroEmpregadores.Empregador.Should().NotBeNull();
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

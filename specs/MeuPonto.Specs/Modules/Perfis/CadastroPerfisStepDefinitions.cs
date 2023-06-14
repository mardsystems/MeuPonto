using MeuPonto.Data;
using TechTalk.SpecFlow.Assist;

namespace MeuPonto.Modules.Perfis;

[Binding]
public class CadastroPerfisStepDefinitions
{
    private readonly ScenarioContext _scenario;

    private readonly CadastroPerfisContext _cadastroPerfis;

    private readonly CadastroPerfisInterface _cadastroPerfisInterface;

    private readonly MeuPontoDbContext _db;

    public CadastroPerfisStepDefinitions(
        ScenarioContext scenario,
        CadastroPerfisContext cadastroPerfis,
        CadastroPerfisInterface cadastroPerfisInterface,
        MeuPontoDbContext db)
    {
        this._scenario = scenario;

        _cadastroPerfis = cadastroPerfis;

        _cadastroPerfisInterface = cadastroPerfisInterface;

        _db = db;
    }

    [Given(@"que o trabalhador não tem nenhum perfil cadastrado")]
    public void GivenQueOTrabalhadorNaoTemNenhumPerfilCadastrado()
    {
        _db.Perfis.Count().Should().Be(0);

        //_home.CriacaoPerfilAnchor.Should().NotBeNull("quando não existe nenhum perfil cadastrado a tela inicial deve ter um link de criação de perfil");
    }

    [Given(@"que o trabalhador tem um perfil cadastrado")]
    [Given(@"que o trabalhador já tem um perfil cadastrado")]
    public async Task GivenQueOTrabalhadorTemUmPerfilCadastrado()
    {
        var perfil = CadastroPerfisStub.ObtemPerfil();

        _db.Perfis.Add(perfil);
        await _db.SaveChangesAsync();

        _cadastroPerfis.ConsideraQueExiste(perfil);
    }

    [Given(@"que o trabalhador tem um perfil cadastrado com o nome '([^']*)'")]
    [Given(@"que o trabalhador já tem um perfil cadastrado com o nome '([^']*)'")]
    public async Task GivenQueOTrabalhadorTemUmPerfilCadastradoComONome(string nome)
    {
        var perfil = CadastroPerfisStub.ObtemPerfilComNome(nome);

        _db.Perfis.Add(perfil);
        await _db.SaveChangesAsync();

        _cadastroPerfis.ConsideraQueExiste(perfil);
    }

    [Given(@"que o trabalhador tem um perfil cadastrado com a matrícula '([^']*)'")]
    [Given(@"que o trabalhador já tem um perfil cadastrado com a matrícula '([^']*)'")]
    public async Task GivenQueOTrabalhadorTemUmPerfilCadastradoComAMatricula(string matricula)
    {
        var perfil = CadastroPerfisStub.ObtemPerfilComMatricula(matricula);

        _db.Perfis.Add(perfil);
        await _db.SaveChangesAsync();

        _cadastroPerfis.ConsideraQueExiste(perfil);
    }

    [Given(@"que o trabalhador tem um perfil cadastrado com a seguinte jornada de trabalho semanal prevista:")]
    public async Task GivenQueOTrabalhadorTemUmPerfilCadastradoComASeguinteJornadaDeTrabalhoSemanalPrevista(Table table)
    {
        var semana = table.CreateSet<(DayOfWeek diaSemana, TimeSpan tempo)>();

        var perfil = CadastroPerfisStub.ObtemPerfilComJornadaTrabalhoSemanalPrevistaDe(semana);

        _db.Perfis.Add(perfil);
        await _db.SaveChangesAsync();

        _cadastroPerfis.ConsideraQueExiste(perfil);
    }

    [Given(@"que o trabalhador identifica na lista o perfil cadastrado")]
    public void GivenQueOTrabalhadorIdentificaNaListaOPerfilCadastrado()
    {
        //_cadastroPerfis.Identifica(_cadastroPerfis.PerfilCadastrado);
    }
}

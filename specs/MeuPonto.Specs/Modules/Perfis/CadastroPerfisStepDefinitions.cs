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
        _db.Perfis.Add(_cadastroPerfis.Perfil);
        await _db.SaveChangesAsync();
    }

    [Given(@"que o trabalhador tem um perfil cadastrado com o nome '([^']*)'")]
    [Given(@"que o trabalhador já tem um perfil cadastrado com o nome '([^']*)'")]
    public async Task GivenQueOTrabalhadorTemUmPerfilCadastradoComONome(string nome)
    {
        _cadastroPerfis.Perfil.Nome = nome;

        _db.Perfis.Add(_cadastroPerfis.Perfil);
        await _db.SaveChangesAsync();
    }

    [Given(@"que o trabalhador tem um perfil cadastrado com a matrícula '([^']*)'")]
    [Given(@"que o trabalhador já tem um perfil cadastrado com a matrícula '([^']*)'")]
    public async Task GivenQueOTrabalhadorTemUmPerfilCadastradoComAMatricula(string matricula)
    {
        _cadastroPerfis.Perfil.Matricula = matricula;

        _db.Perfis.Add(_cadastroPerfis.Perfil);
        await _db.SaveChangesAsync();
    }

    [Given(@"que o trabalhador tem um perfil cadastrado com a seguinte jornada de trabalho semanal prevista:")]
    public async Task GivenQueOTrabalhadorTemUmPerfilCadastradoComASeguinteJornadaDeTrabalhoSemanalPrevista(Table table)
    {
        DefineJornadaTrabalhoSemanalPrevistaDe(table);

        _db.Perfis.Add(_cadastroPerfis.Perfil);
        await _db.SaveChangesAsync();
    }


    public void DefineJornadaTrabalhoSemanalPrevistaDe(Table table)
    {
        var semana = table.CreateSet<(DayOfWeek diaSemana, TimeSpan tempo)>();

        var daysOfWeek = Enum.GetValues<DayOfWeek>();

        foreach (var dayOfWeek in daysOfWeek)
        {
            var jornadaTrabalhoDiaria = semana.SingleOrDefault(x => x.diaSemana == dayOfWeek);

            var i = (int)dayOfWeek;

            if (jornadaTrabalhoDiaria == default)
            {
                _cadastroPerfis.Perfil.JornadaTrabalhoSemanalPrevista.Semana[i].Tempo = new TimeSpan(0, 0, 0);
            }
            else
            {
                _cadastroPerfis.Perfil.JornadaTrabalhoSemanalPrevista.Semana[i].Tempo = jornadaTrabalhoDiaria.tempo;
            }
        }

        //_db.Perfis.Add(perfil);
        //await _db.SaveChangesAsync();

        ////

        //var document = await _angleSharp.GetDocumentAsync("/");

        ////

        //var perfisAnchor = (IHtmlAnchorElement)document.QuerySelector("a.perfis");

        //perfisAnchor.Should().NotBeNull("a tela inicial deve ter um link para os perfis");

        //_scenario["perfisAnchor"] = perfisAnchor;

        ////

        //var marcacaoPontoAnchor = (IHtmlAnchorElement)document.QuerySelector("a.marcacao.ponto");

        //marcacaoPontoAnchor.Should().NotBeNull("a tela inicial deve ter um link de marcação de ponto");

        //_scenario["marcacaoPontoAnchor"] = marcacaoPontoAnchor;

        ////

        //var aberturaFolhaPontoAnchor = (IHtmlAnchorElement)document.QuerySelector("a.abertura.folha.ponto");

        //aberturaFolhaPontoAnchor.Should().NotBeNull("a tela inicial deve ter um link de abertura de folha de ponto");

        //_scenario["aberturaFolhaPontoAnchor"] = aberturaFolhaPontoAnchor;
    }

    [Given(@"que o trabalhador identifica na lista o perfil cadastrado")]
    public void GivenQueOTrabalhadorIdentificaNaListaOPerfilCadastrado()
    {
        //_cadastroPerfis.Identifica(_cadastroPerfis.PerfilCadastrado);
    }
}

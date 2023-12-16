using MeuPonto.Data;
using MeuPonto.Drivers;
using MeuPonto.Support;
using TechTalk.SpecFlow.Assist;

namespace MeuPonto.StepDefinitions;

[Binding]
public class CadastroPerfisStepDefinitions
{
    private readonly ScenarioContext _scenario;

    private readonly CadastroPerfisContext _cadastroPerfis;

    private readonly CadastroPerfisDriver _cadastroPerfisInterface;

    private readonly MeuPontoDbContext _db;

    public CadastroPerfisStepDefinitions(
        ScenarioContext scenario,
        CadastroPerfisContext cadastroPerfis,
        CadastroPerfisDriver cadastroPerfisInterface,
        MeuPontoDbContext db)
    {
        _scenario = scenario;

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
        _cadastroPerfis.DefineNomePerfil(nome);

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

    [Given(@"que o nome do trabalhador é '([^']*)'")]
    public void GivenQueONomeDoTrabalhadorE(string nome)
    {
        _cadastroPerfis.Perfil.Nome = nome;
    }

    [Given(@"que o melhor nome que denota o vínculo entre o trabalhador e o empregador é '([^']*)'")]
    [Given(@"que o melhor nome que denota o novo vínculo entre o trabalhador e o empregador é '([^']*)'")]
    public void GivenQueOMelhorNomeQueDenotaOVinculoEntreOTrabalhadorEOEmpregadorE(string nome)
    {
        _cadastroPerfis.Perfil.Nome = nome;
    }

    [Given(@"que o trabalhador é o '([^']*)'")]
    public void GivenQueOTrabalhadorEO(string nome)
    {
        _cadastroPerfis.Perfil.Nome = nome;
    }

    [Given(@"que o horário de trabalho é de '([^']*)' a '([^']*)' das '([^']*)' às '([^']*)' com '([^']*)' de almoço")]
    public void GivenQueOHorarioDeTrabalhoEDeADasAsComDeAlmoco(DayOfWeek dayOfWeekInicio, DayOfWeek dayOfWeekTermino, TimeSpan horaInicio, TimeSpan horaTermino, TimeSpan tempoAlmoco)
    {
        var tempo = horaTermino - horaInicio - tempoAlmoco;

        var daysOfWeek = Enum.GetValues<DayOfWeek>();

        for (int i = (int)dayOfWeekInicio - 1; i < (int)dayOfWeekTermino; i++)
        {
            var dayOfWeek = daysOfWeek[i];

            _cadastroPerfis.Perfil.JornadaTrabalhoSemanalPrevista.Semana[i].Tempo = tempo;
        }
    }

    [Given(@"que o horário de trabalho de '([^']*)' é das '([^']*)' às '([^']*)'")]
    public void GivenQueOHorarioDeTrabalhoDeEDasAs(DayOfWeek dayOfWeek, TimeSpan horaInicio, TimeSpan horaTermino)
    {
        var tempo = horaTermino - horaInicio;

        var i = (int)dayOfWeek - 1;

        _cadastroPerfis.Perfil.JornadaTrabalhoSemanalPrevista.Semana[i].Tempo = tempo;
    }

    #region Criar Perfil

    [When(@"o trabalhador criar um perfil")]
    public void WhenOTrabalhadorCriarUmPerfil()
    {
        //_cadastroPerfisInterface.GoTo();

        _cadastroPerfisInterface.CriarPerfil(_cadastroPerfis.Perfil);

        var perfilCadastrado = _db.Perfis.FirstOrDefault(x => x.Nome == _cadastroPerfis.Perfil.Nome);

        _cadastroPerfis.Define(perfilCadastrado);
    }

    [Then(@"um perfil deverá ser cadastrado")]
    public void ThenUmPerfilDeveraSerCadastrado()
    {
        _cadastroPerfis.PerfilCadastrado.Should().NotBeNull();
    }

    #endregion

    #region Detalhar Perfil

    [When(@"o trabalhador detalhar o perfil")]
    public void WhenOTrabalhadorDetalharOPerfil()
    {
        var perfilDetalhado = _cadastroPerfisInterface.DetalharPerfil(_cadastroPerfis.NomePerfil);

        _cadastroPerfis.Define(perfilDetalhado);
    }

    [Then(@"o perfil deverá ser detalhado")]
    public void ThenOPerfilDeveraSerDetalhado()
    {
        _cadastroPerfis.PerfilCadastrado.Should().NotBeNull();
    }

    #endregion

    #region Editar Perfil

    [When(@"o trabalhador editar o perfil")]
    public void WhenOTrabalhadorEditarOPerfil()
    {
        _cadastroPerfisInterface.EditarPerfil(_cadastroPerfis.NomePerfil, _cadastroPerfis.Perfil);

        var perfilEdidado = _db.Perfis.FirstOrDefault(x => x.Nome == _cadastroPerfis.Perfil.Nome);

        _cadastroPerfis.Define(perfilEdidado);
    }

    [Then(@"o perfil deverá ser editado")]
    public void ThenOPerfilDeveraSerEditado()
    {
        _cadastroPerfis.PerfilCadastrado.Should().NotBeNull();
    }

    #endregion

    #region Excluir Perfil

    [When(@"o trabalhador excluir o perfil")]
    public void WhenOTrabalhadorExcluirOPerfil()
    {
        _cadastroPerfisInterface.ExcluirPerfil(_cadastroPerfis.NomePerfil);
    }

    [Then(@"o perfil deverá ser excluído")]
    public void ThenOPerfilDeveraSerExcluido()
    {
        var perfil = _db.Perfis.FirstOrDefault(x => x.Nome == _cadastroPerfis.Perfil.Nome);

        perfil.Should().BeNull();
    }

    #endregion

    [Then(@"o nome do perfil deverá ser '([^']*)'")]
    public void ThenONomeDoPerfilDeveraSer(string nome)
    {
        _cadastroPerfis.PerfilCadastrado.Nome.Should().Be(nome);
    }

    [Then(@"a jornada de trabalho semanal prevista deverá ser:")]
    public void ThenAJornadaDeTrabalhoSemanalPrevistaDeveraSer(Table jornadaTrabalhoSemanal)
    {
        var jornadaTrabalhoSemanalPrevista = _cadastroPerfis.PerfilCadastrado.JornadaTrabalhoSemanalPrevista;

        jornadaTrabalhoSemanal.CompareToSet(jornadaTrabalhoSemanalPrevista.Semana);
    }

    [Then(@"o tempo total da jornada de trabalho semanal prevista deverá ser '([^']*)'")]
    public void ThenOTempoTotalDaJornadaDeTrabalhoSemanalPrevistaDeveraSer(TimeSpan tempoTotal)
    {
        var jornadaTrabalhoSemanalPrevista = _cadastroPerfis.PerfilCadastrado.JornadaTrabalhoSemanalPrevista;

        jornadaTrabalhoSemanalPrevista.TempoTotal.Should().Be(tempoTotal);
    }

    [Then(@"o perfil não deverá ser excluído")]
    public void ThenOPerfilNaoDeveraSerExcluido()
    {
        var perfil = _db.Perfis.FirstOrDefault(x => x.Nome == _cadastroPerfis.Perfil.Nome);

        perfil.Should().NotBeNull();
    }
}

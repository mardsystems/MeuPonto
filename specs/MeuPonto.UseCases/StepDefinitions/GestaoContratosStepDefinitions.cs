using MeuPonto.Data;
using MeuPonto.Drivers;
using MeuPonto.Support;
using TechTalk.SpecFlow.Assist;

namespace MeuPonto.StepDefinitions;

[Binding]
public class GestaoContratosStepDefinitions
{
    private readonly ScenarioContext _scenario;

    private readonly GestaoContratosContext _gestaoContratos;

    private readonly GestaoContratosDriver _gestaoContratosInterface;

    private readonly MeuPontoDbContext _db;

    public GestaoContratosStepDefinitions(
        ScenarioContext scenario,
        GestaoContratosContext gestaoContratos,
        GestaoContratosDriver gestaoContratosInterface,
        MeuPontoDbContext db)
    {
        _scenario = scenario;

        _gestaoContratos = gestaoContratos;

        _gestaoContratosInterface = gestaoContratosInterface;

        _db = db;
    }

    [Given(@"que o trabalhador não tem nenhum contrato cadastrado")]
    public void GivenQueOTrabalhadorNaoTemNenhumContratoCadastrado()
    {
        _db.Contratos.Count().Should().Be(0);

        //_home.CriacaoContratoAnchor.Should().NotBeNull("quando não existe nenhum contrato cadastrado a tela inicial deve ter um link de criação de contrato");
    }

    [Given(@"que o trabalhador tem um contrato cadastrado")]
    [Given(@"que o trabalhador já tem um contrato cadastrado")]
    public async Task GivenQueOTrabalhadorTemUmContratoCadastrado()
    {
        _db.Contratos.Add(_gestaoContratos.Contrato);
        await _db.SaveChangesAsync();
    }

    [Given(@"que o trabalhador tem um contrato cadastrado com o nome '([^']*)'")]
    [Given(@"que o trabalhador já tem um contrato cadastrado com o nome '([^']*)'")]
    public async Task GivenQueOTrabalhadorTemUmContratoCadastradoComONome(string nome)
    {
        _gestaoContratos.DefineNomeContrato(nome);

        _db.Contratos.Add(_gestaoContratos.Contrato);
        await _db.SaveChangesAsync();
    }

    [Given(@"que o trabalhador tem um contrato cadastrado com a seguinte jornada de trabalho semanal prevista:")]
    public async Task GivenQueOTrabalhadorTemUmContratoCadastradoComASeguinteJornadaDeTrabalhoSemanalPrevista(Table table)
    {
        DefineJornadaTrabalhoSemanalPrevistaDe(table);

        _db.Contratos.Add(_gestaoContratos.Contrato);
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
                _gestaoContratos.Contrato.JornadaTrabalhoSemanalPrevista.Semana[i].Tempo = new TimeSpan(0, 0, 0);
            }
            else
            {
                _gestaoContratos.Contrato.JornadaTrabalhoSemanalPrevista.Semana[i].Tempo = jornadaTrabalhoDiaria.tempo;
            }
        }

        //_db.Contratos.Add(contrato);
        //await _db.SaveChangesAsync();

        ////

        //var document = await _angleSharp.GetDocumentAsync("/");

        ////

        //var contratosAnchor = (IHtmlAnchorElement)document.QuerySelector("a.contratos");

        //contratosAnchor.Should().NotBeNull("a tela inicial deve ter um link para os contratos");

        //_scenario["contratosAnchor"] = contratosAnchor;

        ////

        //var marcacaoPontoAnchor = (IHtmlAnchorElement)document.QuerySelector("a.marcacao.ponto");

        //marcacaoPontoAnchor.Should().NotBeNull("a tela inicial deve ter um link de marcação de ponto");

        //_scenario["marcacaoPontoAnchor"] = marcacaoPontoAnchor;

        ////

        //var aberturaFolhaPontoAnchor = (IHtmlAnchorElement)document.QuerySelector("a.abertura.folha.ponto");

        //aberturaFolhaPontoAnchor.Should().NotBeNull("a tela inicial deve ter um link de abertura de folha de ponto");

        //_scenario["aberturaFolhaPontoAnchor"] = aberturaFolhaPontoAnchor;
    }

    [Given(@"que o trabalhador identifica na lista o contrato cadastrado")]
    public void GivenQueOTrabalhadorIdentificaNaListaOContratoCadastrado()
    {
        //_gestaoContratos.Identifica(_gestaoContratos.ContratoCadastrado);
    }

    [Given(@"que o nome do trabalhador é '([^']*)'")]
    public void GivenQueONomeDoTrabalhadorE(string nome)
    {
        _gestaoContratos.Contrato.Nome = nome;
    }

    [Given(@"que o melhor nome que denota o vínculo entre o trabalhador e o empregador é '([^']*)'")]
    [Given(@"que o melhor nome que denota o novo vínculo entre o trabalhador e o empregador é '([^']*)'")]
    public void GivenQueOMelhorNomeQueDenotaOVinculoEntreOTrabalhadorEOEmpregadorE(string nome)
    {
        _gestaoContratos.Contrato.Nome = nome;
    }

    [Given(@"que o trabalhador é o '([^']*)'")]
    public void GivenQueOTrabalhadorEO(string nome)
    {
        _gestaoContratos.Contrato.Nome = nome;
    }

    [Given(@"que o horário de trabalho é de '([^']*)' a '([^']*)' das '([^']*)' às '([^']*)' com '([^']*)' de almoço")]
    public void GivenQueOHorarioDeTrabalhoEDeADasAsComDeAlmoco(DayOfWeek dayOfWeekInicio, DayOfWeek dayOfWeekTermino, TimeSpan horaInicio, TimeSpan horaTermino, TimeSpan tempoAlmoco)
    {
        var tempo = horaTermino - horaInicio - tempoAlmoco;

        var daysOfWeek = Enum.GetValues<DayOfWeek>();

        for (int i = (int)dayOfWeekInicio - 1; i < (int)dayOfWeekTermino; i++)
        {
            var dayOfWeek = daysOfWeek[i];

            _gestaoContratos.Contrato.JornadaTrabalhoSemanalPrevista.Semana[i].Tempo = tempo;
        }
    }

    [Given(@"que o horário de trabalho de '([^']*)' é das '([^']*)' às '([^']*)'")]
    public void GivenQueOHorarioDeTrabalhoDeEDasAs(DayOfWeek dayOfWeek, TimeSpan horaInicio, TimeSpan horaTermino)
    {
        var tempo = horaTermino - horaInicio;

        var i = (int)dayOfWeek - 1;

        _gestaoContratos.Contrato.JornadaTrabalhoSemanalPrevista.Semana[i].Tempo = tempo;
    }

    #region Criar Contrato

    [When(@"o trabalhador criar um contrato")]
    public void WhenOTrabalhadorCriarUmContrato()
    {
        //_gestaoContratosInterface.GoTo();

        _gestaoContratosInterface.CriarContrato(_gestaoContratos.Contrato);

        var contratoCadastrado = _db.Contratos.FirstOrDefault(x => x.Nome == _gestaoContratos.Contrato.Nome);

        _gestaoContratos.Define(contratoCadastrado);
    }

    [Then(@"um contrato deverá ser cadastrado")]
    public void ThenUmContratoDeveraSerCadastrado()
    {
        _gestaoContratos.ContratoCadastrado.Should().NotBeNull();
    }

    #endregion

    #region Detalhar Contrato

    [When(@"o trabalhador detalhar o contrato")]
    public void WhenOTrabalhadorDetalharOContrato()
    {
        var contratoDetalhado = _gestaoContratosInterface.DetalharContrato(_gestaoContratos.NomeContrato);

        _gestaoContratos.Define(contratoDetalhado);
    }

    [Then(@"o contrato deverá ser detalhado")]
    public void ThenOContratoDeveraSerDetalhado()
    {
        _gestaoContratos.ContratoCadastrado.Should().NotBeNull();
    }

    #endregion

    #region Editar Contrato

    [When(@"o trabalhador editar o contrato")]
    public void WhenOTrabalhadorEditarOContrato()
    {
        _gestaoContratosInterface.EditarContrato(_gestaoContratos.NomeContrato, _gestaoContratos.Contrato);

        var contratoEdidado = _db.Contratos.FirstOrDefault(x => x.Nome == _gestaoContratos.Contrato.Nome);

        _gestaoContratos.Define(contratoEdidado);
    }

    [Then(@"o contrato deverá ser editado")]
    public void ThenOContratoDeveraSerEditado()
    {
        _gestaoContratos.ContratoCadastrado.Should().NotBeNull();
    }

    #endregion

    #region Excluir Contrato

    [When(@"o trabalhador excluir o contrato")]
    public void WhenOTrabalhadorExcluirOContrato()
    {
        _gestaoContratosInterface.ExcluirContrato(_gestaoContratos.NomeContrato);
    }

    [Then(@"o contrato deverá ser excluído")]
    public void ThenOContratoDeveraSerExcluido()
    {
        var contrato = _db.Contratos.FirstOrDefault(x => x.Nome == _gestaoContratos.Contrato.Nome);

        contrato.Should().BeNull();
    }

    #endregion

    [Then(@"o nome do contrato deverá ser '([^']*)'")]
    public void ThenONomeDoContratoDeveraSer(string nome)
    {
        _gestaoContratos.ContratoCadastrado.Nome.Should().Be(nome);
    }

    [Then(@"a jornada de trabalho semanal prevista deverá ser:")]
    public void ThenAJornadaDeTrabalhoSemanalPrevistaDeveraSer(Table jornadaTrabalhoSemanal)
    {
        var jornadaTrabalhoSemanalPrevista = _gestaoContratos.ContratoCadastrado.JornadaTrabalhoSemanalPrevista;

        jornadaTrabalhoSemanal.CompareToSet(jornadaTrabalhoSemanalPrevista.Semana);
    }

    [Then(@"o tempo total da jornada de trabalho semanal prevista deverá ser '([^']*)'")]
    public void ThenOTempoTotalDaJornadaDeTrabalhoSemanalPrevistaDeveraSer(TimeSpan tempoTotal)
    {
        var jornadaTrabalhoSemanalPrevista = _gestaoContratos.ContratoCadastrado.JornadaTrabalhoSemanalPrevista;

        jornadaTrabalhoSemanalPrevista.TempoTotal.Should().Be(tempoTotal);
    }

    [Then(@"o contrato não deverá ser excluído")]
    public void ThenOContratoNaoDeveraSerExcluido()
    {
        var contrato = _db.Contratos.FirstOrDefault(x => x.Nome == _gestaoContratos.Contrato.Nome);

        contrato.Should().NotBeNull();
    }
}

using MeuPonto.Data;
using TechTalk.SpecFlow.Assist;

namespace MeuPonto.Modules.Perfis;

[Binding]
public class CriarPerfilStepDefinitions
{
    private readonly ScenarioContext _scenario;

    private readonly CadastroPerfisContext _cadastroPerfis;

    private readonly CadastroPerfisInterface _cadastroPerfisInterface;

    private readonly MeuPontoDbContext _db;

    public CriarPerfilStepDefinitions(
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

    [Given(@"que a matrícula do trabalhador é '([^']*)'")]
    public void GivenQueAMatriculaDoTrabalhadorE(string matricula)
    {
        _cadastroPerfis.Perfil.Matricula = matricula;
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
        _cadastroPerfis.Perfil.Matricula = "0001";
        _cadastroPerfis.Perfil.Nome = nome;
    }

    [Given(@"que o horário de trabalho é de '([^']*)' a '([^']*)' das '([^']*)' às '([^']*)' com '([^']*)' de almoço")]
    public void GivenQueOHorarioDeTrabalhoEDeADasAsComDeAlmoco(DayOfWeek dayOfWeekInicio, DayOfWeek dayOfWeekTermino, TimeSpan horaInicio, TimeSpan horaTermino, TimeSpan tempoAlmoco)
    {
        var tempo = (horaTermino - horaInicio) - tempoAlmoco;

        var daysOfWeek = Enum.GetValues<DayOfWeek>();

        for (int i = (int)dayOfWeekInicio; i <= (int)dayOfWeekTermino; i++)
        {
            var dayOfWeek = daysOfWeek[i];

            AdicionaJornadaTrabalhoDiaria(_cadastroPerfis.Perfil.JornadaTrabalhoSemanalPrevista, dayOfWeek, tempo);
        }
    }

    [Given(@"que o horário de trabalho de '([^']*)' é das '([^']*)' às '([^']*)'")]
    public void GivenQueOHorarioDeTrabalhoDeEDasAs(DayOfWeek dayOfWeek, TimeSpan horaInicio, TimeSpan horaTermino)
    {
        var tempo = (horaTermino - horaInicio);

        AdicionaJornadaTrabalhoDiaria(_cadastroPerfis.Perfil.JornadaTrabalhoSemanalPrevista, dayOfWeek, tempo);
    }

    private static void AdicionaJornadaTrabalhoDiaria(JornadaTrabalhoSemanal jornadaTrabalhoSemanal, DayOfWeek dayOfWeek, TimeSpan tempo)
    {
        jornadaTrabalhoSemanal.Semana.Add(new JornadaTrabalhoDiaria
        {
            DiaSemana = dayOfWeek,
            Tempo = tempo
        });
    }

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

    [Then(@"a matrícula do perfil deverá ser '([^']*)'")]
    public void ThenAMatriculaDoPerfilDeveraSer(string matricula)
    {
        _cadastroPerfis.PerfilCadastrado.Matricula.Should().Be(matricula);
    }

    [Then(@"o nome do perfil deverá ser '([^']*)'")]
    public void ThenONomeDoPerfilDeveraSer(string nome)
    {
        _cadastroPerfis.PerfilCadastrado.Nome.Should().Be(nome);
    }

    [Then(@"a jornada de trabalho semanal prevista deverá ser:")]
    public void ThenAJornadaDeTrabalhoSemanalPrevistaDeveraSer(Table table)
    {
        var jornadaTrabalhoSemanal = table.CreateInstance<(DayOfWeek diaSemana, TimeSpan tempo)>();

        //

        var jornadaTrabalhoSemanalPrevista = _cadastroPerfis.PerfilCadastrado.Preve();

        var jornadaTrabalhoDiaria = jornadaTrabalhoSemanalPrevista.Semana.FirstOrDefault(x => x.DiaSemana == jornadaTrabalhoSemanal.diaSemana);

        jornadaTrabalhoDiaria.Should().NotBeNull();

        jornadaTrabalhoDiaria.Tempo.Should().Be(jornadaTrabalhoSemanal.tempo, jornadaTrabalhoDiaria.DiaSemana.ToString());
    }

    [Then(@"o tempo total da jornada de trabalho semanal prevista deverá ser '([^']*)'")]
    public void ThenOTempoTotalDaJornadaDeTrabalhoSemanalPrevistaDeveraSer(TimeSpan tempoTotal)
    {
        var jornadaTrabalhoSemanalPrevista = _cadastroPerfis.PerfilCadastrado.Preve();

        jornadaTrabalhoSemanalPrevista.TempoTotal.Should().Be(tempoTotal);
    }
}

using AngleSharp.Html.Dom;
using MeuPonto.Helpers;
using MeuPonto.Support;

namespace MeuPonto.Modules.Perfis;

public class CadastroPerfisPageDriver : CadastroPerfisInterface
{
    private readonly AngleSharpContext _angleSharp;

    public IHtmlDocument Document { get; private set; }

    public IHtmlAnchorElement CriacaoPerfilAnchor { get; private set; }

    public IHtmlAnchorElement DetalhePerfilAnchor { get; private set; }

    public IHtmlAnchorElement EdicaoPerfilAnchor { get; private set; }

    public IHtmlAnchorElement ExclusaoPerfilAnchor { get; private set; }

    public CadastroPerfisPageDriver(AngleSharpContext angleSharp)
    {
        _angleSharp = angleSharp;
    }

    public void GoTo()
    {
        Document = _angleSharp.GetDocument("/Perfis");

        //

        CriacaoPerfilAnchor = Document.GetAnchor("Criacao.Perfil");

        CriacaoPerfilAnchor.Should().NotBeNull("a tela de perfis deve ter um link de criação de perfil");
    }

    private void Identifica(string nomePerfil)
    {
        var table = Document.GetTable("Perfis");

        var tableRow = table.GetTableRowByDataName(nomePerfil);

        //

        DetalhePerfilAnchor = tableRow.GetAnchor("Detalhe");

        DetalhePerfilAnchor.Should().NotBeNull("a lista de perfis deve ter um link de detalhe do perfil cadastrado");

        //

        EdicaoPerfilAnchor = tableRow.GetAnchor("Edicao");

        EdicaoPerfilAnchor.Should().NotBeNull("a lista de perfis deve ter um link de edição do perfil cadastrado");

        //

        ExclusaoPerfilAnchor = tableRow.GetAnchor("Exclusao");

        ExclusaoPerfilAnchor.Should().NotBeNull("a lista de perfis deve ter um link de exclusão do perfil cadastrado");
    }

    public void CriarPerfil(Concepts.Perfil perfil)
    {
        GoTo();

        Document = _angleSharp.GetDocument(CriacaoPerfilAnchor.Href);

        var form = Document.GetForm();

        form.GetInput("Perfil.Nome").Value = perfil.Nome;

        var daysOfWeek = Enum.GetValues<DayOfWeek>();

        foreach (var dayOfWeek in daysOfWeek)
        {
            var jornadaTrabalhoDiaria = perfil.IdentificaVinculo().Preve().Semana.SingleOrDefault(x => x.DiaSemana == dayOfWeek);

            var i = (int)dayOfWeek;

            if (jornadaTrabalhoDiaria == default)
            {
                form.GetInput($"Perfil.JornadaTrabalhoSemanalPrevista.Semana[{i}].Tempo").Value = "00:00";
            }
            else
            {
                form.GetInput($"Perfil.JornadaTrabalhoSemanalPrevista.Semana[{i}].Tempo").Value = jornadaTrabalhoDiaria.Tempo.Value.ToString("hh\\:mm");
            }
        }

        var submitButton = form.GetSubmitButton();

        var resultPage = _angleSharp.Send(form, submitButton);

        Document = _angleSharp.GetDocument(resultPage);
        
        var hasErrors = Document.GetValidationErrors().Any();

        hasErrors.Should().BeFalse();

        //var perfilCadastrado = await ObtemDetalhes();

        //return perfilCadastrado;
    }

    public Concepts.Perfil DetalharPerfil(string nomePerfil)
    {
        GoTo();

        Identifica(nomePerfil);

        Document = _angleSharp.GetDocument(DetalhePerfilAnchor.Href);

        var perfilDetalhado = ObtemDetalhes();

        return perfilDetalhado;
    }

    public void EditarPerfil(string nomePerfil, Concepts.Perfil perfilCadastrado)
    {
        GoTo();

        Identifica(nomePerfil);

        Document = _angleSharp.GetDocument(EdicaoPerfilAnchor.Href);

        var form = Document.GetForm();

        form.GetInput("Perfil.Nome").Value = perfilCadastrado.Nome;

        var submitButton = form.GetSubmitButton();

        var resultPage = _angleSharp.Send(form, submitButton);

        Document = _angleSharp.GetDocument(resultPage);

        var hasErrors = Document.GetValidationErrors().Any();

        hasErrors.Should().BeFalse();

        //var perfilEditado = await ObtemDetalhes();

        //return perfilEditado;
    }

    private Perfil ObtemDetalhes()
    {
        var hasErrors = Document.GetValidationErrors().Any();

        hasErrors.Should().BeFalse();

        var dl = Document.GetDefinitionList("Perfil");

        var perfilCadastrado = new Perfil
        {
            Nome = dl.GetDataListItem("Nome").GetString(),
        };

        AdicionaJornadaTrabalhoDiaria(perfilCadastrado.JornadaTrabalhoSemanalPrevista, DayOfWeek.Sunday);
        AdicionaJornadaTrabalhoDiaria(perfilCadastrado.JornadaTrabalhoSemanalPrevista, DayOfWeek.Monday);
        AdicionaJornadaTrabalhoDiaria(perfilCadastrado.JornadaTrabalhoSemanalPrevista, DayOfWeek.Tuesday);
        AdicionaJornadaTrabalhoDiaria(perfilCadastrado.JornadaTrabalhoSemanalPrevista, DayOfWeek.Wednesday);
        AdicionaJornadaTrabalhoDiaria(perfilCadastrado.JornadaTrabalhoSemanalPrevista, DayOfWeek.Thursday);
        AdicionaJornadaTrabalhoDiaria(perfilCadastrado.JornadaTrabalhoSemanalPrevista, DayOfWeek.Friday);
        AdicionaJornadaTrabalhoDiaria(perfilCadastrado.JornadaTrabalhoSemanalPrevista, DayOfWeek.Saturday);

        return perfilCadastrado;
    }

    private void AdicionaJornadaTrabalhoDiaria(JornadaTrabalhoSemanal jornadaTrabalhoSemanal, DayOfWeek dayOfWeek)
    {
        var dl = Document.GetDefinitionList($"JornadaTrabalhoDiaria.{dayOfWeek}");

        if (dl != null)
        {
            var diaSemana = dl.GetTermListItem("TempoDiaSemana").GetString();
            var tempo = dl.GetDataListItem("TempoDiaSemana").GetString();

            jornadaTrabalhoSemanal.Semana.Add(new JornadaTrabalhoDiaria
            {
                DiaSemana = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), diaSemana),
                Tempo = TimeSpan.Parse(tempo)
            });
        }
    }

    public void ExcluirPerfil(string nomePerfil)
    {
        GoTo();

        Identifica(nomePerfil);

        Document = _angleSharp.GetDocument(ExclusaoPerfilAnchor.Href);

        var form = Document.GetForm();

        var submitButton = form.GetSubmitButton();

        var resultPage = _angleSharp.Send(form, submitButton);
    }
}

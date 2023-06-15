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

    public async Task GoTo()
    {
        Document = await _angleSharp.GetDocumentAsync("/Perfis");

        //

        CriacaoPerfilAnchor = Document.GetAnchor("Criacao.Perfil");

        CriacaoPerfilAnchor.Should().NotBeNull("a tela de perfis deve ter um link de criação de perfil");
    }

    private void Identifica(Concepts.Perfil perfil)
    {
        var table = Document.GetTable("Perfis");

        var tableRow = table.GetTableRowByDataName(perfil.Nome);

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

    public async Task CriarPerfil(Concepts.Perfil perfil)
    {
        await GoTo();

        Document = await _angleSharp.GetDocumentAsync(CriacaoPerfilAnchor.Href);

        var form = Document.GetForm();

        form.GetInput("Perfil.Nome").Value = perfil.Nome;
        form.GetInput("Perfil.Matricula").Value = perfil.Matricula;

        var daysOfWeek = Enum.GetValues<DayOfWeek>();

        foreach (var dayOfWeek in daysOfWeek)
        {
            var jornadaTrabalhoDiaria = perfil.Preve().Semana.SingleOrDefault(x => x.DiaSemana == dayOfWeek);

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

        var resultPage = await _angleSharp.SendAsync(form, submitButton);

        resultPage.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        Document = await _angleSharp.GetDocumentAsync(resultPage);
        
        var hasErrors = Document.GetValidationErrors().Any();

        hasErrors.Should().BeFalse();

        //var perfilCadastrado = await ObtemDetalhes();

        //return perfilCadastrado;
    }

    public async Task<Concepts.Perfil> DetalharPerfil(Concepts.Perfil perfilCadastrado)
    {
        await GoTo();

        Identifica(perfilCadastrado);

        Document = await _angleSharp.GetDocumentAsync(DetalhePerfilAnchor.Href);

        var perfilDetalhado = await ObtemDetalhes();

        return perfilDetalhado;
    }

    public async Task EditarPerfil(Concepts.Perfil perfilCadastrado)
    {
        await GoTo();

        Identifica(perfilCadastrado);

        Document = await _angleSharp.GetDocumentAsync(EdicaoPerfilAnchor.Href);

        var form = Document.GetForm();

        form.GetInput("Perfil.Matricula").Value = perfilCadastrado.Matricula;

        var submitButton = form.GetSubmitButton();

        var resultPage = await _angleSharp.SendAsync(form, submitButton);

        resultPage.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        Document = await _angleSharp.GetDocumentAsync(resultPage);

        var hasErrors = Document.GetValidationErrors().Any();

        hasErrors.Should().BeFalse();

        //var perfilEditado = await ObtemDetalhes();

        //return perfilEditado;
    }

    private async Task<Perfil> ObtemDetalhes()
    {
        var hasErrors = Document.GetValidationErrors().Any();

        hasErrors.Should().BeFalse();

        var dl = Document.GetDefinitionList("Perfil");

        var perfilCadastrado = new Perfil
        {
            Nome = dl.GetDataListItem("Nome").GetString(),
            Matricula = dl.GetDataListItem("Matricula").GetString(),
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

    public async Task ExcluirPerfil(Concepts.Perfil perfilCadastrado)
    {
        await GoTo();

        Identifica(perfilCadastrado);

        Document = await _angleSharp.GetDocumentAsync(ExclusaoPerfilAnchor.Href);

        var form = Document.GetForm();

        var submitButton = form.GetSubmitButton();

        var resultPage = await _angleSharp.SendAsync(form, submitButton);

        resultPage.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
}

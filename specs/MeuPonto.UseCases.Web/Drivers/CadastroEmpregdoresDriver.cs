using AngleSharp.Html.Dom;
using MeuPonto.Helpers;
using MeuPonto.Support;
using Timesheet.Models.Contratos;

namespace MeuPonto.Drivers;

public class CadastroEmpregadoresDriver
{
    private readonly AngleSharpContext _angleSharp;

    public IHtmlDocument Document { get; private set; }

    public IHtmlAnchorElement CadastroEmpregadorAnchor { get; private set; }

    public IHtmlAnchorElement DetalheEmpregadorAnchor { get; private set; }

    public IHtmlAnchorElement EdicaoEmpregadorAnchor { get; private set; }

    public IHtmlAnchorElement ExclusaoEmpregadorAnchor { get; private set; }

    public CadastroEmpregadoresDriver(AngleSharpContext angleSharp)
    {
        _angleSharp = angleSharp;
    }

    public void GoTo()
    {
        Document = _angleSharp.GetDocument("/Contratos/Empregadores");

        //

        CadastroEmpregadorAnchor = Document.GetAnchor("Criacao.Empregador");

        CadastroEmpregadorAnchor.Should().NotBeNull("a tela de empregadores deve ter um link de criação de empregador");
    }

    private void Identifica(string nomeEmpregador)
    {
        var table = Document.GetTable("Empregadores");

        var tableRow = table.GetTableRowByDataName(nomeEmpregador);

        //

        DetalheEmpregadorAnchor = tableRow.GetAnchor("Detalhe");

        DetalheEmpregadorAnchor.Should().NotBeNull("a lista de empregadores deve ter um link de detalhe do empregador cadastrado");

        //

        EdicaoEmpregadorAnchor = tableRow.GetAnchor("Edicao");

        EdicaoEmpregadorAnchor.Should().NotBeNull("a lista de empregadores deve ter um link de edição do empregador cadastrado");

        //

        ExclusaoEmpregadorAnchor = tableRow.GetAnchor("Exclusao");

        ExclusaoEmpregadorAnchor.Should().NotBeNull("a lista de empregadores deve ter um link de exclusão do empregador cadastrado");
    }

    public Empregador IniciarCadastroEmpregador()
    {
        GoTo();

        Document = _angleSharp.GetDocument(CadastroEmpregadorAnchor.Href);

        var form = Document.GetForm();

        var empregador = new Empregador
        {
            Nome = form.GetInput("CadastroEmpregador.Nome").Value,
        };

        return empregador;
    }

    public void CadastrarEmpregador(Empregador empregador)
    {
        GoTo();

        Document = _angleSharp.GetDocument(CadastroEmpregadorAnchor.Href);

        var form = Document.GetForm();

        form.GetInput("CadastroEmpregador.Nome").Value = empregador.Nome;

        var submitButton = form.GetSubmitButton();

        var resultPage = _angleSharp.Send(form, submitButton);

        Document = _angleSharp.GetDocument(resultPage);

        var hasErrors = Document.GetValidationErrors().Any();

        if (hasErrors)
        {
            var erros = Document.GetValidationErrors();

            var span = erros.FirstSpan();

            throw new Exception(span.InnerHtml);
        }
        else
        {
            //var empregadorCadastrado = await ObtemDetalhes();

            //return empregadorCadastrado;
        }
    }

    public Empregador DetalharEmpregador(string nomeEmpregador)
    {
        GoTo();

        Identifica(nomeEmpregador);

        Document = _angleSharp.GetDocument(DetalheEmpregadorAnchor.Href);

        var empregadorDetalhado = ObtemDetalhes();

        return empregadorDetalhado;
    }

    public void EditarEmpregador(string nomeEmpregador, Empregador empregadorCadastrado)
    {
        GoTo();

        Identifica(nomeEmpregador);

        Document = _angleSharp.GetDocument(EdicaoEmpregadorAnchor.Href);

        var form = Document.GetForm();

        form.GetInput("EdicaoEmpregador.Nome").Value = empregadorCadastrado.Nome;

        var submitButton = form.GetSubmitButton();

        var resultPage = _angleSharp.Send(form, submitButton);

        Document = _angleSharp.GetDocument(resultPage);

        var hasErrors = Document.GetValidationErrors().Any();

        hasErrors.Should().BeFalse();

        //var empregadorEditado = await ObtemDetalhes();

        //return empregadorEditado;
    }

    private Empregador ObtemDetalhes()
    {
        var hasErrors = Document.GetValidationErrors().Any();

        hasErrors.Should().BeFalse();

        var dl = Document.GetDefinitionList("Empregador");

        var empregadorCadastrado = new Empregador
        {
            Nome = dl.GetDataListItem("Nome").GetString(),
        };

        return empregadorCadastrado;
    }

    public void ExcluirEmpregador(string nomeEmpregador)
    {
        GoTo();

        Identifica(nomeEmpregador);

        Document = _angleSharp.GetDocument(ExclusaoEmpregadorAnchor.Href);

        var form = Document.GetForm();

        var submitButton = form.GetSubmitButton();

        var resultPage = _angleSharp.Send(form, submitButton);
    }
}

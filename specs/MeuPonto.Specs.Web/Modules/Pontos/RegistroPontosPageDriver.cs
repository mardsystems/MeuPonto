using AngleSharp.Html.Dom;
using MeuPonto.Helpers;
using MeuPonto.Support;

namespace MeuPonto.Modules.Pontos;

public class RegistroPontosPageDriver : RegistroPontosInterface
{
    private readonly AngleSharpContext _angleSharp;

    public IHtmlDocument Document { get; private set; }

    public IHtmlAnchorElement MarcacaoPontoAnchor { get; private set; }

    public RegistroPontosPageDriver(AngleSharpContext angleSharp)
    {
        _angleSharp = angleSharp;
    }

    public async Task GoTo()
    {
        Document = await _angleSharp.GetDocumentAsync("/Pontos");

        MarcacaoPontoAnchor = Document.GetAnchor("Marcacao.Ponto");

        MarcacaoPontoAnchor.Should().NotBeNull("o registro de pontos deve ter um link para a marcação de ponto");
    }

    public async Task<Ponto_> MarcarPonto(Ponto_ ponto)
    {
        await GoTo();

        Document = await _angleSharp.GetDocumentAsync(MarcacaoPontoAnchor.Href);

        var form = Document.GetForm();

        form.GetSelect("Ponto.PerfilId").GetOption(ponto.Perfil.Nome).IsSelected = true;
        form.GetInput("Ponto.Momento", ponto.Momento.Nome).IsChecked = true;
        if (ponto.Pausa != null)
        {
            form.GetInput("Ponto.Pausa", ponto.Pausa.Nome).IsChecked = true;
        }
        form.GetTextArea("Ponto.Observacao").Value = ponto.Observacao;

        var submitButton = form.GetSubmitButton();

        var resultPage = await _angleSharp.SendAsync(form, submitButton);

        resultPage.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        Document = await _angleSharp.GetDocumentAsync(resultPage);

        var pontoRegistrado = ObtemDetalhes();

        return pontoRegistrado;
    }

    private Ponto ObtemDetalhes()
    {
        var hasErrors = Document.GetValidationErrors().Any();

        hasErrors.Should().BeFalse();

        var dl = Document.GetDefinitionList("Ponto");

        //bool estimado = false;

        //bool.TryParse(((IHtmlSelectElement)dl.QuerySelector("dd.estimado > select")).Value, out estimado);

        var momentoValue = dl.GetDataListItem("Momento").GetInput().Value;
        var pausaValue = dl.GetDataListItem("Pausa").GetInput().Value;

        var pontoRegistrado = new Ponto
        {
            Perfil = new PerfilRef
            {
                Nome = dl.GetDataListItem("Perfil").GetString()
            },
            DataHora = DateTime.Parse(dl.GetDataListItem("DataHora").GetString()),
            Momento = (MomentoEnum)Enum.Parse(typeof(MomentoEnum), momentoValue),
            Pausa = string.IsNullOrEmpty(pausaValue) ? null : (PausaEnum)Enum.Parse(typeof(PausaEnum), pausaValue),
            Estimado = dl.GetDataListItem("Estimado").GetInput().IsChecked,
            Observacao = dl.GetDataListItem("Observacao").TextContent
        };

        return pontoRegistrado;
    }
}

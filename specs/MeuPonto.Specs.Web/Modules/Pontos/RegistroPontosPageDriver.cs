using AngleSharp.Html.Dom;
using MeuPonto.Helpers;
using MeuPonto.Modules.Perfis;
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

    public void GoTo()
    {
        Document = _angleSharp.GetDocument("/Pontos");

        MarcacaoPontoAnchor = Document.GetAnchor("Marcacao.Ponto");

        MarcacaoPontoAnchor.Should().NotBeNull("o registro de pontos deve ter um link para a marcação de ponto");
    }

    public Concepts.Ponto MarcarPonto(Concepts.Ponto ponto)
    {
        GoTo();

        Document = _angleSharp.GetDocument(MarcacaoPontoAnchor.Href);

        var form = Document.GetForm();

        form.GetSelect("Ponto.PerfilId").GetOption(ponto.Perfil.Nome).IsSelected = true;
        form.GetInput("Ponto.MomentoId", ponto.Momento.Nome).IsChecked = true;
        if (ponto.Pausa != null)
        {
            form.GetInput("Ponto.PausaId", ponto.Pausa.Nome).IsChecked = true;
        }
        form.GetTextArea("Ponto.Observacao").Value = ponto.Observacao;

        var submitButton = form.GetSubmitButton();

        var resultPage = _angleSharp.Send(form, submitButton);

        Document = _angleSharp.GetDocument(resultPage);

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
            Perfil = new Perfil
            {
                Nome = dl.GetDataListItem("Perfil").GetString()
            },
            DataHora = DateTime.Parse(dl.GetDataListItem("DataHora").GetString()),
            MomentoId = (MomentoEnum)Enum.Parse(typeof(MomentoEnum), momentoValue),
            PausaId = string.IsNullOrEmpty(pausaValue) ? null : (PausaEnum)Enum.Parse(typeof(PausaEnum), pausaValue),
            Estimado = dl.GetDataListItem("Estimado").GetInput().IsChecked,
            Observacao = dl.GetDataListItem("Observacao").TextContent
        };

        return pontoRegistrado;
    }
}

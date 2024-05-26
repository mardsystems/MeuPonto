using AngleSharp.Html.Dom;
using MeuPonto.Helpers;
using MeuPonto.Support;
using System.ComponentModel;
using MeuPonto.Models.Pontos;

namespace MeuPonto.Drivers;

public class MarcacaoPontosDriver
{
    private readonly AngleSharpContext _angleSharp;

    public IHtmlDocument Document { get; private set; }

    public IHtmlAnchorElement MarcacaoPontoAnchor { get; private set; }

    public MarcacaoPontosDriver(AngleSharpContext angleSharp)
    {
        _angleSharp = angleSharp;
    }

    public void GoTo()
    {
        Document = _angleSharp.GetDocument("/Pontos");

        MarcacaoPontoAnchor = Document.GetAnchor("Marcacao.Ponto");

        MarcacaoPontoAnchor.Should().NotBeNull("o registro de pontos deve ter um link para a marcação de ponto");
    }

    public Ponto SolicitarMarcacaoPonto()
    {
        GoTo();

        Document = _angleSharp.GetDocument(MarcacaoPontoAnchor.Href);

        var form = Document.GetForm();

        var momentoInput = form.GetCheckedRadioInput("Ponto.MomentoId");
        var pausaInput = form.GetCheckedRadioInput("Ponto.PausaId");

        var momentoValue = momentoInput == null ? null : momentoInput.Value;
        var pausaValue = pausaInput == null ? null : pausaInput.Value;

        var ponto = new Ponto
        {
            DataHora = DateTime.Parse(form.GetInput("Ponto.DataHora").Value),
            ContratoId = null,
            MomentoId = string.IsNullOrEmpty(momentoValue) ? null : (MomentoEnum)Enum.Parse(typeof(MomentoEnum), momentoValue),
            PausaId = string.IsNullOrEmpty(pausaValue) ? null : (PausaEnum)Enum.Parse(typeof(PausaEnum), pausaValue),
            Observacao = form.GetTextArea("Ponto.Observacao").Value
        };

        return ponto;
    }

    public void MarcarPonto(Ponto ponto)
    {
        GoTo();

        Document = _angleSharp.GetDocument(MarcacaoPontoAnchor.Href);

        var form = Document.GetForm();

        if (ponto.Contrato != null)
        {
            var contratoIdOption = form.GetSelect("Ponto.ContratoId").GetOption(ponto.Contrato.Nome);

            if (contratoIdOption != null)
            {
                contratoIdOption.IsSelected = true;
            }
        }
        if (ponto.MomentoId != null)
        {
            form.GetInput("Ponto.MomentoId", ponto.MomentoId.GetDisplayName()).IsChecked = true;
        }
        if (ponto.PausaId != null)
        {
            form.GetInput("Ponto.PausaId", ponto.PausaId.GetDisplayName()).IsChecked = true;
        }
        form.GetTextArea("Ponto.Observacao").Value = ponto.Observacao;

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
            //var pontoRegistrado = ObtemDetalhes();

            //return pontoRegistrado;
        }
    }
}

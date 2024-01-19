using AngleSharp.Html.Dom;
using MeuPonto.Helpers;
using MeuPonto.Support;
using System.ComponentModel;
using Timesheet.Models.Pontos;
using Timesheet.Models.Pontos.Comprovantes;

namespace MeuPonto.Drivers;

public class BackupComprovantesDriver
{
    private readonly AngleSharpContext _angleSharp;

    public IHtmlDocument Document { get; private set; }

    public IHtmlAnchorElement GuardarComprovanteAnchor { get; private set; }

    public BackupComprovantesDriver(AngleSharpContext angleSharp)
    {
        _angleSharp = angleSharp;
    }

    public void GoTo()
    {
        Document = _angleSharp.GetDocument("/Pontos/Comprovantes");

        GuardarComprovanteAnchor = Document.GetAnchor("Guardar.Comprovante");

        GuardarComprovanteAnchor.Should().NotBeNull("o backup de comprovantes deve ter um link para a guardar um comprovante");
    }

    public Comprovante EscanearComprovante(Stream imagem, Comprovante comprovante, Ponto ponto)
    {
        GoTo();

        Document = _angleSharp.GetDocument(GuardarComprovanteAnchor.Href);

        var form = Document.GetForm();

        using (var fileEntry = new FileEntry("Arquivo", "jpg", imagem))
        {
            form.GetInput("Imagem").Files.Add(fileEntry);
            form.GetSelect("Comprovante.TipoImagemId").GetOption(comprovante.TipoImagemId.GetDisplayName()).IsSelected = true;

            form.GetSelect("Ponto.ContratoId").GetOption(ponto.Contrato.Nome).IsSelected = true;
            form.GetInput("Ponto.DataHora").Value = ponto.DataHora.Value.ToString("yyyy-MM-dd\\THH:mm:ss");
            form.GetInput("Ponto.MomentoId", ponto.MomentoId.GetDisplayName()).IsChecked = true;
            if (ponto.PausaId != null)
            {
                form.GetInput("Ponto.PausaId", ponto.PausaId.GetDisplayName()).IsChecked = true;
            }
            form.GetTextArea("Ponto.Observacao").Value = ponto.Observacao;

            var submitButton = form.GetSubmitButton();

            var resultPage = _angleSharp.Send(form, submitButton);

            Document = _angleSharp.GetDocument(resultPage);
        }

        var comprovanteEscaneado = ObtemResultadoEscanearComprovante();

        return comprovanteEscaneado;
    }

    private Comprovante ObtemResultadoEscanearComprovante()
    {
        var hasErrors = Document.GetValidationErrors().Any();

        hasErrors.Should().BeFalse();

        var form = Document.GetForm();

        var momentoValue = form.GetInput("Ponto.MomentoId").Value;
        var pausaValue = form.GetInput("Ponto.PausaId").Value;

        var comprovanteEscaneado = new Comprovante
        {
            Ponto = new()
            {
                Contrato = new()
                {
                    Nome = form.GetSelect("Ponto.ContratoId").Value
                },
                DataHora = DateTime.Parse(form.GetInput("Ponto.DataHora").Value),
                MomentoId = (MomentoEnum)Enum.Parse(typeof(MomentoEnum), momentoValue),
                PausaId = string.IsNullOrEmpty(pausaValue) ? null : (PausaEnum)Enum.Parse(typeof(PausaEnum), pausaValue),
            }
        };



        return comprovanteEscaneado;
    }

    public Comprovante GuardarComprovante(Stream imagem, Comprovante comprovante, Ponto ponto)
    {
        GoTo();

        Document = _angleSharp.GetDocument(GuardarComprovanteAnchor.Href);

        var form = Document.GetForm();

        using (var fileEntry = new FileEntry("Arquivo", "jpg", imagem))
        {
            form.GetInput("Imagem").Files.Add(fileEntry);
            form.GetSelect("Comprovante.TipoImagemId").GetOption(comprovante.TipoImagemId.GetDisplayName()).IsSelected = true;

            form.GetSelect("Ponto.ContratoId").GetOption(ponto.Contrato.Nome).IsSelected = true;
            form.GetInput("Ponto.DataHora").Value = ponto.DataHora.Value.ToString("yyyy-MM-dd\\THH:mm:ss");
            form.GetInput("Ponto.MomentoId", ponto.MomentoId.GetDisplayName()).IsChecked = true;
            if (ponto.PausaId != null)
            {
                form.GetInput("Ponto.PausaId", ponto.PausaId.GetDisplayName()).IsChecked = true;
            }
            form.GetTextArea("Ponto.Observacao").Value = ponto.Observacao;

            var submitButton = form.GetSubmitButton("button.btn-primary");

            var resultPage = _angleSharp.Send(form, submitButton);

            Document = _angleSharp.GetDocument(resultPage);
        }

        var comprovanteGuardado = ObtemDetalhes();

        return comprovanteGuardado;
    }

    private Comprovante ObtemDetalhes()
    {
        var hasErrors = Document.GetValidationErrors().Any();

        hasErrors.Should().BeFalse();

        var dl = Document.GetDefinitionList("Ponto");

        var momentoValue = dl.GetDataListItem("Momento").GetInput().Value;
        var pausaValue = dl.GetDataListItem("Pausa").GetInput().Value;

        var comprovanteGuardado = new Comprovante
        {
            Ponto = new()
            {
                Contrato = new()
                {
                    Nome = dl.GetDataListItem("Contrato").GetString()
                },
                DataHora = DateTime.Parse(dl.GetDataListItem("DataHora").GetString()),
                MomentoId = (MomentoEnum)Enum.Parse(typeof(MomentoEnum), momentoValue),
                PausaId = string.IsNullOrEmpty(pausaValue) ? null : (PausaEnum)Enum.Parse(typeof(PausaEnum), pausaValue),
            }
        };

        return comprovanteGuardado;
    }
}

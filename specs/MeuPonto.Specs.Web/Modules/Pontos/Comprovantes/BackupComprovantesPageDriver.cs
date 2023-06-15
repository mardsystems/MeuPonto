using AngleSharp.Html.Dom;
using MeuPonto.Helpers;
using MeuPonto.Support;

namespace MeuPonto.Modules.Pontos.Comprovantes;

public class BackupComprovantesPageDriver : BackupComprovantesInterface
{
    private readonly AngleSharpContext _angleSharp;

    public IHtmlDocument Document { get; private set; }

    public IHtmlAnchorElement GuardarComprovanteAnchor { get; private set; }

    public BackupComprovantesPageDriver(AngleSharpContext angleSharp)
    {
        _angleSharp = angleSharp;
    }

    public async Task GoTo()
    {
        Document = await _angleSharp.GetDocumentAsync("/Pontos/Comprovantes");

        GuardarComprovanteAnchor = Document.GetAnchor("Guardar.Comprovante");

        GuardarComprovanteAnchor.Should().NotBeNull("o backup de comprovantes deve ter um link para a guardar um comprovante");
    }

    public async Task<Concepts.Comprovante> EscanearComprovante(Stream imagem, Concepts.Comprovante comprovante, Concepts.Ponto ponto)
    {
        await GoTo();

        Document = await _angleSharp.GetDocumentAsync(GuardarComprovanteAnchor.Href);

        var form = Document.GetForm();

        var perfil = ponto.EQualificadoPelo();

        using (var fileEntry = new FileEntry("Arquivo", "jpg", imagem))
        {
            form.GetInput("Imagem").Files.Add(fileEntry);
            form.GetSelect("Comprovante.TipoImagem").GetOption(comprovante.TipoImagem.Nome).IsSelected = true;
            form.GetInput("Comprovante.Numero").Value = comprovante.Numero;

            form.GetSelect("Ponto.PerfilId").GetOption(perfil.Nome).IsSelected = true;
            form.GetInput("Ponto.DataHora").Value = ponto.DataHora.Value.ToString("yyyy-MM-dd\\THH:mm:ss");
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
        }

        var comprovanteEscaneado = ObtemResultadoEscanearComprovante();

        return comprovanteEscaneado;
    }

    private Comprovante ObtemResultadoEscanearComprovante()
    {
        var hasErrors = Document.GetValidationErrors().Any();

        hasErrors.Should().BeFalse();

        var form = Document.GetForm();

        var momentoValue = form.GetInput("Ponto.Momento").Value;
        var pausaValue = form.GetInput("Ponto.Pausa").Value;

        var comprovanteEscaneado = new Comprovante
        {
            Ponto = new PontoRef
            {
                Perfil = new PerfilRef
                {
                    Nome = form.GetSelect("Ponto.PerfilId").Value
                },
                DataHora = DateTime.Parse(form.GetInput("Ponto.DataHora").Value),
                Momento = (MomentoEnum)Enum.Parse(typeof(MomentoEnum), momentoValue),
                Pausa = string.IsNullOrEmpty(pausaValue) ? null : (PausaEnum)Enum.Parse(typeof(PausaEnum), pausaValue),
            }
        };

        return comprovanteEscaneado;
    }

    public async Task<Concepts.Comprovante> GuardarComprovante(Stream imagem, Concepts.Comprovante comprovante, Concepts.Ponto ponto)
    {
        await GoTo();

        Document = await _angleSharp.GetDocumentAsync(GuardarComprovanteAnchor.Href);

        var form = Document.GetForm();

        var perfil = ponto.EQualificadoPelo();

        using (var fileEntry = new FileEntry("Arquivo", "jpg", imagem))
        {
            form.GetInput("Imagem").Files.Add(fileEntry);
            form.GetSelect("Comprovante.TipoImagem").GetOption(comprovante.TipoImagem.Nome).IsSelected = true;
            form.GetInput("Comprovante.Numero").Value = comprovante.Numero;

            form.GetSelect("Ponto.PerfilId").GetOption(perfil.Nome).IsSelected = true;
            form.GetInput("Ponto.DataHora").Value = ponto.DataHora.Value.ToString("yyyy-MM-dd\\THH:mm:ss");
            form.GetInput("Ponto.Momento", ponto.Momento.Nome).IsChecked = true;
            if (ponto.Pausa != null)
            {
                form.GetInput("Ponto.Pausa", ponto.Pausa.Nome).IsChecked = true;
            }
            form.GetTextArea("Ponto.Observacao").Value = ponto.Observacao;

            var submitButton = form.GetSubmitButton("button.btn-primary");

            var resultPage = await _angleSharp.SendAsync(form, submitButton);

            resultPage.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            Document = await _angleSharp.GetDocumentAsync(resultPage);
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
            Ponto = new PontoRef
            {
                Perfil = new PerfilRef
                {
                    Nome = dl.GetDataListItem("Perfil").GetString()
                },
                DataHora = DateTime.Parse(dl.GetDataListItem("DataHora").GetString()),
                Momento = (MomentoEnum)Enum.Parse(typeof(MomentoEnum), momentoValue),
                Pausa = string.IsNullOrEmpty(pausaValue) ? null : (PausaEnum)Enum.Parse(typeof(PausaEnum), pausaValue),
            }
        };

        return comprovanteGuardado;
    }
}

using AngleSharp.Html.Dom;
using MeuPonto.Helpers;
using MeuPonto.Support;
using System.Globalization;

namespace MeuPonto.Modules.Pontos.Folhas;

public class GestaoFolhasPageDriver : GestaoFolhasInterface
{
    private readonly AngleSharpContext _angleSharp;

    public IHtmlDocument Document { get; private set; }

    public IHtmlAnchorElement AberturaFolhaAnchor { get; private set; }

    public IHtmlAnchorElement FechamentoFolhaAnchor { get; private set; }

    public GestaoFolhasPageDriver(AngleSharpContext angleSharp)
    {
        _angleSharp = angleSharp;
    }

    public async Task GoTo()
    {
        Document = await _angleSharp.GetDocumentAsync("/Pontos/Folhas");

        AberturaFolhaAnchor = Document.GetAnchor("Abertura.Folha");

        AberturaFolhaAnchor.Should().NotBeNull("'a gestão de folhas deve ter um link para a abertura de uma folha'");
    }

    private void Identifica(Concepts.Folha folha)
    {
        var table = Document.GetTable("Folhas");

        var tableRow = table.GetTableRowByDataName(folha.Competencia.Value.ToString("yyyy-MM"));

        //

        FechamentoFolhaAnchor = tableRow.GetAnchor("Fechamento");

        FechamentoFolhaAnchor.Should().NotBeNull("a gestão de folhas deve ter um link para o fechamento da folha");
    }

    public async Task<Concepts.Folha> AbrirFolha(Concepts.Folha folha)
    {
        await GoTo();

        Document = await _angleSharp.GetDocumentAsync(AberturaFolhaAnchor.Href);

        var form = Document.GetForm();

        var perfil = folha.EQualificadaPelo();

        //var competencia = folha.Competencia.Value.ToString("yyyy-MM-dd\\THH:mm:ss");

        form.GetSelect("Folha.PerfilId").GetOption(perfil.Nome).IsSelected = true;
        form.GetInput("CompetenciaAno").Value = folha.Competencia.Value.Year.ToString();
        form.GetSelect("CompetenciaMes").Value = folha.Competencia.Value.Month.ToString();
        form.GetTextArea("Folha.Observacao").Value = folha.Observacao;

        var submitButton = form.GetSubmitButton("button.btn-primary");

        var resultPage = await _angleSharp.SendAsync(form, submitButton);

        resultPage.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        Document = await _angleSharp.GetDocumentAsync(resultPage);

        var folhaAberta = ObtemDetalhes();

        return folhaAberta;
    }

    public async Task<Concepts.Folha> FecharFolha(Concepts.Folha folhaAberta)
    {
        await GoTo();

        Identifica(folhaAberta);

        Document = await _angleSharp.GetDocumentAsync(FechamentoFolhaAnchor.Href);

        var form = Document.GetForm();

        var submitButton = form.GetSubmitButton();

        var resultPage = await _angleSharp.SendAsync(form, submitButton);

        resultPage.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        Document = await _angleSharp.GetDocumentAsync(resultPage);

        var folhaFechada = ObtemDetalhes();

        return folhaFechada;
    }

    private Folha ObtemDetalhes()
    {
        var hasErrors = Document.GetValidationErrors().Any();

        hasErrors.Should().BeFalse();

        var dl = Document.GetDefinitionList("Folha");

        var dlApuracaoMensal = Document.GetDefinitionList("ApuracaoMensal");

        var tempoTotalApurado = dlApuracaoMensal.GetDataListItem("TempoTotalApurado").TextContent.Trim();

        var folhaAberta = new Folha
        {
            Perfil = new PerfilRef
            {
                Nome = dl.GetDataListItem("Perfil").GetString(),
            },
            Competencia = DateTime.ParseExact(dl.GetDataListItem("Competencia").GetString().Substring(0, 7), "yyyy/MM", CultureInfo.InvariantCulture),
            StatusId = (StatusEnum)Enum.Parse(typeof(StatusEnum), dl.GetDataListItem("Status").GetString()),
            Observacao = dl.GetDataListItem("Observacao").GetString(),
            ApuracaoMensal = new ApuracaoMensal
            {
                Dias = new[] {
                    new ApuracaoDiaria {
                        Dia = 1,
                        TempoPrevisto = new TimeSpan()
                    }
                },
                TempoTotalApurado = string.IsNullOrEmpty(tempoTotalApurado) ? null : TimeSpan.Parse(tempoTotalApurado)
            }
        };

        return folhaAberta;
    }

    private IEnumerable<ApuracaoDiaria> ParseApuracaoDiaria(IHtmlElement dlApuracaoMensal)
    {
        var dlApuracaoDiariaList = dlApuracaoMensal.QuerySelectorAll("dl.ApuracaoDiaria");

        var apuracaoDiariaList = dlApuracaoDiariaList.Select(dl =>
        {
            return new ApuracaoDiaria
            {

            };
        });

        return apuracaoDiariaList;
    }
}

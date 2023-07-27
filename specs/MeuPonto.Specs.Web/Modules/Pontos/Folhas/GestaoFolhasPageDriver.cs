using AngleSharp.Html.Dom;
using MeuPonto.Helpers;
using MeuPonto.Modules.Perfis;
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

    public void GoTo()
    {
        Document = _angleSharp.GetDocument("/Pontos/Folhas");

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

    public Concepts.Folha AbrirFolha(Concepts.Folha folha)
    {
        GoTo();

        Document = _angleSharp.GetDocument(AberturaFolhaAnchor.Href);

        var form = Document.GetForm();

        var perfil = folha.EQualificadaPelo();

        //var competencia = folha.Competencia.Value.ToString("yyyy-MM-dd\\THH:mm:ss");

        form.GetSelect("Folha.PerfilId").GetOption(perfil.Nome).IsSelected = true;
        form.GetInput("CompetenciaAno").Value = folha.Competencia.Value.Year.ToString();
        form.GetSelect("CompetenciaMes").Value = folha.Competencia.Value.Month.ToString();
        form.GetTextArea("Folha.Observacao").Value = folha.Observacao;

        var confirmarCompetenciaButton = form.GetSubmitButton("button[value='ConfirmarCompetencia']");

        var competenciaConfirmadaPage = _angleSharp.Send(form, confirmarCompetenciaButton);

        Document = _angleSharp.GetDocument(competenciaConfirmadaPage);

        form = Document.GetForm();

        var submitButton = form.GetSubmitButton("button.btn-primary");

        var resultPage = _angleSharp.Send(form, submitButton);

        Document = _angleSharp.GetDocument(resultPage);

        var folhaAberta = ObtemDetalhes();

        return folhaAberta;
    }

    public Concepts.Folha FecharFolha(Concepts.Folha folhaAberta)
    {
        GoTo();

        Identifica(folhaAberta);

        Document = _angleSharp.GetDocument(FechamentoFolhaAnchor.Href);

        var form = Document.GetForm();

        var submitButton = form.GetSubmitButton();

        var resultPage = _angleSharp.Send(form, submitButton);

        Document = _angleSharp.GetDocument(resultPage);

        var folhaFechada = ObtemDetalhes();

        return folhaFechada;
    }

    private Folha ObtemDetalhes()
    {
        var hasErrors = Document.GetValidationErrors().Any();

        hasErrors.Should().BeFalse();

        var dl = Document.GetDefinitionList("Folha");

        var dlApuracaoDiariaCollection = Document.GetDefinitionListCollection("ApuracaoDiaria");

        //var tempoTotalApurado = dlApuracaoMensal.GetDataListItem("TempoTotalApurado").TextContent.Trim();

        var folhaAberta = new Folha
        {
            Perfil = new()
            {
                Nome = dl.GetDataListItem("Perfil").GetString(),
            },
            Competencia = DateTime.ParseExact(dl.GetDataListItem("Competencia").GetString().Substring(0, 7), "yyyy/MM", CultureInfo.InvariantCulture),
            StatusId = (StatusEnum)Enum.Parse(typeof(StatusEnum), dl.GetDataListItem("Status").GetString()),
            Observacao = dl.GetDataListItem("Observacao").GetString(),
            ApuracaoMensal = new ApuracaoMensal
            {
                Dias = dlApuracaoDiariaCollection.Select(element =>
                {
                    var dlApuracaoDiaria = (IHtmlElement)element;

                    var dia = int.Parse(dlApuracaoDiaria.GetAttribute("data-dia"));

                    var tempoPrevisto = dlApuracaoDiaria.GetDataListItem("TempoPrevisto").TextContent.Trim();

                    var tempoApurado = dlApuracaoDiaria.GetDataListItem("TempoApurado").TextContent.Trim();

                    var apuracaoDiaria = new ApuracaoDiaria
                    {
                        Dia = dia,
                        TempoPrevisto = string.IsNullOrEmpty(tempoPrevisto) ? null : TimeSpan.Parse(tempoPrevisto),
                        TempoApurado = string.IsNullOrEmpty(tempoApurado) ? null : TimeSpan.Parse(tempoApurado),
                    };

                    return apuracaoDiaria;
                }).ToArray()
            },
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

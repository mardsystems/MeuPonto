using AngleSharp.Html.Dom;
using MeuPonto.Helpers;
using MeuPonto.Modules.Pontos.Folhas;
using MeuPonto.Support;

namespace MeuPonto.Modules;

public class HomePageDriver : HomeInterface
{
    private readonly AngleSharpContext _angleSharp;

    public IHtmlDocument Document { get; private set; }

    public IHtmlAnchorElement PerfisAnchor { get; private set; }

    public IHtmlAnchorElement MarcacaoPontoAnchor { get; private set; }

    public IHtmlAnchorElement AberturaFolhaPontoAnchor { get; private set; }

    public IHtmlAnchorElement CriacaoPerfilAnchor { get; private set; }

    public HomePageDriver(AngleSharpContext angleSharp)
    {
        _angleSharp = angleSharp;
    }

    public void GoTo()
    {
        Document = _angleSharp.GetDocument("/");

        PerfisAnchor = Document.GetAnchor("Perfis");

        PerfisAnchor.Should().NotBeNull("'a tela inicial deve ter um link para o cadastro de perfis'");

        //

        MarcacaoPontoAnchor = Document.GetAnchor("Marcacao.Ponto");

        MarcacaoPontoAnchor.Should().NotBeNull("'a tela inicial deve ter um link para a marcação de ponto'");

        AberturaFolhaPontoAnchor = Document.GetAnchor("Abertura.Folha");

        AberturaFolhaPontoAnchor.Should().NotBeNull("'a tela inicial deve ter um link para a abertura de folha de ponto'");

        //

        CriacaoPerfilAnchor = Document.GetAnchor("Criacao.Perfil");

        //CriacaoPerfilAnchor.Should().NotBeNull("'a tela inicial deve ter um link para a criação de perfil'");
    }

    public Concepts.Folha ApurarFolha(Concepts.Folha folhaAberta)
    {
        GoTo();

        var form = Document.GetForm();

        var perfil = folhaAberta.EQualificadaPelo();

        form.GetSelect("PerfilId").GetOption(perfil.Nome).IsSelected = true;
        form.GetInput("CompetenciaAno").Value = folhaAberta.Competencia.Value.ToString("yyyy");
        form.GetSelect("CompetenciaMes").Value = folhaAberta.Competencia.Value.ToString("MM");

        var submitButton = form.GetSubmitButton();

        var resultPage = _angleSharp.Send(form, submitButton);

        resultPage.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        Document = _angleSharp.GetDocument(resultPage);

        var folhaApurada = IdentificaFolhaParaApuracao();

        return folhaApurada;
    }

    private Folha IdentificaFolhaParaApuracao()
    {
        var apuracaoMensalElement = (IHtmlElement)Document.QuerySelector(".apuracaoMensal");

        var tempoTotalApuradoElement = (IHtmlElement)Document.QuerySelector(".tempoTotalApurado");

        var folhaApurada = new Folha
        {
            //Perfil = new PontoPerfilRef
            //{
            //    Matricula = folhaElement.QuerySelector("dd.perfil").TextContent
            //},
            //Competencia = DateTime.Parse(folhaElement.QuerySelector("dd.competencia").TextContent),
            //Status = (PontoFolhaStatusEnum)Enum.Parse(typeof(PontoFolhaStatusEnum), folhaElement.QuerySelector("dd.status").TextContent),
            //Observacao = folhaElement.QuerySelector("dd.observacao").TextContent,
            ApuracaoMensal = new ApuracaoMensal
            {
                TempoTotalApurado = tempoTotalApuradoElement == null ? null : TimeSpan.Parse(tempoTotalApuradoElement.TextContent.Trim()),
            }
            //Dias = new[] {
            //    new PontoFolhaDia{
            //        Data = folhaAberta.Competencia,
            //        TempoTotalPrevisto = new TimeSpan()
            //    }
            //}
        };

        return folhaApurada;
    }
}

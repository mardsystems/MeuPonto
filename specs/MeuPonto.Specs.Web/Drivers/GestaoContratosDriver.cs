using AngleSharp.Html.Dom;
using MeuPonto.Helpers;
using MeuPonto.Support;
using Timesheet.Models.Contratos;

namespace MeuPonto.Drivers;

public class GestaoContratosDriver
{
    private readonly AngleSharpContext _angleSharp;

    public IHtmlDocument Document { get; private set; }

    public IHtmlAnchorElement CriacaoContratoAnchor { get; private set; }

    public IHtmlAnchorElement DetalheContratoAnchor { get; private set; }

    public IHtmlAnchorElement EdicaoContratoAnchor { get; private set; }

    public IHtmlAnchorElement ExclusaoContratoAnchor { get; private set; }

    public GestaoContratosDriver(AngleSharpContext angleSharp)
    {
        _angleSharp = angleSharp;
    }

    public void GoTo()
    {
        Document = _angleSharp.GetDocument("/Contratos");

        //

        CriacaoContratoAnchor = Document.GetAnchor("Criacao.Contrato");

        CriacaoContratoAnchor.Should().NotBeNull("a tela de contratos deve ter um link de criação de contrato");
    }

    private void Identifica(string nomeContrato)
    {
        var table = Document.GetTable("Contratos");

        var tableRow = table.GetTableRowByDataName(nomeContrato);

        //

        DetalheContratoAnchor = tableRow.GetAnchor("Detalhe");

        DetalheContratoAnchor.Should().NotBeNull("a lista de contratos deve ter um link de detalhe do contrato cadastrado");

        //

        EdicaoContratoAnchor = tableRow.GetAnchor("Edicao");

        EdicaoContratoAnchor.Should().NotBeNull("a lista de contratos deve ter um link de edição do contrato cadastrado");

        //

        ExclusaoContratoAnchor = tableRow.GetAnchor("Exclusao");

        ExclusaoContratoAnchor.Should().NotBeNull("a lista de contratos deve ter um link de exclusão do contrato cadastrado");
    }

    public void CriarContrato(Contrato contrato)
    {
        GoTo();

        Document = _angleSharp.GetDocument(CriacaoContratoAnchor.Href);

        var form = Document.GetForm();

        form.GetInput("AberturaContrato.Nome").Value = contrato.Nome;

        var daysOfWeek = Enum.GetValues<DayOfWeek>();

        foreach (var dayOfWeek in daysOfWeek)
        {
            var jornadaTrabalhoDiaria = contrato.JornadaTrabalhoSemanalPrevista.Semana.SingleOrDefault(x => x.DiaSemana == dayOfWeek);

            var i = (int)dayOfWeek;

            if (jornadaTrabalhoDiaria == default)
            {
                form.GetInput($"AberturaContrato.JornadaTrabalhoSemanalPrevista.Semana[{i}].Tempo").Value = "00:00";
            }
            else
            {
                form.GetInput($"AberturaContrato.JornadaTrabalhoSemanalPrevista.Semana[{i}].Tempo").Value = jornadaTrabalhoDiaria.Tempo.Value.ToString("hh\\:mm");
            }
        }

        var submitButton = form.GetSubmitButton();

        var resultPage = _angleSharp.Send(form, submitButton);

        Document = _angleSharp.GetDocument(resultPage);

        var hasErrors = Document.GetValidationErrors().Any();

        hasErrors.Should().BeFalse();

        //var contratoCadastrado = await ObtemDetalhes();

        //return contratoCadastrado;
    }

    public Contrato DetalharContrato(string nomeContrato)
    {
        GoTo();

        Identifica(nomeContrato);

        Document = _angleSharp.GetDocument(DetalheContratoAnchor.Href);

        var contratoDetalhado = ObtemDetalhes();

        return contratoDetalhado;
    }

    public void EditarContrato(string nomeContrato, Contrato contratoCadastrado)
    {
        GoTo();

        Identifica(nomeContrato);

        Document = _angleSharp.GetDocument(EdicaoContratoAnchor.Href);

        var form = Document.GetForm();

        form.GetInput("EdicaoContrato.Nome").Value = contratoCadastrado.Nome;

        var submitButton = form.GetSubmitButton();

        var resultPage = _angleSharp.Send(form, submitButton);

        Document = _angleSharp.GetDocument(resultPage);

        var hasErrors = Document.GetValidationErrors().Any();

        hasErrors.Should().BeFalse();

        //var contratoEditado = await ObtemDetalhes();

        //return contratoEditado;
    }

    private Contrato ObtemDetalhes()
    {
        var hasErrors = Document.GetValidationErrors().Any();

        hasErrors.Should().BeFalse();

        var dl = Document.GetDefinitionList("Contrato");

        var contratoCadastrado = new Contrato
        {
            Nome = dl.GetDataListItem("Nome").GetString(),
        };

        AdicionaJornadaTrabalhoDiaria(contratoCadastrado.JornadaTrabalhoSemanalPrevista, DayOfWeek.Sunday);
        AdicionaJornadaTrabalhoDiaria(contratoCadastrado.JornadaTrabalhoSemanalPrevista, DayOfWeek.Monday);
        AdicionaJornadaTrabalhoDiaria(contratoCadastrado.JornadaTrabalhoSemanalPrevista, DayOfWeek.Tuesday);
        AdicionaJornadaTrabalhoDiaria(contratoCadastrado.JornadaTrabalhoSemanalPrevista, DayOfWeek.Wednesday);
        AdicionaJornadaTrabalhoDiaria(contratoCadastrado.JornadaTrabalhoSemanalPrevista, DayOfWeek.Thursday);
        AdicionaJornadaTrabalhoDiaria(contratoCadastrado.JornadaTrabalhoSemanalPrevista, DayOfWeek.Friday);
        AdicionaJornadaTrabalhoDiaria(contratoCadastrado.JornadaTrabalhoSemanalPrevista, DayOfWeek.Saturday);

        return contratoCadastrado;
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

    public void ExcluirContrato(string nomeContrato)
    {
        GoTo();

        Identifica(nomeContrato);

        Document = _angleSharp.GetDocument(ExclusaoContratoAnchor.Href);

        var form = Document.GetForm();

        var submitButton = form.GetSubmitButton();

        var resultPage = _angleSharp.Send(form, submitButton);
    }
}

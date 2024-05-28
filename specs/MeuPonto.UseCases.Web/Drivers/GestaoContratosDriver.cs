using AngleSharp.Html.Dom;
using MeuPonto.Helpers;
using MeuPonto.Support;
using MeuPonto.Models.Contratos;

namespace MeuPonto.Drivers;

public class GestaoContratosDriver
{
    private readonly AngleSharpContext _angleSharp;

    public IHtmlDocument Document { get; private set; }

    public IHtmlAnchorElement AberturaContratoAnchor { get; private set; }

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

        AberturaContratoAnchor = Document.GetAnchor("Criacao.Contrato");

        AberturaContratoAnchor.Should().NotBeNull("a tela de contratos deve ter um link de criação de contrato");
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

    public Contrato SolicitarAbrerturaContrato()
    {
        GoTo();

        Document = _angleSharp.GetDocument(AberturaContratoAnchor.Href);

        var form = Document.GetForm();

        var aberturaContrato = new Contrato
        {
            Nome = form.GetInput("AberturaContrato.Nome").Value,
            Ativo = form.GetInput("AberturaContrato.Ativo").IsChecked,
        };

        var daysOfWeek = Enum.GetValues<DayOfWeek>();

        foreach (var dayOfWeek in daysOfWeek)
        {
            var i = (int)dayOfWeek;

            var input = form.GetInput($"AberturaContrato.JornadaTrabalhoSemanalPrevista.Semana[{i}].Tempo");

            if (input != null)
            {
                var tempo = input.Value;

                aberturaContrato.JornadaTrabalhoSemanalPrevista.Semana.Add(new JornadaTrabalhoDiaria
                {
                    DiaSemana = dayOfWeek,
                    Tempo = TimeSpan.Parse(tempo)
                });
            }
        }

        return aberturaContrato;
    }

    public void AbrirContrato(AberturaContratoData aberturaContrato, bool attemptOnly = false)
    {
        GoTo();

        Document = _angleSharp.GetDocument(AberturaContratoAnchor.Href);

        var form = Document.GetForm();

        form.GetInput("AberturaContrato.Nome").Value = aberturaContrato.Nome;

        form.GetInput("AberturaContrato.Ativo").IsChecked = aberturaContrato.Ativo;

        if (aberturaContrato.Empregador != null)
        {
            form.GetSelect("AberturaContrato.EmpregadorId").GetOption(aberturaContrato.Empregador).IsSelected = true;
        }

        if (aberturaContrato.Domingo.HasValue)
        {
            form.GetInput($"AberturaContrato.JornadaTrabalhoSemanalPrevista.Semana[0].Tempo").Value = aberturaContrato.Domingo.Value.ToString("hh\\:mm");
        }
        else
        {
            form.GetInput($"AberturaContrato.JornadaTrabalhoSemanalPrevista.Semana[0].Tempo").Value = "00:00";
        }

        if (aberturaContrato.Segunda.HasValue)
        {
            form.GetInput($"AberturaContrato.JornadaTrabalhoSemanalPrevista.Semana[1].Tempo").Value = aberturaContrato.Segunda.Value.ToString("hh\\:mm");
        }
        else
        {
            form.GetInput($"AberturaContrato.JornadaTrabalhoSemanalPrevista.Semana[1].Tempo").Value = "00:00";
        }

        if (aberturaContrato.Terca.HasValue)
        {
            form.GetInput($"AberturaContrato.JornadaTrabalhoSemanalPrevista.Semana[2].Tempo").Value = aberturaContrato.Terca.Value.ToString("hh\\:mm");
        }
        else
        {
            form.GetInput($"AberturaContrato.JornadaTrabalhoSemanalPrevista.Semana[2].Tempo").Value = "00:00";
        }

        if (aberturaContrato.Quarta.HasValue)
        {
            form.GetInput($"AberturaContrato.JornadaTrabalhoSemanalPrevista.Semana[3].Tempo").Value = aberturaContrato.Quarta.Value.ToString("hh\\:mm");
        }
        else
        {
            form.GetInput($"AberturaContrato.JornadaTrabalhoSemanalPrevista.Semana[3].Tempo").Value = "00:00";
        }

        if (aberturaContrato.Quinta.HasValue)
        {
            form.GetInput($"AberturaContrato.JornadaTrabalhoSemanalPrevista.Semana[4].Tempo").Value = aberturaContrato.Quinta.Value.ToString("hh\\:mm");
        }
        else
        {
            form.GetInput($"AberturaContrato.JornadaTrabalhoSemanalPrevista.Semana[4].Tempo").Value = "00:00";
        }

        if (aberturaContrato.Sexta.HasValue)
        {
            form.GetInput($"AberturaContrato.JornadaTrabalhoSemanalPrevista.Semana[5].Tempo").Value = aberturaContrato.Sexta.Value.ToString("hh\\:mm");
        }
        else
        {
            form.GetInput($"AberturaContrato.JornadaTrabalhoSemanalPrevista.Semana[5].Tempo").Value = "00:00";
        }

        if (aberturaContrato.Sabado.HasValue)
        {
            form.GetInput($"AberturaContrato.JornadaTrabalhoSemanalPrevista.Semana[6].Tempo").Value = aberturaContrato.Sabado.Value.ToString("hh\\:mm");
        }
        else
        {
            form.GetInput($"AberturaContrato.JornadaTrabalhoSemanalPrevista.Semana[6].Tempo").Value = "00:00";
        }

        //var daysOfWeek = Enum.GetValues<DayOfWeek>();

        //foreach (var dayOfWeek in daysOfWeek)
        //{
        //    var jornadaTrabalhoDiaria = aberturaContrato.JornadaTrabalhoSemanalPrevista.Semana.SingleOrDefault(x => x.DiaSemana == dayOfWeek);

        //    var i = (int)dayOfWeek;

        //    if (jornadaTrabalhoDiaria == default)
        //    {
        //        form.GetInput($"AberturaContrato.JornadaTrabalhoSemanalPrevista.Semana[{i}].Tempo").Value = "00:00";
        //    }
        //    else
        //    {
        //        form.GetInput($"AberturaContrato.JornadaTrabalhoSemanalPrevista.Semana[{i}].Tempo").Value = jornadaTrabalhoDiaria.Tempo.Value.ToString("hh\\:mm");
        //    }
        //}

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
            //var contratoCadastrado = await ObtemDetalhes();

            //return contratoCadastrado;
        }
    }

    public Contrato DetalharContrato(string nomeContrato)
    {
        GoTo();

        Identifica(nomeContrato);

        Document = _angleSharp.GetDocument(DetalheContratoAnchor.Href);

        var detalheContrato = ObtemDetalhes();

        return detalheContrato;
    }

    public Contrato SolicitarEdicaoContrato(string nomeContrato)
    {
        GoTo();

        Identifica(nomeContrato);

        Document = _angleSharp.GetDocument(EdicaoContratoAnchor.Href);

        var form = Document.GetForm();

        var edicaoContrato = new Contrato
        {
            Nome = form.GetInput("EdicaoContrato.Nome").Value,
            Ativo = form.GetInput("EdicaoContrato.Ativo").IsChecked,
        };

        var daysOfWeek = Enum.GetValues<DayOfWeek>();

        foreach (var dayOfWeek in daysOfWeek)
        {
            var i = (int)dayOfWeek;

            var input = form.GetInput($"EdicaoContrato.JornadaTrabalhoSemanalPrevista.Semana[{i}].Tempo");

            if (input != null)
            {
                var tempo = input.Value;

                edicaoContrato.JornadaTrabalhoSemanalPrevista.Semana.Add(new JornadaTrabalhoDiaria
                {
                    DiaSemana = dayOfWeek,
                    Tempo = TimeSpan.Parse(tempo)
                });
            }
        }

        return edicaoContrato;
    }

    public void EditarContrato(string nomeContrato, AberturaContratoData edicaoContrato)
    {
        GoTo();

        Identifica(nomeContrato);

        Document = _angleSharp.GetDocument(EdicaoContratoAnchor.Href);

        var form = Document.GetForm();

        form.GetInput("EdicaoContrato.Nome").Value = edicaoContrato.Nome;

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
            //var contratoEditado = await ObtemDetalhes();

            //return contratoEditado;
        }
    }

    private Contrato ObtemDetalhes()
    {
        var hasErrors = Document.GetValidationErrors().Any();

        hasErrors.Should().BeFalse();

        var dl = Document.GetDefinitionList("Contrato");

        var detalheContrato = new Contrato
        {
            Nome = dl.GetDataListItem("Nome").GetString(),
        };

        AdicionaJornadaTrabalhoDiaria(detalheContrato.JornadaTrabalhoSemanalPrevista, DayOfWeek.Sunday);
        AdicionaJornadaTrabalhoDiaria(detalheContrato.JornadaTrabalhoSemanalPrevista, DayOfWeek.Monday);
        AdicionaJornadaTrabalhoDiaria(detalheContrato.JornadaTrabalhoSemanalPrevista, DayOfWeek.Tuesday);
        AdicionaJornadaTrabalhoDiaria(detalheContrato.JornadaTrabalhoSemanalPrevista, DayOfWeek.Wednesday);
        AdicionaJornadaTrabalhoDiaria(detalheContrato.JornadaTrabalhoSemanalPrevista, DayOfWeek.Thursday);
        AdicionaJornadaTrabalhoDiaria(detalheContrato.JornadaTrabalhoSemanalPrevista, DayOfWeek.Friday);
        AdicionaJornadaTrabalhoDiaria(detalheContrato.JornadaTrabalhoSemanalPrevista, DayOfWeek.Saturday);

        return detalheContrato;
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

    public Contrato SolicitarExclusaoContrato(string nomeContrato)
    {
        GoTo();

        Identifica(nomeContrato);

        Document = _angleSharp.GetDocument(ExclusaoContratoAnchor.Href);

        var detalheContrato = ObtemDetalhes();

        return detalheContrato;

        //var form = Document.GetForm();

        //var exclusaoContrato = new Contrato
        //{
        //    Nome = form.GetInput("ExclusaoContrato.Nome").Value,
        //    Ativo = form.GetInput("ExclusaoContrato.Ativo").IsChecked,
        //};

        //var daysOfWeek = Enum.GetValues<DayOfWeek>();

        //foreach (var dayOfWeek in daysOfWeek)
        //{
        //    var i = (int)dayOfWeek;

        //    var input = form.GetInput($"ExclusaoContrato.JornadaTrabalhoSemanalPrevista.Semana[{i}].Tempo");

        //    if (input != null)
        //    {
        //        var tempo = input.Value;

        //        exclusaoContrato.JornadaTrabalhoSemanalPrevista.Semana.Add(new JornadaTrabalhoDiaria
        //        {
        //            DiaSemana = dayOfWeek,
        //            Tempo = TimeSpan.Parse(tempo)
        //        });
        //    }
        //}

        //return exclusaoContrato;
    }

    public void ExcluirContrato(string nomeContrato)
    {
        GoTo();

        Identifica(nomeContrato);

        Document = _angleSharp.GetDocument(ExclusaoContratoAnchor.Href);

        var form = Document.GetForm();

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
            //var contratoEditado = await ObtemDetalhes();

            //return contratoEditado;
        }
    }
}

public class AberturaContratoData
{
    public string Nome { get; set; }
    public bool Ativo { get; set; }
    public string Empregador { get; set; }
    public TimeSpan? Domingo { get; set; }
    public TimeSpan? Segunda { get; set; }
    public TimeSpan? Terca { get; set; }
    public TimeSpan? Quarta { get; set; }
    public TimeSpan? Quinta { get; set; }
    public TimeSpan? Sexta { get; set; }
    public TimeSpan? Sabado { get; set; }
}

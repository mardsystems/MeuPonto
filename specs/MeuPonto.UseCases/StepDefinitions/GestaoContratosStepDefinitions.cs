using MeuPonto.Data;
using MeuPonto.Drivers;
using MeuPonto.Support;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using TechTalk.SpecFlow.Assist;
using MeuPonto.Features.GestaoContratos;
using MeuPonto.Models.Contratos;

namespace MeuPonto.StepDefinitions;

[Binding]
public class GestaoContratosStepDefinitions
{
    private readonly ScenarioContext _scenario;
    private readonly TransactionContext _transaction;
    private readonly GestaoContratosContext _gestaoContratos;
    private readonly GestaoContratosDriver _gestaoContratosInterface;
    private readonly MeuPontoDbContext _db;

    public GestaoContratosStepDefinitions(
        ScenarioContext scenario,
        TransactionContext transaction,
        GestaoContratosContext gestaoContratos,
        GestaoContratosDriver gestaoContratosInterface,
        MeuPontoDbContext db)
    {
        _scenario = scenario;
        _transaction = transaction;
        _gestaoContratos = gestaoContratos;
        _gestaoContratosInterface = gestaoContratosInterface;
        _db = db;
    }

    [Given(@"que existe um contrato aberto")]
    public void GivenQueExisteUmContratoAberto()
    {
        GivenQueExisteUmContratoAberto("Contrato Aberto");
    }

    [Given(@"que existe um contrato aberto '([^']*)'")]
    public void GivenQueExisteUmContratoAberto(string nome)
    {
        var contrato = _db.Contratos
            .Include(x => x.Empregador)
            .FirstOrDefault(x => x.Nome == nome);

        if (contrato == null)
        {
            contrato = _transaction.InciarAberturaContrato();

            contrato.Nome = nome;
            contrato.JornadaTrabalhoSemanalPrevista = new JornadaTrabalhoSemanal
            {
                Semana = new List<JornadaTrabalhoDiaria>(new[]{
                        new JornadaTrabalhoDiaria
                        {
                            DiaSemana = DayOfWeek.Monday,
                            Tempo = new TimeSpan(8,0,0)
                        },
                        new JornadaTrabalhoDiaria
                        {
                            DiaSemana = DayOfWeek.Tuesday,
                            Tempo = new TimeSpan(8,0,0)
                        },
                        new JornadaTrabalhoDiaria
                        {
                            DiaSemana = DayOfWeek.Wednesday,
                            Tempo = new TimeSpan(8,0,0)
                        },
                        new JornadaTrabalhoDiaria
                        {
                            DiaSemana = DayOfWeek.Thursday,
                            Tempo = new TimeSpan(8,0,0)
                        },
                        new JornadaTrabalhoDiaria
                        {
                            DiaSemana = DayOfWeek.Friday,
                            Tempo = new TimeSpan(8,0,0)
                        },
                        new JornadaTrabalhoDiaria
                        {
                            DiaSemana = DayOfWeek.Saturday,
                            Tempo = new TimeSpan(0,0,0)
                        },
                        new JornadaTrabalhoDiaria
                        {
                            DiaSemana = DayOfWeek.Sunday,
                            Tempo = new TimeSpan(0,0,0)
                        }
                    })
            };

            _db.Contratos.Add(contrato);
            _db.SaveChanges();
        }
    }

    [Given(@"que existe um contrato aberto com a seguinte jornada de trabalho semanal prevista:")]
    public void GivenQueExisteUmContratoAbertoComASeguinteJornadaDeTrabalhoSemanalPrevista(Table table)
    {
        DefineJornadaTrabalhoSemanalPrevistaDe(table);

        _db.Contratos.Add(_gestaoContratos.Contrato);
        _db.SaveChanges();
    }

    public void DefineJornadaTrabalhoSemanalPrevistaDe(Table table)
    {
        var semana = table.CreateSet<(DayOfWeek diaSemana, TimeSpan tempo)>();

        var daysOfWeek = Enum.GetValues<DayOfWeek>();

        foreach (var dayOfWeek in daysOfWeek)
        {
            var jornadaTrabalhoDiaria = semana.SingleOrDefault(x => x.diaSemana == dayOfWeek);

            var i = (int)dayOfWeek;

            if (jornadaTrabalhoDiaria == default)
            {
                _gestaoContratos.Contrato.JornadaTrabalhoSemanalPrevista.Semana[i].Tempo = new TimeSpan(0, 0, 0);
            }
            else
            {
                _gestaoContratos.Contrato.JornadaTrabalhoSemanalPrevista.Semana[i].Tempo = jornadaTrabalhoDiaria.tempo;
            }
        }

        //_db.Contratos.Add(contrato);
        //await _db.SaveChangesAsync();

        ////

        //var document = await _angleSharp.GetDocumentAsync("/");

        ////

        //var contratosAnchor = (IHtmlAnchorElement)document.QuerySelector("a.contratos");

        //contratosAnchor.Should().NotBeNull("a tela inicial deve ter um link para os contratos");

        //_scenario["contratosAnchor"] = contratosAnchor;

        ////

        //var marcacaoPontoAnchor = (IHtmlAnchorElement)document.QuerySelector("a.marcacao.ponto");

        //marcacaoPontoAnchor.Should().NotBeNull("a tela inicial deve ter um link de marcação de ponto");

        //_scenario["marcacaoPontoAnchor"] = marcacaoPontoAnchor;

        ////

        //var aberturaFolhaPontoAnchor = (IHtmlAnchorElement)document.QuerySelector("a.abertura.folha.ponto");

        //aberturaFolhaPontoAnchor.Should().NotBeNull("a tela inicial deve ter um link de abertura de folha de ponto");

        //_scenario["aberturaFolhaPontoAnchor"] = aberturaFolhaPontoAnchor;
    }

    [Given(@"que o nome do trabalhador é '([^']*)'")]
    public void GivenQueONomeDoTrabalhadorE(string nome)
    {
        _gestaoContratos.Contrato.Nome = nome;
    }

    [Given(@"que o trabalhador é o '([^']*)'")]
    public void GivenQueOTrabalhadorEO(string nome)
    {
        _gestaoContratos.Contrato.Nome = nome;
    }

    [Given(@"que a jornada de trabalho semanal é de '([^']*)' a '([^']*)' das '([^']*)' às '([^']*)' com '([^']*)' de almoço")]
    public void GivenQueAJornadaDeTrabalhoSemanalEDeADasAsComDeAlmoco(DayOfWeek dayOfWeekInicio, DayOfWeek dayOfWeekTermino, TimeSpan horaInicio, TimeSpan horaTermino, TimeSpan tempoAlmoco)
    {
        var tempo = horaTermino - horaInicio - tempoAlmoco;

        var daysOfWeek = Enum.GetValues<DayOfWeek>();

        for (int i = (int)dayOfWeekInicio - 1; i < (int)dayOfWeekTermino; i++)
        {
            var dayOfWeek = daysOfWeek[i];

            _gestaoContratos.Contrato.JornadaTrabalhoSemanalPrevista.Semana[i].Tempo = tempo;
        }
    }

    [Given(@"que a jornada de trabalho de '([^']*)' é das '([^']*)' às '([^']*)'")]
    public void GivenQueAJornadaDeTrabalhoDeEDasAs(DayOfWeek dayOfWeek, TimeSpan horaInicio, TimeSpan horaTermino)
    {
        var tempo = horaTermino - horaInicio;

        _gestaoContratos.Contrato.JornadaTrabalhoSemanalPrevista.Semana[(int)dayOfWeek].Tempo = tempo;
    }

    [Given(@"que não tem jornada de trabalho no '([^']*)'")]
    public void GivenQueNaoTemJornadaDeTrabalhoNo(DayOfWeek dayOfWeek)
    {
        _gestaoContratos.Contrato.JornadaTrabalhoSemanalPrevista.Semana[(int)dayOfWeek].Tempo = TimeSpan.Zero;
    }

    [Given(@"que não tem jornada de trabalho no '([^']*)' e no '([^']*)'")]
    public void GivenQueNaoTemJornadaDeTrabalhoNoENo(DayOfWeek dayOfWeek1, DayOfWeek dayOfWeek2)
    {
        _gestaoContratos.Contrato.JornadaTrabalhoSemanalPrevista.Semana[(int)dayOfWeek1].Tempo = TimeSpan.Zero;

        _gestaoContratos.Contrato.JornadaTrabalhoSemanalPrevista.Semana[(int)dayOfWeek2].Tempo = TimeSpan.Zero;
    }

    [Given(@"que existe uma abertura de contrato em andamento")]
    public void GivenQueExisteUmaAberturaDeContratoEmAndamento()
    {
        GivenQueExisteUmaAberturaDeContratoEmAndamento("Contrato Novo");
    }

    [Given(@"que existe uma abertura de contrato em andamento '([^']*)'")]
    public void GivenQueExisteUmaAberturaDeContratoEmAndamento(string nome)
    {
        var contrato = _gestaoContratosInterface.SolicitarAbrerturaContrato();

        contrato.Nome = nome;

        _gestaoContratos.Contextualizar(contrato);
    }

    [Given(@"que existe uma edição do contrato '([^']*)' em andamento")]
    public void GivenQueExisteUmaEdicaoDoContratoEmAndamento(string nomeContrato)
    {
        var contrato = _gestaoContratosInterface.SolicitarEdicaoContrato(nomeContrato);

        _gestaoContratos.Contextualizar(contrato);
    }

    [When(@"o trabalhador solicitar a abertura de um contrato")]
    public void WhenOTrabalhadorSolicitarAAberturaDeUmContrato()
    {
        var contrato = _gestaoContratosInterface.SolicitarAbrerturaContrato();

        _gestaoContratos.Contextualizar(contrato);
    }

    [When(@"o trabalhador abrir o contrato como:")]
    public void WhenOTrabalhadorAbrirOContratoComo(Table table)
    {
        _gestaoContratos.Especificar(table);

        var contrato = _gestaoContratos.Contrato;

        var data = table.CreateInstance(() => new AberturaContratoData
        {
            Nome = contrato.Nome ?? "Contrato Padrão",
            Ativo = contrato.Ativo,
            Domingo = contrato.JornadaTrabalhoSemanalPrevista.Semana[0].Tempo,
            Segunda = contrato.JornadaTrabalhoSemanalPrevista.Semana[1].Tempo,
            Terca = contrato.JornadaTrabalhoSemanalPrevista.Semana[2].Tempo,
            Quarta = contrato.JornadaTrabalhoSemanalPrevista.Semana[3].Tempo,
            Quinta = contrato.JornadaTrabalhoSemanalPrevista.Semana[4].Tempo,
            Sexta = contrato.JornadaTrabalhoSemanalPrevista.Semana[5].Tempo,
            Sabado = contrato.JornadaTrabalhoSemanalPrevista.Semana[6].Tempo,
        });

        contrato.Nome = data.Nome;
        contrato.Ativo = data.Ativo;

        contrato.JornadaTrabalhoSemanalPrevista.Semana[0].Tempo = data.Domingo;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[1].Tempo = data.Segunda;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[2].Tempo = data.Terca;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[3].Tempo = data.Quarta;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[4].Tempo = data.Quinta;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[5].Tempo = data.Sexta;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[6].Tempo = data.Sabado;

        _gestaoContratosInterface.AbrirContrato(contrato);

        //_db.ChangeTracker.Clear();

        var contratoAberto = _db.Contratos
            .FirstOrDefault(x => x.Nome == contrato.Nome);

        _gestaoContratos.Contextualizar(contratoAberto);
    }

    [When(@"o trabalhador tentar abrir o contrato como")]
    public void WhenOTrabalhadorTentarAbrirOContratoComo(Table table)
    {
        _gestaoContratos.Especificar(table);

        var contrato = _gestaoContratos.Contrato;

        var data = table.CreateInstance(() => new AberturaContratoData
        {
            Nome = contrato.Nome ?? "Contrato Padrão",
            Ativo = contrato.Ativo,
            Empregador = contrato.Empregador?.Nome,
            Domingo = contrato.JornadaTrabalhoSemanalPrevista.Semana[0].Tempo,
            Segunda = contrato.JornadaTrabalhoSemanalPrevista.Semana[1].Tempo,
            Terca = contrato.JornadaTrabalhoSemanalPrevista.Semana[2].Tempo,
            Quarta = contrato.JornadaTrabalhoSemanalPrevista.Semana[3].Tempo,
            Quinta = contrato.JornadaTrabalhoSemanalPrevista.Semana[4].Tempo,
            Sexta = contrato.JornadaTrabalhoSemanalPrevista.Semana[5].Tempo,
            Sabado = contrato.JornadaTrabalhoSemanalPrevista.Semana[6].Tempo,
        });

        contrato.Nome = data.Nome;
        contrato.Ativo = data.Ativo;

        var empregador = _db.Empregadores.FirstOrDefault(x => x.Nome == data.Empregador);

        contrato.FeitoCom(empregador);

        contrato.JornadaTrabalhoSemanalPrevista.Semana[0].Tempo = data.Domingo;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[1].Tempo = data.Segunda;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[2].Tempo = data.Terca;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[3].Tempo = data.Quarta;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[4].Tempo = data.Quinta;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[5].Tempo = data.Sexta;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[6].Tempo = data.Sabado;

        try
        {
            _gestaoContratosInterface.AbrirContrato(contrato);
        }
        catch (Exception ex)
        {
            _gestaoContratos.Erro = ex.Message;
        }
    }

    [When(@"o trabalhador alterar o contrato para")]
    public void WhenOTrabalhadorAlterarOContratoPara(Table table)
    {
        _gestaoContratos.Especificar(table);

        var contrato = _gestaoContratos.Contrato;

        var data = table.CreateInstance(() => new AberturaContratoData
        {
            Nome = contrato.Nome ?? "Contrato Padrão",
            Ativo = contrato.Ativo,
            Empregador = contrato.Empregador?.Nome,
            Domingo = contrato.JornadaTrabalhoSemanalPrevista.Semana[0].Tempo,
            Segunda = contrato.JornadaTrabalhoSemanalPrevista.Semana[1].Tempo,
            Terca = contrato.JornadaTrabalhoSemanalPrevista.Semana[2].Tempo,
            Quarta = contrato.JornadaTrabalhoSemanalPrevista.Semana[3].Tempo,
            Quinta = contrato.JornadaTrabalhoSemanalPrevista.Semana[4].Tempo,
            Sexta = contrato.JornadaTrabalhoSemanalPrevista.Semana[5].Tempo,
            Sabado = contrato.JornadaTrabalhoSemanalPrevista.Semana[6].Tempo,
        });

        contrato.Nome = data.Nome;
        contrato.Ativo = data.Ativo;

        var empregador = _db.Empregadores.FirstOrDefault(x => x.Nome == data.Empregador);

        contrato.FeitoCom(empregador);

        contrato.JornadaTrabalhoSemanalPrevista.Semana[0].Tempo = data.Domingo;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[1].Tempo = data.Segunda;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[2].Tempo = data.Terca;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[3].Tempo = data.Quarta;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[4].Tempo = data.Quinta;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[5].Tempo = data.Sexta;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[6].Tempo = data.Sabado;

        _gestaoContratosInterface.EditarContrato(_gestaoContratos.NomeContrato, contrato);

        _db.ChangeTracker.Clear();

        var contratoAberto = _db.Contratos
            .Include(x => x.Empregador)
            .FirstOrDefault(x => x.Nome == contrato.Nome);

        _gestaoContratos.Contextualizar(contratoAberto);
    }

    [When(@"o trabalhador tentar alterar o contrato para")]
    public void WhenOTrabalhadorTentarAlterarOContratoPara(Table table)
    {
        _gestaoContratos.Especificar(table);

        var contrato = _gestaoContratos.Contrato;

        var data = table.CreateInstance(() => new AberturaContratoData
        {
            Nome = contrato.Nome,
            Ativo = contrato.Ativo,
            Empregador = contrato.Empregador?.Nome,
            Domingo = contrato.JornadaTrabalhoSemanalPrevista.Semana[0].Tempo,
            Segunda = contrato.JornadaTrabalhoSemanalPrevista.Semana[1].Tempo,
            Terca = contrato.JornadaTrabalhoSemanalPrevista.Semana[2].Tempo,
            Quarta = contrato.JornadaTrabalhoSemanalPrevista.Semana[3].Tempo,
            Quinta = contrato.JornadaTrabalhoSemanalPrevista.Semana[4].Tempo,
            Sexta = contrato.JornadaTrabalhoSemanalPrevista.Semana[5].Tempo,
            Sabado = contrato.JornadaTrabalhoSemanalPrevista.Semana[6].Tempo,
        });

        contrato.Nome = data.Nome;
        contrato.Ativo = data.Ativo;

        var empregador = _db.Empregadores.FirstOrDefault(x => x.Nome == data.Empregador);

        contrato.FeitoCom(empregador);

        contrato.JornadaTrabalhoSemanalPrevista.Semana[0].Tempo = data.Domingo;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[1].Tempo = data.Segunda;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[2].Tempo = data.Terca;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[3].Tempo = data.Quarta;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[4].Tempo = data.Quinta;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[5].Tempo = data.Sexta;
        contrato.JornadaTrabalhoSemanalPrevista.Semana[6].Tempo = data.Sabado;

        try
        {
            _gestaoContratosInterface.EditarContrato(_gestaoContratos.NomeContrato, contrato);
        }
        catch (Exception ex)
        {
            _gestaoContratos.Erro = ex.Message;
        }
    }

    [When(@"o trabalhador solicitar a edição do contrato '([^']*)'")]
    public void WhenOTrabalhadorSolicitarAEdicaoDoContrato(string nomeContrato)
    {
        var contrato = _gestaoContratosInterface.SolicitarEdicaoContrato(nomeContrato);

        _gestaoContratos.Contextualizar(contrato);
    }

    [Then(@"o contrato deverá ser ativo")]
    public void ThenOContratoDeveraSerAtivo()
    {
        _gestaoContratos.Contrato.Ativo.Should().BeTrue();
    }

    [Then(@"a jornada de trabalho semanal prevista no contrato deverá ser:")]
    [Then(@"o contrato deverá prever a seguinte jornada de trabalho semanal:")]
    public void ThenAJornadaDeTrabalhoSemanalPrevistaNoContratoDeveraSer(Table jornadaTrabalhoSemanal)
    {
        var jornadaTrabalhoSemanalPrevista = _gestaoContratos.Contrato.JornadaTrabalhoSemanalPrevista;

        jornadaTrabalhoSemanal.CompareToSet(jornadaTrabalhoSemanalPrevista.Semana);
    }

    #region Abrir Contrato

    [When(@"o trabalhador abrir o contrato")]
    public void WhenOTrabalhadorAbrirOContrato()
    {
        //_gestaoContratosInterface.GoTo();

        _gestaoContratosInterface.AbrirContrato(_gestaoContratos.Contrato);

        var contratoAberto = _db.Contratos.FirstOrDefault(x => x.Nome == _gestaoContratos.Contrato.Nome);

        _gestaoContratos.Contextualizar(contratoAberto);
    }

    [Then(@"um contrato deverá ser cadastrado")]
    public void ThenUmContratoDeveraSerCadastrado()
    {
        _gestaoContratos.Contrato.Should().NotBeNull();
    }

    #endregion

    #region Detalhar Contrato

    [When(@"o trabalhador detalhar o contrato")]
    public void WhenOTrabalhadorDetalharOContrato()
    {
        var contratoDetalhado = _gestaoContratosInterface.DetalharContrato(_gestaoContratos.NomeContrato);

        _gestaoContratos.Contextualizar(contratoDetalhado);
    }

    [Then(@"o contrato deverá ser detalhado")]
    public void ThenOContratoDeveraSerDetalhado()
    {
        _gestaoContratos.Contrato.Should().NotBeNull();
    }

    #endregion

    #region Editar Contrato

    [When(@"o trabalhador editar o contrato")]
    public void WhenOTrabalhadorEditarOContrato()
    {
        _gestaoContratosInterface.EditarContrato(_gestaoContratos.NomeContrato, _gestaoContratos.Contrato);

        var contratoEdidado = _db.Contratos.FirstOrDefault(x => x.Nome == _gestaoContratos.Contrato.Nome);

        _gestaoContratos.Contextualizar(contratoEdidado);
    }

    [Then(@"o contrato deverá ser editado")]
    public void ThenOContratoDeveraSerEditado()
    {
        _gestaoContratos.Contrato.Should().NotBeNull();
    }

    #endregion

    [When(@"o trabalhador solicitar o encerramento do contrato '([^']*)'")]
    public void WhenOTrabalhadorSolicitarOEncerramentoDoContrato(string nomeContrato)
    {
        throw new PendingStepException();

        _gestaoContratos.Contextualizar(null);
    }

    [When(@"o trabalhador encerrar o contrato '([^']*)'")]
    public void WhenOTrabalhadorEncerrarOContrato(string nomeContrato)
    {

    }

    [Then(@"o contrato deverá ser encerrado")]
    public void ThenOContratoDeveraSerEncerrado()
    {
        throw new PendingStepException();
    }

    #region Excluir Contrato

    [When(@"o trabalhador solicitar a exclusão do contrato '([^']*)'")]
    public void WhenOTrabalhadorSolicitarAExclusaoDoContrato(string nomeContrato)
    {
        throw new PendingStepException();

        _gestaoContratos.Contextualizar(null);
    }

    [When(@"o trabalhador excluir o contrato '([^']*)'")]
    public void WhenOTrabalhadorExcluirOContrato(string nomeContrato)
    {
        _gestaoContratosInterface.ExcluirContrato(nomeContrato);
    }

    [Then(@"o contrato deverá ser excluído")]
    public void ThenOContratoDeveraSerExcluido()
    {
        var contrato = _db.Contratos.FirstOrDefault(x => x.Nome == _gestaoContratos.Contrato.Nome);

        contrato.Should().BeNull();
    }

    #endregion

    [Then(@"o sistema deverá apresentar um contrato novo")]
    public void ThenOSistemaDeveraApresentarUmContratoNovo()
    {
        _gestaoContratos.Contrato.Should().NotBeNull();
    }

    [Then(@"o sistema deverá registrar o contrato como esperado")]
    public void ThenOSistemaDeveraRegistrarOContratoComoEsperado()
    {
        _gestaoContratos.Especificacao.CompareToSet(_db.Contratos);
    }

    [Then(@"o sistema deverá alterar o contrato como esperado")]
    public void ThenOSistemaDeveraAlterarOContratoComoEsperado()
    {
        _gestaoContratos.Especificacao.CompareToSet(_db.Contratos);
    }

    [Then(@"o nome do contrato deverá ser '([^']*)'")]
    public void ThenONomeDoContratoDeveraSer(string nome)
    {
        _gestaoContratos.Contrato.Nome.Should().Be(nome);
    }

    [Then(@"o tempo total da jornada de trabalho semanal prevista no contrato deverá ser '([^']*)'")]
    public void ThenOTempoTotalDaJornadaDeTrabalhoSemanalPrevistaNoContratoDeveraSer(TimeSpan tempoTotal)
    {
        var jornadaTrabalhoSemanalPrevista = _gestaoContratos.Contrato.JornadaTrabalhoSemanalPrevista;

        jornadaTrabalhoSemanalPrevista.TempoTotal.Should().Be(tempoTotal);
    }

    [Then(@"a tentativa de abrir o contrato deverá falhar com um erro ""([^""]*)""")]
    public void ThenATentativaDeAbrirOContratoDeveraFalharComUmErro(string erro)
    {
        _gestaoContratos.Erro.Should().Be(erro);
    }

    [Then(@"a tentativa de alterar o contrato deverá falhar com um erro ""([^""]*)""")]
    public void ThenATentativaDeAlterarOContratoDeveraFalharComUmErro(string erro)
    {
        _gestaoContratos.Erro.Should().Be(erro);
    }

    [Then(@"o contrato não deverá ser excluído")]
    public void ThenOContratoNaoDeveraSerExcluido()
    {
        var contrato = _db.Contratos.FirstOrDefault(x => x.Nome == _gestaoContratos.Contrato.Nome);

        contrato.Should().NotBeNull();
    }
}

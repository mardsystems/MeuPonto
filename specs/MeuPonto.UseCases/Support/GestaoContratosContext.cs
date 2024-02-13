using Timesheet.Models.Contratos;

namespace MeuPonto.Support;

public class GestaoContratosContext
{
    public GestaoContratosContext()
    {
        //var contrato = new Contrato
        //{
        //    Nome = "Test user",
        //};

        //Contrato = contrato;
    }

    public void Inicia(Contrato contrato)
    {
        Contrato = contrato;

        if (contrato == null)
        {
            throw new ArgumentNullException(nameof(contrato));
        }

        NomeContrato = contrato.Nome;
    }

    public void ConsideraQueExiste(Contrato contrato)
    {
        Contrato = contrato;

        NomeContrato = contrato.Nome;
    }

    public Contrato Contrato { get; private set; }

    public void DefineNomeContrato(string nomeContrato)
    {
        Contrato.Nome = nomeContrato;

        NomeContrato = nomeContrato;
    }

    public string NomeContrato { get; private set; }

    public void Define(Contrato contratoCadastrado)
    {
        ContratoCadastrado = contratoCadastrado;

        NomeContrato = contratoCadastrado.Nome;
    }

    public Contrato ContratoCadastrado { get; private set; }
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

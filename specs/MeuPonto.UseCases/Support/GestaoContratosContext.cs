using MeuPonto.Models.Contratos;

namespace MeuPonto.Support;

public class GestaoContratosContext
{
    public Table Especificacao { get; set; }

    public Contrato Contrato { get; private set; }

    public string NomeContrato { get; private set; }

    public string Erro { get; set; }

    public GestaoContratosContext()
    {
        //var contrato = new Contrato
        //{
        //    Nome = "Test user",
        //};

        //Contrato = contrato;
    }

    public void Iniciar(Contrato contrato)
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

    public void DefineNomeContrato(string nomeContrato)
    {
        Contrato.Nome = nomeContrato;

        NomeContrato = nomeContrato;
    }

    public void Define(Contrato contratoCadastrado)
    {
        Contrato = contratoCadastrado;

        NomeContrato = contratoCadastrado.Nome;
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

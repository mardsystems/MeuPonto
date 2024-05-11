using Timesheet.Models.Contratos;

namespace MeuPonto.Support;

public class CadastroEmpregadoresContext
{
    public Empregador Empregador { get; private set; }

    public string NomeEmpregador { get; private set; }

    public CadastroEmpregadoresContext()
    {
        //var empregador = new Empregador
        //{
        //    Nome = "Test user",
        //};

        //Empregador = empregador;
    }

    public void Iniciar(Empregador empregador)
    {
        Empregador = empregador;

        if (empregador == null)
        {
            throw new ArgumentNullException(nameof(empregador));
        }

        NomeEmpregador = empregador.Nome;
    }

    public void ConsideraQueExiste(Empregador empregador)
    {
        Empregador = empregador;

        NomeEmpregador = empregador.Nome;
    }

    public void DefineNomeEmpregador(string nomeEmpregador)
    {
        Empregador.Nome = nomeEmpregador;

        NomeEmpregador = nomeEmpregador;
    }

    public void Define(Empregador empregadorCadastrado)
    {
        Empregador = empregadorCadastrado;

        NomeEmpregador = empregadorCadastrado.Nome;
    }
}

public class CadastroEmpregadorData
{
    public string Nome { get; set; }    
}

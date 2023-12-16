using MeuPonto.Models.Timesheet.Pontos.Folhas;

namespace MeuPonto.Support;

public class GestaoFolhasContext
{
    public GestaoFolhasContext()
    {
        //var hoje = DateTime.Today;

        //var folhaNova = new Folha
        //{
        //    Competencia = new DateTime(hoje.Year, hoje.Month, 1)
        //};

        //Folha = folhaNova;
    }

    public Folha Folha { get; private set; }

    public void Inicia(Folha folha)
    {
        Folha = folha;
    }

    public void ConsideraQueExiste(Folha folha)
    {
        Folha = folha;
    }

    public void Define(Folha folhaAberta)
    {
        FolhaAberta = folhaAberta;
    }

    public Folha FolhaAberta { get; private set; }
}

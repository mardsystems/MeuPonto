using MeuPonto.Concepts;

namespace MeuPonto.Modules;

public interface HomeInterface
{
    Task<Folha> ApurarFolha(Folha folhaAberta);
}
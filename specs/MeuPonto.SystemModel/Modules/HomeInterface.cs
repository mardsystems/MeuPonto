using MeuPonto.Modules.Pontos.Folhas;

namespace MeuPonto.Modules;

public interface HomeInterface
{
    Task<Folha_> ApurarFolha(Folha_ folhaAberta);
}
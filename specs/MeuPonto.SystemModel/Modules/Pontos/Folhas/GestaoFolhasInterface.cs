using MeuPonto.Concepts;

namespace MeuPonto.Modules.Pontos.Folhas;

public interface GestaoFolhasInterface
{
    Task<Folha> AbrirFolha(Folha folha);

    Task<Folha> FecharFolha(Folha folhaAberta);
}
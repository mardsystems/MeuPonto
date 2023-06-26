using MeuPonto.Concepts;

namespace MeuPonto.Modules.Pontos.Folhas;

public interface GestaoFolhasInterface
{
    Folha AbrirFolha(Folha folha);

    Folha FecharFolha(Folha folhaAberta);
}
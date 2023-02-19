namespace MeuPonto.Modules.Pontos.Folhas;

public interface GestaoFolhasInterface
{
    Task<Folha_> AbrirFolha(Folha_ folha);

    Task<Folha_> FecharFolha(Folha_ folhaAberta);
}
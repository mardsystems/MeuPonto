namespace MeuPonto.Modules.Pontos.Folhas;

public static class PerfilFacade
{
    public static void QualificaFolha(this Perfis.Perfil perfil, Folha folha)
    {
        folha.Perfil = new Perfil
        {
            Nome = perfil.Nome
        };

        folha.PerfilId = perfil.Id;
    }
}

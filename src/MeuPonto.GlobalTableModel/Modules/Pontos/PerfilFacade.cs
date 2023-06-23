namespace MeuPonto.Modules.Pontos;

public static class PerfilFacade
{
    public static void QualificaPonto(this Perfis.Perfil perfil, Ponto ponto)
    {
        ponto.Perfil = perfil;

        ponto.PerfilId = perfil.Id;
    }
}

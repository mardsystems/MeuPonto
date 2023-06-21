using MeuPonto.Modules.Perfis;

namespace MeuPonto.Modules.Pontos;

public static class PerfilFacade
{
    public static void QualificaPonto(this Perfil perfil, Ponto ponto)
    {
        ponto.Perfil = new PerfilRef
        {
            Nome = perfil.Nome
        };

        ponto.PerfilId = perfil.Id;
    }
}

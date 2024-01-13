using MeuPonto.Models.Timesheet.Perfis;

namespace MeuPonto.Models.Timesheet.Pontos;

public static class PerfilFacade
{
    public static void QualificaPonto(this Perfil perfil, Ponto ponto)
    {
        ponto.Perfil = perfil;

        ponto.PerfilId = perfil.Id;
    }
}

using MeuPonto.Models.Timesheet.Perfis;

namespace MeuPonto.Models.Timesheet.Pontos.Folhas;

public static class PerfilFacade
{
    public static void QualificaFolha(this Perfil perfil, Folha folha)
    {
        folha.Perfil = new PerfilRef
        {
            Nome = perfil.Nome
        };

        folha.PerfilId = perfil.Id;
    }
}

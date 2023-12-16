using MeuPonto.Models.Timesheet.Empregadores;

namespace MeuPonto.Models.Timesheet.Perfis;

public static class PerfilFacade
{
    public static void VinculaEmpregador(this Perfil perfil, Empregador empregador)
    {
        perfil.Empregador = empregador;

        perfil.EmpregadorId = empregador.Id;
    }
}

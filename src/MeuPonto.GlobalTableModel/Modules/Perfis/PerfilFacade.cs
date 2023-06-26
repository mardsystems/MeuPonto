using MeuPonto.Modules.Perfis.Empregadores;

namespace MeuPonto.Modules.Perfis;

public static class PerfilFacade
{
    public static void VinculaEmpregador(this Perfil perfil, Empregador empregador)
    {
        perfil.Empregador = empregador;

        perfil.EmpregadorId = empregador.Id;
    }
}

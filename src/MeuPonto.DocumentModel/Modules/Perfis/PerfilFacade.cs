using MeuPonto.Modules.Empregadores;

namespace MeuPonto.Modules.Perfis;

public static class PerfilFacade
{
    public static void VinculaEmpregador(this Perfil perfil, Empregador empregador)
    {
        perfil.Empregador = new EmpregadorRef
        {
            Nome = empregador.Nome
        };

        perfil.EmpregadorId = empregador.Id;
    }
}

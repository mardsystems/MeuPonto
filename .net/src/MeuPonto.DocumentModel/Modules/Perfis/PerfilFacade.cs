namespace MeuPonto.Modules.Perfis;

public static class PerfilFacade
{
    public static void VinculaEmpregador(this Perfil perfil, Empregadores.Empregador empregador)
    {
        perfil.Empregador = new EmpregadorRef
        {
            Nome = empregador.Nome
        };

        perfil.EmpregadorId = empregador.Id;
    }
}

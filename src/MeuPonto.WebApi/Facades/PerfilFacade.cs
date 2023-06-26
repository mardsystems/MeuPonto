using MeuPonto.Models;

namespace MeuPonto.Facades;

public static class PerfilFacade
{
    public static void VinculaEmpregador(this Perfil perfil, Empregador empregador)
    {
        perfil.Empregador = empregador;

        perfil.EmpregadorId = empregador.Id;
    }

    public static void QualificaPonto(this Perfil perfil, Ponto ponto)
    {
        ponto.Perfil = perfil;

        ponto.PerfilId = perfil.Id;
    }
    
    public static void QualificaFolha(this Perfil perfil, Folha folha)
    {
        folha.Perfil = perfil;

        folha.PerfilId = perfil.Id;
    }
}


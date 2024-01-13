using MeuPonto.Models;

namespace MeuPonto.Facades;

public static class PontoFacade
{
    public static bool EstaQualificado(this Ponto ponto)
    {
        return ponto.ContratoId.HasValue;
    }

    public static bool EstaSemQualificacao(this Ponto ponto)
    {
        return ponto.ContratoId == null;
    }
}

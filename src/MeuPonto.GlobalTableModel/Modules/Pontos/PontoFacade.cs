namespace MeuPonto.Modules.Pontos;

public static class PontoFacade
{
    public static bool EstaQualificado(this Ponto ponto)
    {
        return ponto.PerfilId.HasValue;
    }

    public static bool EstaSemQualificacao(this Ponto ponto)
    {
        return ponto.PerfilId == null;
    }
}

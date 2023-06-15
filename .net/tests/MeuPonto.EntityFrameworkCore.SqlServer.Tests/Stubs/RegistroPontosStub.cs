using MeuPonto.Enums;
using MeuPonto.Models;

namespace MeuPonto.Stubs;

public static class RegistroPontosStub
{
    public static Ponto ObtemPonto(Perfil perfil, DateTime data, MomentoEnum momento)
    {
        var pontoEntrada = new Ponto
        {
            Perfil = perfil,
            PerfilId = perfil.Id,
            DataHora = data,
            MomentoId = momento,
            Id = null,
            CreationDate = DateTime.Now
        };

        return pontoEntrada;
    }

    public static void QualificaCom(this Ponto ponto, Perfil perfil)
    {
        ponto.Perfil = perfil;

        ponto.PerfilId = perfil.Id;
    }
}

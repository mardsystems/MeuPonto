using MeuPonto.Modules.Perfis;

namespace MeuPonto.Modules.Pontos;

public static class RegistroPontosStub
{
    public static Ponto ObtemPonto(Perfil perfil, DateTime data, MomentoEnum momento)
    {
        var pontoEntrada = new Ponto
        {
            PerfilId = perfil.Id,
            DataHora = data,
            Momento = momento,
            Id = Guid.NewGuid(),
            CreationDate = DateTime.Now
        };

        return pontoEntrada;
    }

    public static void DefinePerfil(this Ponto ponto, Perfil perfil)
    {
        ponto.Perfil = perfil;

        ponto.PerfilId = perfil.Id;
    }
}

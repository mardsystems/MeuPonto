using MeuPonto.Modules.Perfis;

namespace MeuPonto.Modules.Pontos;

public static class RegistroPontosStub
{
    public static Ponto ObtemPonto(Perfil perfil, DateTime data, MomentoEnum momento)
    {
        var pontoEntrada = new Ponto
        {
            Perfil = new PerfilRef
            {
                Nome = perfil.Nome
            },
            PerfilId = perfil.Id,
            DataHora = data,
            Momento = momento,
            Id = Guid.NewGuid(),
            PartitionKey = "Test user",
            CreationDate = DateTime.Now
        };

        return pontoEntrada;
    }

    public static void QualificaCom(this Ponto ponto, Perfil perfil)
    {
        ponto.Perfil = new PerfilRef
        {
            Nome = perfil.Nome
        };

        ponto.PerfilId = perfil.Id;
    }
}

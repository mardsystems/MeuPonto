namespace MeuPonto.Modules.Pontos.Comprovantes;

public static class BackupComprovantesStub
{
    public static Comprovante ObtemComprovante(Ponto ponto)
    {
        var comprovante = new Comprovante
        {
            PontoId = ponto.Id,
            Ponto = new Ponto
            {
                PerfilId = ponto.PerfilId,
                DataHora = ponto.DataHora,
            },
            Id = Guid.NewGuid(),
            CreationDate = DateTime.Now,
        };

        return comprovante;
    }
}

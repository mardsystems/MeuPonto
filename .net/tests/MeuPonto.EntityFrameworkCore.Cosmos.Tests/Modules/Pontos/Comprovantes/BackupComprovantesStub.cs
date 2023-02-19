namespace MeuPonto.Modules.Pontos.Comprovantes;

public static class BackupComprovantesStub
{
    public static Comprovante ObtemComprovante(Ponto ponto)
    {
        var comprovante = new Comprovante
        {
            PontoId = ponto.Id,
            Ponto = new PontoRef
            {
                PerfilId = ponto.PerfilId,
                DataHora = ponto.DataHora,
            },
            Id = Guid.NewGuid(),
            PartitionKey = "Test user",
            CreationDate = DateTime.Now,
        };

        return comprovante;
    }
}

using MeuPonto.Concepts;

namespace MeuPonto.Modules.Pontos.Comprovantes;

public interface BackupComprovantesInterface
{
    Task<Comprovante> EscanearComprovante(Stream imagem, Comprovante comprovante, Ponto ponto);

    Task<Comprovante> GuardarComprovante(Stream imagem, Comprovante comprovante, Ponto ponto);
}

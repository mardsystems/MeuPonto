using MeuPonto.Concepts;

namespace MeuPonto.Modules.Pontos.Comprovantes;

public interface BackupComprovantesInterface
{
    Comprovante EscanearComprovante(Stream imagem, Comprovante comprovante, Ponto ponto);

    Comprovante GuardarComprovante(Stream imagem, Comprovante comprovante, Ponto ponto);
}

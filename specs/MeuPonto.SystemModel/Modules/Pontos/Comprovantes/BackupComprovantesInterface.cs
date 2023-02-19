namespace MeuPonto.Modules.Pontos.Comprovantes;

public interface BackupComprovantesInterface
{
    Task<Comprovante_> EscanearComprovante(Stream imagem, Comprovante_ comprovante, Pontos.Ponto_ ponto);

    Task<Comprovante_> GuardarComprovante(Stream imagem, Comprovante_ comprovante, Pontos.Ponto_ ponto);
}

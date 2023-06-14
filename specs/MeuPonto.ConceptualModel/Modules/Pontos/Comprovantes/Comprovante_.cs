namespace MeuPonto.Modules.Pontos.Comprovantes;

public interface Comprovante_
{
    Ponto_? Ponto { get; }   
    string? Numero { get; }
    byte[]? Imagem { get; }
    TipoImagem_? TipoImagem { get; }
}
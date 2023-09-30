namespace MeuPonto.Concepts;

public interface Comprovante
{
    string? Numero { get; }
    byte[]? Imagem { get; }
    string? TipoImagem { get; }

    Ponto? Comprova();
}
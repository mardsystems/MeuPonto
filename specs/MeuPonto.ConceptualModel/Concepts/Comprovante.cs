namespace MeuPonto.Concepts;

public interface Comprovante
{
    Ponto? Ponto { get; }   
    string? Numero { get; }
    byte[]? Imagem { get; }
    string? TipoImagem { get; }
}
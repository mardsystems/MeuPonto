namespace MeuPonto.Concepts;

public interface Ponto
{
    Perfil? Perfil { get; }    
    DateTime? DataHora { get; }
    string? Momento { get; }
    string? Pausa { get; }
    bool Estimado { get; }
    string? Observacao { get; }
}
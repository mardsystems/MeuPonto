namespace MeuPonto.Concepts;

public interface Ponto
{
    Perfil? Perfil { get; }
    DateTime? DataHora { get; }
    Momento? Momento { get; }
    Pausa? Pausa { get; }
    bool Estimado { get; }
    string? Observacao { get; }
}
namespace MeuPonto.Concepts;

public interface Ponto
{
    DateTime? DataHora { get; }
    string? Momento { get; }
    string? Pausa { get; }
    bool Estimado { get; }
    string? Observacao { get; }

    Contrato EReferenteAo();
}
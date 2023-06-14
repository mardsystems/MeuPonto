namespace MeuPonto.Modules.Pontos;

public interface Ponto_
{
    Perfil_? Perfil { get; }
    DateTime? DataHora { get; }
    Momento_? Momento { get; }
    Pausa_? Pausa { get; }
    bool Estimado { get; }
    string? Observacao { get; }
}
namespace MeuPonto.Concepts;

public interface Ponto
{
    DateTime? DataHora { get; }
    Momento? Momento { get; }
    Pausa? Pausa { get; }
    bool Estimado { get; }
    string? Observacao { get; }

    Comprovante[] Guarda();
    Perfil EQualificadoPelo();
}
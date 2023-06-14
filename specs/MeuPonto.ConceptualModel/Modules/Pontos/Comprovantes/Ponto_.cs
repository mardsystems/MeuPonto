namespace MeuPonto.Modules.Pontos.Comprovantes;

public interface Ponto_
{
    Perfil_? Perfil { get; }
    DateTime? DataHora { get; }
    Momento_? Momento { get; }
    Pausa_? Pausa { get; }
}
namespace MeuPonto.Concepts;

public interface Perfil
{
    string? Nome { get; }
    bool Ativo { get; }

    Contrato? IdentificaVinculo();
}
namespace MeuPonto.Concepts;

public interface Empresa
{
    string? Nome { get; }
    string? Cnpj { get; }
    string? Endereco { get; }
    string? InscricaoEstadual { get; }
}
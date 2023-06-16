namespace MeuPonto.Concepts;

public interface Empregador
{
    string? Nome { get; }
    string? Cnpj { get; }
    string? Cpf { get; }
    string? Endereco { get; }
    string? InscricaoEstadual { get; }
}
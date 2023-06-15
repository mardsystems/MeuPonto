namespace MeuPonto.Concepts;

public interface Folha
{
    Perfil? Perfil { get; }    
    DateTime? Competencia { get; }
    ApuracaoMensal ApuracaoMensal { get; }
    Status? Status { get; }
    string? Observacao { get; }
}
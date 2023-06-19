namespace MeuPonto.Concepts;

public interface Folha
{
    ApuracaoMensal ApuracaoMensal { get; }
    DateTime? Competencia { get; }
    Status? Status { get; }
    string? Observacao { get; }

    Ponto[] Apura();
    Perfil EQualificadaPelo();
}
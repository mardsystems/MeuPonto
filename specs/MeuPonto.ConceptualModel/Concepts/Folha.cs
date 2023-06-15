namespace MeuPonto.Concepts;

public interface Folha
{
    DateTime? Competencia { get; }
    Status? Status { get; }
    string? Observacao { get; }

    ApuracaoMensal Guarda();
    Ponto[] Apura();
    Perfil EQualificadaPelo();
}
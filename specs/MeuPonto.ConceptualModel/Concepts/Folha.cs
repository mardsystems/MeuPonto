namespace MeuPonto.Concepts;

public interface Folha
{
    ApuracaoMensal ApuracaoMensal { get; }
    DateTime? Competencia { get; }
    string? Status { get; }
    string? Observacao { get; }

    Ponto[] Registra();
    Contrato EReferenteAo();
}
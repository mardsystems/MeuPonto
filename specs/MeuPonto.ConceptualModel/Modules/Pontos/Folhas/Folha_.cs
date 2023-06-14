namespace MeuPonto.Modules.Pontos.Folhas;

public interface Folha_
{
    Perfil_? Perfil { get; }    
    DateTime? Competencia { get; }
    ApuracaoMensal_ ApuracaoMensal { get; }
    Status_? Status { get; }
    string? Observacao { get; }
}
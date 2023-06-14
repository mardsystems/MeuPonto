namespace MeuPonto.Modules.Perfis;

public interface Perfil_
{
    string? Nome { get; }
    bool Ativo { get; }
    string? Matricula { get; }
    Empresa_? Empresa { get; }
    JornadaTrabalhoSemanal_ JornadaTrabalhoSemanalPrevista { get; }   
}
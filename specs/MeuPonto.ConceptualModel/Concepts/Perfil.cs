namespace MeuPonto.Concepts;

public interface Perfil
{
    string? Nome { get; }
    bool Ativo { get; }
    string? Matricula { get; }

    Empregador? Vincula();
    JornadaTrabalhoSemanal Preve();
}
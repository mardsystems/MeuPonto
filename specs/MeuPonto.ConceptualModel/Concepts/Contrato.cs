namespace MeuPonto.Concepts;

public interface Contrato
{
    string? Nome { get; }
    bool Ativo { get; }

    Empregador? Vincula();
    JornadaTrabalhoSemanal Preve();
}

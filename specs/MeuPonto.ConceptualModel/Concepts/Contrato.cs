namespace MeuPonto.Concepts;

public interface Contrato
{
    string? Matricula { get; }

    Empregador? Vincula();
    JornadaTrabalhoSemanal Preve();
}

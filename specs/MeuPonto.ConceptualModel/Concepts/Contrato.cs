namespace MeuPonto.Concepts;

public interface Contrato
{
    Empregador? Vincula();
    JornadaTrabalhoSemanal Preve();
}

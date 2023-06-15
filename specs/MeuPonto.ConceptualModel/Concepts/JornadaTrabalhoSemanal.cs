namespace MeuPonto.Concepts;

public interface JornadaTrabalhoSemanal
{
    IList<JornadaTrabalhoDiaria> Semana { get; }
    TimeSpan TempoTotal { get; }
}
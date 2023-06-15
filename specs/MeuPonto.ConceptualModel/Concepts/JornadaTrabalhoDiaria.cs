namespace MeuPonto.Concepts;

public interface JornadaTrabalhoDiaria
{
    DayOfWeek? DiaSemana { get; }
    TimeSpan? Tempo { get; }
}
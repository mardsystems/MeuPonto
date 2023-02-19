namespace MeuPonto.Modules.Perfis;

public interface JornadaTrabalhoDiaria_
{
    DayOfWeek? DiaSemana { get; }
    TimeSpan? Tempo { get; }
}
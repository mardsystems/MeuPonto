namespace MeuPonto.Modules.Perfis;

public interface JornadaTrabalhoSemanal_
{
    IList<JornadaTrabalhoDiaria_> Semana { get; }
    TimeSpan TempoTotal { get; }
}
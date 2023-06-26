namespace MeuPonto.Concepts;

public interface ApuracaoMensal
{
    IList<ApuracaoDiaria> Dias { get;}
    int TotalDias { get; }
    TimeSpan? TempoTotalPrevisto { get; }
    TimeSpan? TempoTotalApurado { get; }
    TimeSpan? DiferencaTempoTotal { get; }
    TimeSpan? TempoTotalPeriodoAnterior { get; }
}
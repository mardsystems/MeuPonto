namespace MeuPonto.Modules.Pontos.Folhas;

public interface ApuracaoMensal_
{
    IList<ApuracaoDiaria_> Dias { get;}
    int TotalDias { get; }
    TimeSpan? TempoTotalPrevisto { get; }
    TimeSpan? TempoTotalApurado { get; }
    TimeSpan? DiferencaTempoTotal { get; }
    TimeSpan? TempoTotalPeriodoAnterior { get; }
}
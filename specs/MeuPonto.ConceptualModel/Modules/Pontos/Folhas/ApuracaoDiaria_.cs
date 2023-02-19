namespace MeuPonto.Modules.Pontos.Folhas;

public interface ApuracaoDiaria_
{
    int? Dia { get; }
    TimeSpan? TempoPrevisto { get; }
    TimeSpan? TempoApurado { get; }
    TimeSpan? DiferencaTempo { get; }
    TimeSpan? TempoAbonado { get; }
    bool Feriado { get; }
    bool Falta { get; }
}
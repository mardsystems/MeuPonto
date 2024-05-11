using MeuPonto.Pages.Shared;
using System.ComponentModel.DataAnnotations;
using MeuPonto.Models.Pontos;

namespace MeuPonto.Pages.Folhas;

public class ApuracaoMensalViewModel
{
    public DayOfWeek PrimeiroDiaSemanaMes { get; set; }

    public IList<ApuracaoSemanalViewModel> Semanas { get; set; } = default!;

    public int TotalSemanas { get => Semanas.Count; }

    public IList<ApuracaoDiariaViewModel> Dias { get; set; } = default!;

    public int TotalDias { get => Dias.Count; }

    public TimeSpan TempoTotalPrevisto { get; set; }

    public TimeSpan TempoTotalApurado { get; set; }

    public TimeSpan DiferencaTempoTotal { get; set; }

    public TempoPeriodo TempoPeriodo { get; set; }

    public ApuracaoMensalViewModel()
    {
        Semanas = new List<ApuracaoSemanalViewModel>();

        Dias = new List<ApuracaoDiariaViewModel>();

        TempoPeriodo = new TempoPeriodo();
    }
}

public class ApuracaoSemanalViewModel
{
    public int NumeroSemana { get; set; }

    [DisplayFormat(DataFormatString = "{0:d\\d\\ hh\\:mm}")]
    public TimeSpan TempoTotalPrevisto { get; set; }

    [DisplayFormat(DataFormatString = "{0:d\\d\\ hh\\:mm}")]
    public TimeSpan TempoTotalApurado { get; set; }

    [DisplayFormat(DataFormatString = "{0:d\\d\\ hh\\:mm}")]
    public TimeSpan DiferencaTempoTotal { get; set; }

    public TempoPeriodo TempoPeriodo { get; set; }

    public ApuracaoSemanalViewModel()
    {
        TempoPeriodo = new TempoPeriodo();
    }
}

public class ApuracaoDiariaViewModel
{
    public int Dia { get; set; }

    public DayOfWeek DiaSemana { get; set; }

    public string DescricaoDia { get; set; }

    public TimeSpan TempoPrevisto { get; set; }

    public TimeSpan TempoApurado { get; set; }

    public bool TempoApuradoIdeterminado { get; set; }

    public TimeSpan DiferencaTempo { get; set; }

    public TimeSpan TempoAbonado { get; set; }

    public bool Hoje { get; set; }

    public bool Feriado { get; set; }

    public bool Falta { get; set; }

    public string Observacao { get; set; }

    public TempoPeriodo TempoPeriodo { get; set; }

    public DateTime? DataHoraInicio { get; set; }

    public DateTime? DataHoraFim { get; set; }

    public Ponto[] Pontos { get; set; }

    public ApuracaoDiariaViewModel()
    {
        TempoPeriodo = new TempoPeriodo();
    }
}
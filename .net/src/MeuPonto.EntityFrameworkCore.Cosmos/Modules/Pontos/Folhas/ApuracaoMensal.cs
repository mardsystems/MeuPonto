using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace MeuPonto.Modules.Pontos.Folhas;

[Owned]
public class ApuracaoMensal : ApuracaoMensal_
{
    [DisplayName("Dias")]
    public IList<ApuracaoDiaria> Dias { get; set; }
    IList<ApuracaoDiaria_> ApuracaoMensal_.Dias => Dias.Cast<ApuracaoDiaria_>().ToList();

    [DisplayName("Total Dias")]
    public int TotalDias { get => Dias.Count; }

    [DisplayName("Tempo Total Previsto")]
    //[DisplayFormat(DataFormatString = "{0:d\\d\\ hh\\:mm}")]
    public TimeSpan? TempoTotalPrevisto { get; set; }

    [DisplayName("Tempo Total Apurado")]
    //[DisplayFormat(DataFormatString = "{0:d\\d\\ hh\\:mm}")]
    public TimeSpan? TempoTotalApurado { get; set; }

    [DisplayName("Diferença Tempo Total")]
    //[DisplayFormat(DataFormatString = "{0:d\\d\\ hh\\:mm}")]
    public TimeSpan? DiferencaTempoTotal { get; set; }

    [DisplayName("Tempo Total Período Anterior")]
    //[DisplayFormat(DataFormatString = "{0:d\\d\\ hh\\:mm}")]
    public TimeSpan? TempoTotalPeriodoAnterior { get; set; }

    public ApuracaoMensal()
    {
        Dias = new List<ApuracaoDiaria>();
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace MeuPonto.Modules.Pontos.Folhas;

[Owned]
public class ApuracaoMensal
{
    [DisplayName("Dias")]
    public IList<ApuracaoDiaria> Dias { get; set; }

    [DisplayName("Total Dias")]
    public int TotalDias { get => Dias.Count; }

    [DisplayName("Tempo Total Previsto")]
    //[DisplayFormat(DataFormatString = "{0:d\\d\\ hh\\:mm}")]
    public TimeSpan? TempoTotalPrevisto
    {
        get
        {
            TimeSpan? total = null;

            foreach (var apuracaoDiaria in Dias)
            {
                if (apuracaoDiaria.TempoPrevisto.HasValue)
                {
                    total = (total ?? TimeSpan.Zero) + apuracaoDiaria.TempoPrevisto;
                }
            }

            return total;
        }
    }

    [DisplayName("Tempo Total Apurado")]
    //[DisplayFormat(DataFormatString = "{0:d\\d\\ hh\\:mm}")]
    public TimeSpan? TempoTotalApurado
    {
        get
        {
            TimeSpan? total = null;

            foreach (var apuracaoDiaria in Dias)
            {
                if (apuracaoDiaria.TempoApurado.HasValue)
                {
                    total = (total ?? TimeSpan.Zero) + apuracaoDiaria.TempoApurado + (apuracaoDiaria.TempoAbonado ?? TimeSpan.Zero);
                }
            }

            return total;
        }
    }

    [DisplayName("Diferença Tempo Total")]
    //[DisplayFormat(DataFormatString = "{0:d\\d\\ hh\\:mm}")]
    public TimeSpan? DiferencaTempoTotal
    {
        get
        {
            TimeSpan? total = null;

            foreach (var apuracaoDiaria in Dias)
            {
                if (apuracaoDiaria.DiferencaTempo.HasValue)
                {
                    total = (total ?? TimeSpan.Zero) + apuracaoDiaria.DiferencaTempo;
                }
            }

            return total;
        }
    }

    [DisplayName("Tempo Total Período Anterior")]
    //[DisplayFormat(DataFormatString = "{0:d\\d\\ hh\\:mm}")]
    public TimeSpan? TempoTotalPeriodoAnterior { get; set; }

    public ApuracaoMensal()
    {
        Dias = new List<ApuracaoDiaria>();
    }
}

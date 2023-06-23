using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MeuPonto.Models;

[Owned]
public class JornadaTrabalhoSemanal : Concepts.JornadaTrabalhoSemanal
{
    [DisplayName("Semana")]
    public virtual IList<JornadaTrabalhoDiaria> Semana { get; set; } = default!;
    IList<Concepts.JornadaTrabalhoDiaria> Concepts.JornadaTrabalhoSemanal.Semana => Semana.Cast<Concepts.JornadaTrabalhoDiaria>().ToList();

    [DisplayName("Tempo Total")]
    [DisplayFormat(DataFormatString = "{0:d\\d\\ hh\\:mm}")]
    public TimeSpan TempoTotal
    {
        get
        {
            TimeSpan tempoTotal = TimeSpan.Zero;

            foreach (var jornadaTrabalhoDiaria in Semana)
            {
                tempoTotal += jornadaTrabalhoDiaria.Tempo ?? TimeSpan.Zero;
            }

            return tempoTotal;
        }
    }

    public JornadaTrabalhoSemanal()
    {
        Semana = new List<JornadaTrabalhoDiaria>();
    }
}

using MeuPonto.Models;

namespace MeuPonto.Stubs;

public static class GestaoFolhasStub
{
    public static Folha ObtemFolhaAbertaFrom(Perfil perfil, DateTime competencia)
    {
        var folhaAberta = new Folha
        {
            Perfil = perfil,
            PerfilId = perfil.Id,
            Competencia = competencia,
            Id = Guid.NewGuid(),
            CreationDate = DateTime.Now,
        };

        var competenciaAtual = competencia;

        var competenciaPosterior = competenciaAtual.AddMonths(1);

        var dias = (competenciaPosterior - competenciaAtual).Days;

        for (int dia = 1; dia <= dias; dia++)
        {
            var data = competenciaAtual.AddDays(dia - 1);

            var pontoFolhaDia = new ApuracaoDiaria
            {
                Dia = dia,
                TempoPrevisto = perfil.JornadaTrabalhoSemanalPrevista.Semana.Single(x => x.DiaSemana == data.DayOfWeek).Tempo,
                TempoApurado = null,
                DiferencaTempo = null,
                Feriado = false,
                Falta = false
            };

            folhaAberta.ApuracaoMensal.Dias.Add(pontoFolhaDia);
        }

        folhaAberta.ApuracaoMensal.TempoTotalPeriodoAnterior = TimeSpan.Zero;

        return folhaAberta;
    }

    public static void QualificaCom(this Folha folha, Perfil perfil)
    {
        folha.Perfil = perfil;

        folha.PerfilId = perfil.Id;
    }
}

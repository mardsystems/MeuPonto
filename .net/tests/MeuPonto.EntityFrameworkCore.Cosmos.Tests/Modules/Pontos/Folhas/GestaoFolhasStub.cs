using MeuPonto.Modules.Perfis;

namespace MeuPonto.Modules.Pontos.Folhas;

public static class GestaoFolhasStub
{
    public static Folha ObtemFolhaAbertaFrom(Perfis.Perfil perfil, DateTime competencia)
    {
        var folhaAberta = new Folha
        {
            Perfil = new Perfil
            {
                Nome = perfil.Nome
            },
            PerfilId = perfil.Id,
            Competencia = competencia,
            Id = Guid.NewGuid(),
            PartitionKey = "Test user",
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

    public static void QualificaCom(this Folha folha, Perfis.Perfil perfil)
    {
        folha.Perfil = new Perfil
        {
            Nome = perfil.Nome
        };

        folha.PerfilId = perfil.Id;
    }
}

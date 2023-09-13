namespace MeuPonto.Modules.Pontos.Folhas;

public static class FolhaFacade
{
    public static void ConfirmarCompetencia(this Folha folha, Perfis.Perfil? perfil)
    {
        var competenciaAtual = folha.Competencia.Value;

        var competenciaPosterior = competenciaAtual.AddMonths(1);

        var dias = (competenciaPosterior - competenciaAtual).Days;

        for (int dia = 1; dia <= dias; dia++)
        {
            var data = competenciaAtual.AddDays(dia - 1);

            var apuracaoDiaria = new ApuracaoDiaria
            {
                Dia = dia,
                TempoPrevisto = perfil.JornadaTrabalhoSemanalPrevista.Semana.Single(x => x.DiaSemana == data.DayOfWeek).Tempo,
                TempoApurado = null,
                DiferencaTempo = null,
                Feriado = false,
                Falta = false
            };

            folha.ApuracaoMensal.Dias.Add(apuracaoDiaria);
        }

        folha.ApuracaoMensal.TempoTotalPeriodoAnterior = TimeSpan.Zero;

        folha.PartitionKey = $"{folha.TrabalhadorId}|{folha.Competencia:yyyy}";
    }
}

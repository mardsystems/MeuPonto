using MeuPonto.Modules.Pontos;
using MeuPonto.Modules.Pontos.Folhas;
using MeuPonto.Modules.Shared;
using MeuPonto.Modules.Trabalhadores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Modules;

[AllowAnonymous]
public class IndexModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(Data.MeuPontoDbContext db, ILogger<IndexModel> logger)
    {
        _db = db;

        _logger = logger;
    }

    [BindProperty(SupportsGet = true)]
    [DisplayName("Perfil")]
    public Guid? PerfilId { get; set; }

    [BindProperty(SupportsGet = true)]
    [DisplayName("Ano")]
    public int? CompetenciaAno { get; set; }

    [BindProperty(SupportsGet = true)]
    [DisplayName("Mês")]
    public int? CompetenciaMes { get; set; }

    public Folha Folha { get; set; }

    public ApuracaoMensalViewModel ApuracaoMensal { get; set; }

    public async Task OnGet()
    {
        if (User.Identity.IsAuthenticated)
        {
            if (Trabalhador.Default == null)
            {
                ViewData["HasPerfil"] = false;

                return;
            }

            var perfisSelectList = new SelectList(_db.Perfis.Where(x => x.TrabalhadorId == Trabalhador.Default.Id), "Id", "Nome");

            ViewData["PerfilId"] = perfisSelectList;

            ViewData["HasPerfil"] = perfisSelectList.Any();

            ApuracaoMensal = new ApuracaoMensalViewModel();

            var hoje = DateTime.Today;

            if (CompetenciaAno == null || CompetenciaMes == null)
            {
                CompetenciaAno = hoje.Year;

                CompetenciaMes = hoje.Month;
            }
            else
            {
                var competencia = new DateTime(CompetenciaAno.Value, CompetenciaMes.Value, 1);

                Folha = await _db.Folhas.FirstOrDefaultAsync(x => true
                    && x.PerfilId == PerfilId
                    && x.Competencia == competencia
                    && x.TrabalhadorId == Trabalhador.Default.Id);

                if (Folha != null)
                {
                    var competenciaAtual = new DateTime(hoje.Year, hoje.Month, 1);

                    var competenciaFolha = Folha.Competencia.Value;

                    var competenciaFolhaPosterior = competenciaFolha.AddMonths(1);

                    var pontos = await _db.Pontos
                        .Where(x => true
                            && x.DataHora >= competenciaFolha
                            && x.DataHora < competenciaFolhaPosterior
                            && x.TrabalhadorId == Trabalhador.Default.Id)
                        .OrderByDescending(x => x.DataHora)
                        .ToListAsync();

                    ApuracaoMensal.PrimeiroDiaSemanaMes = competenciaFolha.DayOfWeek;

                    var totalInteiroSemanas = (int)(competenciaFolhaPosterior - competenciaFolha).TotalDays / 7;

                    var restoDiasSemana = (competenciaFolhaPosterior - competenciaFolha).TotalDays % 7;

                    int totalSemanas;

                    if (restoDiasSemana == 0)
                    {
                        totalSemanas = totalInteiroSemanas;
                    }
                    else
                    {
                        totalSemanas = totalInteiroSemanas + 1;
                    }

                    ApuracaoMensal.TempoTotalPrevisto = TimeSpan.Zero;

                    ApuracaoMensal.TempoTotalApurado = TimeSpan.Zero;

                    ApuracaoMensal.DiferencaTempoTotal = TimeSpan.Zero;

                    if (competenciaFolha < competenciaAtual)
                    {
                        ApuracaoMensal.TempoPeriodo.Atual = false;
                        ApuracaoMensal.TempoPeriodo.Passado = true;
                        ApuracaoMensal.TempoPeriodo.Futuro = false;
                    }
                    else if (competenciaFolha == competenciaAtual)
                    {
                        ApuracaoMensal.TempoPeriodo.Atual = true;
                        ApuracaoMensal.TempoPeriodo.Passado = false;
                        ApuracaoMensal.TempoPeriodo.Futuro = false;
                    }
                    else
                    {
                        ApuracaoMensal.TempoPeriodo.Atual = false;
                        ApuracaoMensal.TempoPeriodo.Passado = false;
                        ApuracaoMensal.TempoPeriodo.Futuro = true;
                    }

                    for (int semanaIndex = 0; semanaIndex < totalSemanas; semanaIndex++)
                    {
                        var numeroSemanaAtual = hoje.GetWeekNumber();

                        var apuracaoSemanalModel = new ApuracaoSemanalViewModel
                        {
                            NumeroSemana = competenciaFolha.GetWeekNumber() + semanaIndex,
                            TempoTotalPrevisto = TimeSpan.Zero,
                            TempoTotalApurado = TimeSpan.Zero,
                            DiferencaTempoTotal = TimeSpan.Zero,
                        };

                        if (ApuracaoMensal.TempoPeriodo.Passado)
                        {
                            apuracaoSemanalModel.TempoPeriodo.Atual = false;
                            apuracaoSemanalModel.TempoPeriodo.Passado = true;
                            apuracaoSemanalModel.TempoPeriodo.Futuro = false;
                        }
                        else if (ApuracaoMensal.TempoPeriodo.Atual)
                        {
                            if (apuracaoSemanalModel.NumeroSemana < numeroSemanaAtual)
                            {
                                apuracaoSemanalModel.TempoPeriodo.Atual = false;
                                apuracaoSemanalModel.TempoPeriodo.Passado = true;
                                apuracaoSemanalModel.TempoPeriodo.Futuro = false;
                            }
                            else if (apuracaoSemanalModel.NumeroSemana == numeroSemanaAtual)
                            {
                                apuracaoSemanalModel.TempoPeriodo.Atual = true;
                                apuracaoSemanalModel.TempoPeriodo.Passado = false;
                                apuracaoSemanalModel.TempoPeriodo.Futuro = false;
                            }
                            else
                            {
                                apuracaoSemanalModel.TempoPeriodo.Atual = false;
                                apuracaoSemanalModel.TempoPeriodo.Passado = false;
                                apuracaoSemanalModel.TempoPeriodo.Futuro = true;
                            }
                        }
                        else
                        {
                            apuracaoSemanalModel.TempoPeriodo.Atual = false;
                            apuracaoSemanalModel.TempoPeriodo.Passado = false;
                            apuracaoSemanalModel.TempoPeriodo.Futuro = true;
                        }

                        for (int diaSemanaIndex = 0; diaSemanaIndex < 7; diaSemanaIndex++)
                        {
                            var diaIndex = (semanaIndex * 7) + diaSemanaIndex;

                            if (diaIndex >= Folha.ApuracaoMensal.TotalDias)
                            {
                                break;
                            }

                            //

                            var apuracaoDiaria = Folha.ApuracaoMensal.Dias[diaIndex];

                            DateTime? horaEntrada = null;

                            bool? tempoApuradoIndeterminado = null;

                            TimeSpan? tempoApurado = TimeSpan.Zero;

                            var pontosDoDia = pontos
                                .Where(x => x.DataHora.Value.Day == apuracaoDiaria.Dia.Value)
                                .OrderBy(x => x.DataHora);

                            foreach (var pontoDoDia in pontosDoDia)
                            {
                                if (horaEntrada == null)
                                {
                                    if (pontoDoDia.MomentoId == MomentoEnum.Entrada)
                                    {
                                        horaEntrada = pontoDoDia.DataHora;
                                    }
                                    else
                                    {
                                        tempoApuradoIndeterminado = true;

                                        break;
                                    }
                                }
                                else
                                {
                                    if (pontoDoDia.MomentoId == MomentoEnum.Saida)
                                    {
                                        var tempoRealizado = pontoDoDia.DataHora - horaEntrada;

                                        if (tempoApurado == null)
                                        {
                                            tempoApurado = tempoRealizado;
                                        }
                                        else
                                        {
                                            tempoApurado += tempoRealizado;
                                        }

                                        horaEntrada = null;
                                    }
                                    else
                                    {
                                        tempoApuradoIndeterminado = true;

                                        break;
                                    }
                                }
                            }

                            var diferencaTempo = tempoApurado - (apuracaoDiaria.TempoPrevisto ?? TimeSpan.Zero);

                            var data = competenciaFolha.AddDays(apuracaoDiaria.Dia.Value - 1);

                            var apuracaoDiariaModel = new ApuracaoDiariaViewModel
                            {
                                Dia = apuracaoDiaria.Dia.Value,
                                DiaSemana = data.DayOfWeek,
                                DescricaoDia = data.ToString("dddd"),
                                TempoPrevisto = apuracaoDiaria.TempoPrevisto ?? TimeSpan.Zero,
                                TempoApurado = tempoApurado ?? TimeSpan.Zero,
                                TempoApuradoIdeterminado = tempoApuradoIndeterminado ?? false,
                                DiferencaTempo = diferencaTempo ?? TimeSpan.Zero,
                                TempoAbonado = apuracaoDiaria.TempoAbonado ?? TimeSpan.Zero,
                                Feriado = apuracaoDiaria.Feriado,
                                Falta = apuracaoDiaria.Falta,
                                Pontos = pontosDoDia.ToArray()
                            };

                            apuracaoDiaria.TempoApurado = apuracaoDiaria.TempoApurado ?? TimeSpan.Zero;

                            apuracaoDiaria.DiferencaTempo = apuracaoDiaria.DiferencaTempo ?? TimeSpan.Zero;

                            //

                            apuracaoSemanalModel.TempoTotalPrevisto += apuracaoDiariaModel.TempoPrevisto;

                            apuracaoSemanalModel.TempoTotalApurado += apuracaoDiariaModel.TempoApurado + apuracaoDiariaModel.TempoAbonado;

                            apuracaoSemanalModel.DiferencaTempoTotal += apuracaoDiariaModel.DiferencaTempo + apuracaoDiariaModel.TempoAbonado;

                            if (apuracaoDiaria.TempoPrevisto != TimeSpan.Zero)
                            {
                            }

                            //

                            ApuracaoMensal.Dias.Add(apuracaoDiariaModel);

                            //


                            if (data < hoje)
                            {
                                apuracaoDiariaModel.TempoPeriodo.Atual = false;
                                apuracaoDiariaModel.TempoPeriodo.Passado = true;
                                apuracaoDiariaModel.TempoPeriodo.Futuro = false;
                            }
                            else if (data == hoje)
                            {
                                apuracaoDiariaModel.TempoPeriodo.Atual = true;
                                apuracaoDiariaModel.TempoPeriodo.Passado = false;
                                apuracaoDiariaModel.TempoPeriodo.Futuro = false;
                            }
                            else
                            {
                                apuracaoDiariaModel.TempoPeriodo.Atual = false;
                                apuracaoDiariaModel.TempoPeriodo.Passado = false;
                                apuracaoDiariaModel.TempoPeriodo.Futuro = true;
                            }
                        }

                        ApuracaoMensal.TempoTotalPrevisto += apuracaoSemanalModel.TempoTotalPrevisto;

                        ApuracaoMensal.TempoTotalApurado += apuracaoSemanalModel.TempoTotalApurado;

                        ApuracaoMensal.DiferencaTempoTotal += apuracaoSemanalModel.DiferencaTempoTotal;

                        ApuracaoMensal.Semanas.Add(apuracaoSemanalModel);
                    }
                }
            }
        }
    }
}

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

    public bool Feriado { get; set; }

    public bool Falta { get; set; }

    public TempoPeriodo TempoPeriodo { get; set; }

    public Ponto[] Pontos { get; set; }

    public ApuracaoDiariaViewModel()
    {
        TempoPeriodo = new TempoPeriodo();
    }
}
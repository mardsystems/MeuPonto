using MeuPonto.Extensions;
using MeuPonto.Pages.Folhas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace MeuPonto.Pages.Pontos.Folhas;

public class RelatorioModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public RelatorioModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    [BindProperty(SupportsGet = true)]
    [DisplayName("Contrato")]
    public Guid? ContratoId { get; set; }

    [BindProperty(SupportsGet = true)]
    [DisplayName("Grupo Contrato")]
    public int? ContratoGrupoId { get; set; }

    [DisplayName("Grupo Contrato")]
    public string? ContratoGrupo { get; set; }

    [BindProperty(SupportsGet = true)]
    [DisplayName("Código Contrato")]
    public string? ContratoCodigo { get; set; }

    [BindProperty(SupportsGet = true)]
    [DisplayName("CPF Contrato")]
    public string? ContratoCpf { get; set; }

    [BindProperty(SupportsGet = true)]
    [DisplayName("Nome Contrato")]
    public string? ContratoNome { get; set; }

    [BindProperty(SupportsGet = true)]
    [DisplayName("Contrato Ativo")]
    public bool ContratoAtivo { get; set; } = true;

    [BindProperty(SupportsGet = true)]
    [DisplayName("Competência")]
    public DateTime? Competencia { get; set; }

    public ApuracaoMensalViewModel ApuracaoMensal { get; set; }

    public async Task<IActionResult> OnGet()
    {
        var contratosSelectList = new SelectList(_db.Contratos.Where(x => x.UserId == User.GetUserId()), "Id", "Nome");

        ViewData["ContratoId"] = contratosSelectList;

        ViewData["HasContrato"] = contratosSelectList.Any();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        ApuracaoMensal = new ApuracaoMensalViewModel();

        var hoje = DateTime.Today;

        if (Competencia == null)
        {
            Competencia = hoje;
        }
        else
        {
            var competencia = Competencia;

            var folha = await _db.Folhas.FirstOrDefaultAsync(x => true
                && x.ContratoId == ContratoId
                && x.Competencia == competencia
                && x.UserId == User.GetUserId());

            if (folha != null)
            {
                var competenciaAtual = new DateTime(hoje.Year, hoje.Month, 1);

                var competenciaFolha = folha.Competencia.Value;

                var competenciaFolhaPosterior = competenciaFolha.AddMonths(1);

                ApuracaoMensal = await _db.ApurarFolha(folha, User, hoje, competenciaAtual, competenciaFolha, competenciaFolhaPosterior);
            }
        }

        return Page();
    }
}

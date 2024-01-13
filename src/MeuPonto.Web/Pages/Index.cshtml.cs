using MeuPonto.Extensions;
using MeuPonto.Models.Timesheet.Pontos.Folhas;
using MeuPonto.Pages.Pontos.Folhas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace MeuPonto.Pages;

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
    [DisplayName("Competência")]
    public DateTime? Competencia { get; set; }

    public Folha Folha { get; set; }

    public ApuracaoMensalViewModel ApuracaoMensal { get; set; }

    public async Task<IActionResult> OnGet()
    {
        if (User.Identity.IsAuthenticated == false)
        {
            return Page();
        }

        var perfisSelectList = new SelectList(_db.Perfis.Where(x => x.UserId == User.GetUserId()), "Id", "Nome");

        ViewData["PerfilId"] = perfisSelectList;

        ViewData["HasPerfil"] = perfisSelectList.Any();

        ApuracaoMensal = new ApuracaoMensalViewModel();

        var hoje = DateTime.Today;

        if (Competencia == null)
        {
            Competencia = hoje;
        }
        else
        {
            var competencia = Competencia;

            Folha = await _db.Folhas.FirstOrDefaultAsync(x => true
                && x.PerfilId == PerfilId
                && x.Competencia == competencia
                && x.UserId == User.GetUserId());

            if (Folha != null)
            {
                var competenciaAtual = new DateTime(hoje.Year, hoje.Month, 1);

                var competenciaFolha = Folha.Competencia.Value;

                var competenciaFolhaPosterior = competenciaFolha.AddMonths(1);

                ApuracaoMensal = await _db.ApurarFolha(Folha, User, hoje, competenciaAtual, competenciaFolha, competenciaFolhaPosterior);
            }
        }

        return Page();
    }
}

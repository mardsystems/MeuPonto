using MeuPonto.Extensions;
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
    [DisplayName("Perfil")]
    public Guid? PerfilId { get; set; }

    [BindProperty(SupportsGet = true)]
    [DisplayName("Grupo Perfil")]
    public int? PerfilGrupoId { get; set; }

    [DisplayName("Grupo Perfil")]
    public string? PerfilGrupo { get; set; }

    [BindProperty(SupportsGet = true)]
    [DisplayName("Código Perfil")]
    public string? PerfilCodigo { get; set; }

    [BindProperty(SupportsGet = true)]
    [DisplayName("CPF Perfil")]
    public string? PerfilCpf { get; set; }

    [BindProperty(SupportsGet = true)]
    [DisplayName("Nome Perfil")]
    public string? PerfilNome { get; set; }

    [BindProperty(SupportsGet = true)]
    [DisplayName("Perfil Ativo")]
    public bool PerfilAtivo { get; set; } = true;

    [BindProperty(SupportsGet = true)]
    [DisplayName("Competência")]
    public DateTime? Competencia { get; set; }

    public ApuracaoMensalViewModel ApuracaoMensal { get; set; }

    public async Task<IActionResult> OnGet()
    {
        var perfisSelectList = new SelectList(_db.Perfis.Where(x => x.UserId == User.GetUserId()), "Id", "Nome");

        ViewData["PerfilId"] = perfisSelectList;

        ViewData["HasPerfil"] = perfisSelectList.Any();

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
                && x.PerfilId == PerfilId
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

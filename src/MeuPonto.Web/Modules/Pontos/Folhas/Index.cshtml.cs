using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MeuPonto.Helpers;
using MeuPonto.Modules.Shared;

namespace MeuPonto.Modules.Pontos.Folhas;

public class IndexModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    [BindProperty(SupportsGet = true)]
    public Guid? PerfilId { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime? Competencia { get; set; }

    [BindProperty(SupportsGet = true)]
    public StatusEnum? Status { get; set; }

    [MinLength(3)]
    [MaxLength(255)]
    [BindProperty(SupportsGet = true)]
    public string? Observacao { get; set; }

    public IList<Folha> Folhas { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public int? PaginaAtual { get; set; }

    public PaginationModel Pagination { get; set; }

    public IndexModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    public async Task OnGetAsync()
    {
        ViewData["PerfilId"] = new SelectList(_db.Perfis.Where(x => x.TrabalhadorId == User.GetUserId()), "Id", "Nome").AddEmptyValue();

        var totalRegistros = await _db.Folhas.CountAsync(x => x.TrabalhadorId == User.GetUserId());

        Pagination = new PaginationModel(totalRegistros, PaginaAtual ?? 1);

        if (_db.Folhas != null)
        {
            Folhas = await _db.Folhas
                .Where(x => true
                    && (PerfilId == null || x.PerfilId == PerfilId)
                    && (Competencia == null || x.Competencia == Competencia)
                    && (Status == null || x.StatusId == Status)
                    && (Observacao == null || x.Observacao.Contains(Observacao))
                    && x.TrabalhadorId == User.GetUserId())
                .OrderByDescending(x => x.Competencia)
                .Skip((Pagination.PaginaAtual - 1) * Pagination.TamanhoPagina.Value)
                .Take(Pagination.TamanhoPagina.Value)
                .ToListAsync();
        }
    }
}

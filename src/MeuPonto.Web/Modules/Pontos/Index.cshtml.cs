using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using MeuPonto.Helpers;
using MeuPonto.Modules.Shared;

namespace MeuPonto.Modules.Pontos;

public class IndexModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public IndexModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    [BindProperty(SupportsGet = true)]
    public Guid? PerfilId { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime? DataHora { get; set; }

    [BindProperty(SupportsGet = true)]
    public MomentoEnum? Momento { get; set; }

    [BindProperty(SupportsGet = true)]
    public PausaEnum? Pausa { get; set; }

    [BindProperty(SupportsGet = true)]
    public bool? Estimado { get; set; }

    [MinLength(3)]
    [MaxLength(255)]
    [BindProperty(SupportsGet = true)]
    public string? Observacao { get; set; }

    public IList<Ponto> Pontos { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public int? PaginaAtual { get; set; }

    public PaginationModel Pagination { get; set; }

    public async Task OnGetAsync()
    {
        ViewData["PerfilId"] = new SelectList(_db.Perfis, "Id", "Nome").AddEmptyValue();

        var totalRegistros = await _db.Pontos.CountAsync();

        Pagination = new PaginationModel(totalRegistros, PaginaAtual ?? 1);

        if (_db.Pontos != null)
        {
            Pontos = await _db.Pontos
                .Where(x => true
                    && (PerfilId == null || x.PerfilId == PerfilId)
                    && (DataHora == null || x.DataHora == DataHora)
                    && (Momento == null || x.MomentoId == Momento)
                    && (Pausa == null || x.PausaId == Pausa)
                    && (Estimado == null || x.Estimado == Estimado)
                    && (Observacao == null || x.Observacao.Contains(Observacao)))
                .OrderByDescending(x => x.DataHora)
                .Skip((Pagination.PaginaAtual - 1) * Pagination.TamanhoPagina.Value)
                .Take(Pagination.TamanhoPagina.Value)
                .ToListAsync();
        }
    }
}

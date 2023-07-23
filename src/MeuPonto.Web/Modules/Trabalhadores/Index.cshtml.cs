using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MeuPonto.Modules.Shared;
using Microsoft.AspNetCore.Authorization;

namespace MeuPonto.Modules.Trabalhadores;

[Authorize(Policy = "Admin")]
public class IndexModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public IndexModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    [BindProperty(SupportsGet = true)]
    public Guid? Id { get; set; }

    public IList<Trabalhador> Trabalhadores { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public int? PaginaAtual { get; set; }

    public PaginationModel Pagination { get; set; }

    public async Task OnGetAsync()
    {
        var totalRegistros = await _db.Trabalhadores.CountAsync();

        Pagination = new PaginationModel(totalRegistros, PaginaAtual ?? 1);

        if (_db.Trabalhadores != null)
        {
            Trabalhadores = await _db.Trabalhadores
                .Where(x => true
                    && (Id == null || x.Id == Id))
                .OrderByDescending(x => x.Id)
                .Skip((Pagination.PaginaAtual - 1) * Pagination.TamanhoPagina.Value)
                .Take(Pagination.TamanhoPagina.Value)
                .ToListAsync();
        }
    }
}

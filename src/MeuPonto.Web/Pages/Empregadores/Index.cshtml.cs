using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MeuPonto.Pages.Shared;
using MeuPonto.Models.Timesheet.Empregadores;
using MeuPonto.Extensions;

namespace MeuPonto.Pages.Empregadores;

public class IndexModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public IndexModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    [MinLength(3)]
    [MaxLength(35)]
    [BindProperty(SupportsGet = true)]
    public string? Nome { get; set; }

    public IList<Empregador> Empregadores { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public int? PaginaAtual { get; set; }

    public PaginationModel Pagination { get; set; }

    public async Task OnGetAsync()
    {
        var totalRegistros = await _db.Empregadores.CountAsync();

        Pagination = new PaginationModel(totalRegistros, PaginaAtual ?? 1);

        if (_db.Empregadores != null)
        {
            Empregadores = await _db.Empregadores
                .Where(x => true
                    && (Nome == null || x.Nome == Nome)
                    && x.UserId == User.GetUserId())
                .OrderByDescending(x => x.Nome)
                .Skip((Pagination.PaginaAtual - 1) * Pagination.TamanhoPagina.Value)
                .Take(Pagination.TamanhoPagina.Value)
                .ToListAsync();
        }
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MeuPonto.Modules.Shared;

namespace MeuPonto.Modules.Perfis.Empresas;

public class IndexModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    public IndexModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    [MinLength(3)]
    [MaxLength(36)]
    [BindProperty(SupportsGet = true)]
    public string? Nome { get; set; }

    [StringLength(14)]
    [BindProperty(SupportsGet = true)]
    public string? Cnpj { get; set; }

    [StringLength(12)]
    [BindProperty(SupportsGet = true)]
    public string? InscricaoEstadual { get; set; }

    [MaxLength(36)]
    [BindProperty(SupportsGet = true)]
    public string? Endereco { get; set; }

    public IList<Empresa> Empresa { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public int? PaginaAtual { get; set; }

    public PaginationModel Pagination { get; set; }

    public async Task OnGetAsync()
    {
        var totalRegistros = await _db.Empresas.CountAsync();

        Pagination = new PaginationModel(totalRegistros, PaginaAtual ?? 1);

        if (_db.Empresas != null)
        {
            Empresa = await _db.Empresas
                .Where(x => true
                    && (Nome == null || x.Nome == Nome)
                    && (Cnpj == null || x.Cnpj == Cnpj)
                    && (InscricaoEstadual == null || x.InscricaoEstadual == InscricaoEstadual)
                    && (Endereco == null || x.Endereco == Endereco))
                .OrderByDescending(x => x.Nome)
                .Skip((Pagination.PaginaAtual - 1) * Pagination.TamanhoPagina.Value)
                .Take(Pagination.TamanhoPagina.Value)
                .ToListAsync();
        }
    }
}

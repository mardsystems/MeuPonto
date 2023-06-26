using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MeuPonto.Modules.Shared;
using Microsoft.AspNetCore.Mvc.Rendering;
using MeuPonto.Helpers;

namespace MeuPonto.Modules.Perfis;

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

    [BindProperty(SupportsGet = true)]
    public bool? Ativo { get; set; }

    [MaxLength(30)]
    [BindProperty(SupportsGet = true)]
    public string? Matricula { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid? EmpregadorId { get; set; }

    public IList<Perfil> Perfil { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public int? PaginaAtual { get; set; }

    public PaginationModel Pagination { get; set; }

    public async Task OnGetAsync()
    {
        ViewData["EmpregadorId"] = new SelectList(_db.Empregadores, "Id", "Nome").AddEmptyValue();

        var totalRegistros = await _db.Perfis.CountAsync();

        Pagination = new PaginationModel(totalRegistros, PaginaAtual ?? 1);

        if (_db.Perfis != null)
        {
            Perfil = await _db.Perfis
                .Where(x => true
                    && (Nome == null || x.Nome == Nome)
                    && (Ativo == null || x.Ativo == Ativo)
                    && (Matricula == null || x.Matricula == Matricula)
                    && (EmpregadorId == null || x.EmpregadorId == EmpregadorId))
                .OrderByDescending(x => x.Nome)
                .Skip((Pagination.PaginaAtual - 1) * Pagination.TamanhoPagina.Value)
                .Take(Pagination.TamanhoPagina.Value)
                .ToListAsync();
        }
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MeuPonto.Pages.Shared;
using Microsoft.AspNetCore.Mvc.Rendering;
using MeuPonto.Helpers;
using MeuPonto.Extensions;
using MeuPonto.Models.Contratos;

namespace MeuPonto.Pages.Contratos;

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

    [BindProperty(SupportsGet = true)]
    public bool? Ativo { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid? EmpregadorId { get; set; }

    public IList<Contrato> Contrato { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public int? PaginaAtual { get; set; }

    public PaginationModel Pagination { get; set; }

    public async Task OnGetAsync()
    {
        ViewData["EmpregadorId"] = new SelectList(_db.Empregadores.Where(x => x.UserId == User.GetUserId()), "Id", "Nome").AddEmptyValue();

        var totalRegistros = await _db.Contratos.CountAsync(x => x.UserId == User.GetUserId());

        Pagination = new PaginationModel(totalRegistros, PaginaAtual ?? 1);

        if (_db.Contratos != null)
        {
            Contrato = await _db.Contratos
                .Where(x => true
                    && (Nome == null || x.Nome == Nome)
                    && (Ativo == null || x.Ativo == Ativo)
                    && (EmpregadorId == null || x.EmpregadorId == EmpregadorId)
                    && x.UserId == User.GetUserId())
                .OrderByDescending(x => x.Nome)
                .Skip((Pagination.PaginaAtual - 1) * Pagination.TamanhoPagina.Value)
                .Take(Pagination.TamanhoPagina.Value)
                .ToListAsync();
        }
    }
}

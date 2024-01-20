using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MeuPonto.Helpers;
using MeuPonto.Pages.Shared;
using MeuPonto.Extensions;
using Timesheet.Models.Folhas;

namespace MeuPonto.Pages.Pontos.Folhas;

public class IndexModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    [BindProperty(SupportsGet = true)]
    public Guid? ContratoId { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime? Competencia { get; set; }

    [BindProperty(SupportsGet = true)]
    public StatusFolhaEnum? Status { get; set; }

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
        ViewData["ContratoId"] = new SelectList(_db.Contratos.Where(x => x.UserId == User.GetUserId()), "Id", "Nome").AddEmptyValue();

        var totalRegistros = await _db.Folhas.CountAsync(x => x.UserId == User.GetUserId());

        Pagination = new PaginationModel(totalRegistros, PaginaAtual ?? 1);

        if (_db.Folhas != null)
        {
            Folhas = await _db.Folhas
                .Where(x => true
                    && (ContratoId == null || x.ContratoId == ContratoId)
                    && (Competencia == null || x.Competencia == Competencia)
                    && (Status == null || x.StatusId == Status)
                    && (Observacao == null || x.Observacao.Contains(Observacao))
                    && x.UserId == User.GetUserId())
                .OrderByDescending(x => x.Competencia)
                .Skip((Pagination.PaginaAtual - 1) * Pagination.TamanhoPagina.Value)
                .Take(Pagination.TamanhoPagina.Value)
                .ToListAsync();
        }
    }
}

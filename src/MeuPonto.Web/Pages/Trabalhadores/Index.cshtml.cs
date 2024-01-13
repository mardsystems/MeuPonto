using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MeuPonto.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using MeuPonto.Models.Billing;
using MeuPonto.Models.Timesheet.Trabalhadores;

namespace MeuPonto.Pages.Trabalhadores;

[Authorize(Policy = "Admin")]
public class IndexModel : PageModel
{
    private readonly Data.MeuPontoDbContext _db;

    [BindProperty(SupportsGet = true)]
    public Guid? Id { get; set; }

    [BindProperty(SupportsGet = true)]
    public FiltroCustomerSubscription? CustomerSubscription { get; set; }

    public IList<Trabalhador> Trabalhadores { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public int? PaginaAtual { get; set; }

    public PaginationModel Pagination { get; set; }

    public bool AskSubscriptionConfirmation { get; set; }

    public IndexModel(Data.MeuPontoDbContext db)
    {
        _db = db;
    }

    public async Task OnGetAsync()
    {
        if (CustomerSubscription == null)
        {
            CustomerSubscription = new FiltroCustomerSubscription();
        }

        var totalRegistros = await _db.Trabalhadores.CountAsync();

        Pagination = new PaginationModel(totalRegistros, PaginaAtual ?? 1);

        if (_db.Trabalhadores != null)
        {
            Trabalhadores = await _db.Trabalhadores
                .Where(x => true
                    && (Id == null || x.Id == Id)
                    && (CustomerSubscription.SubscriptionPlanId == null || x.CustomerSubscription.SubscriptionPlanId == CustomerSubscription.SubscriptionPlanId))
                .OrderByDescending(x => x.CreationDate)
                .Skip((Pagination.PaginaAtual - 1) * Pagination.TamanhoPagina.Value)
                .Take(Pagination.TamanhoPagina.Value)
                .ToListAsync();
        }
    }
}

public class FiltroCustomerSubscription
{
    public SubscriptionPlanEnum? SubscriptionPlanId { get; set; }
}
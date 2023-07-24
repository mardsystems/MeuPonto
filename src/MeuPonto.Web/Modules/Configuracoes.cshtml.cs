using MeuPonto.Data;
using MeuPonto.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace MeuPonto.Modules;

public class ConfiguracoesModel : PageModel
{
    private readonly MeuPontoDbContext _db;

    private readonly IMemoryCache _cache;

    private readonly IAuthorizationService _authorizationService;

    private readonly ILogger<ConfiguracoesModel> _logger;

    public bool ResetSuccess { get; set; }

    [BindProperty]
    public string AskConfirmation { get; set; }

    [BindProperty]
    public bool JavascriptIsEnabled { get; set; }

    public bool AskResetConfirmation { get; set; }

    [BindProperty]
    public Configuracoes Configuracoes { get; set; } = default!;

    public ConfiguracoesModel(
        MeuPontoDbContext db,
        IMemoryCache cache,
        IAuthorizationService authorizationService,
        ILogger<ConfiguracoesModel> logger)
    {
        _db = db;

        _cache = cache;

        _authorizationService = authorizationService;

        _logger = logger;
    }

    public async Task<IActionResult> OnGet(Guid? id)
    {
        ResetSuccess = false;

        Guid userId;

        if (id.HasValue)
        {
            if ((await _authorizationService.AuthorizeAsync(User, "Admin")).Succeeded)
            {
                userId = id.Value;
            }
            else
            {
                return Forbid();
            }
        }
        else
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);

            userId = Guid.Parse(nameIdentifier.Value);
        }

        Configuracoes = await _db.Configuracoes.FindAsync(userId);

        if (Configuracoes == null)
        {
            Configuracoes = new Configuracoes
            {
                UserId = userId
            };
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id, string command)
    {
        if (AskConfirmation == "Reset 2")
        {
            AskResetConfirmation = true;

            ViewData.ShowModal(true);
        }

        if (command == "Reset")
        {
            _db.Database.EnsureDeleted();
            _db.Database.EnsureCreated();
            _cache.Remove("JavascriptIsEnabled");

            ResetSuccess = true;
        }

        Guid userId;

        if (id.HasValue)
        {
            if ((await _authorizationService.AuthorizeAsync(User, "Admin")).Succeeded)
            {
                userId = id.Value;
            }
            else
            {
                return Forbid();
            }
        }
        else
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);

            userId = Guid.Parse(nameIdentifier.Value);
        }

        Configuracoes = await _db.Configuracoes.FindAsync(userId);

        if (Configuracoes == null)
        {
            Configuracoes = new Configuracoes
            {
                UserId = userId,
                JavascriptIsEnabled = JavascriptIsEnabled
            };

            _db.Configuracoes.Add(Configuracoes);
        }

        if (JavascriptIsEnabled)
        {
            Configuracoes.JavascriptIsEnabled = true;
        }
        else
        {
            Configuracoes.JavascriptIsEnabled = false;
        }

        try
        {
            await _db.SaveChangesAsync();

            _cache.Set("JavascriptIsEnabled", JavascriptIsEnabled);

            HttpContext.JavascriptIsEnabled(JavascriptIsEnabled);
        }
        catch (Exception _)
        {
            throw;
        }

        return Page();
    }
}
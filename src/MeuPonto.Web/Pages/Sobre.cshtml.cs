using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeuPonto.Pages;

[AllowAnonymous]
public class SobreModel : PageModel
{
    private readonly ILogger<SobreModel> _logger;

    public SobreModel(ILogger<SobreModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
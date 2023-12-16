using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeuPonto.Pages;

[AllowAnonymous]
public class TermosModel : PageModel
{
    private readonly ILogger<TermosModel> _logger;

    public TermosModel(ILogger<TermosModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}
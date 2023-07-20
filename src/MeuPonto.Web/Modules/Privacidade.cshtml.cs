using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeuPonto.Modules;

public class PrivacidadeModel : PageModel
{
    private readonly ILogger<PrivacidadeModel> _logger;

    public PrivacidadeModel(ILogger<PrivacidadeModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}
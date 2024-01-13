using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace MeuPonto.Pages;

public abstract class FormPageModel : PageModel
{
    [BindProperty]
    public string? RefererUrl { get; set; }

    public void HoldRefererUrl()
    {
        RefererUrl = Request.Headers.Referer;
    }

    public bool ShouldRedirectToRefererPage()
    {
        var pageModelName = GetType().Name;

        var match = Regex.Match(pageModelName, "(.*)Model");

        var pageName = match.Groups[1].Value;

        var pageLink = Url.PageLink(pageName);

        var pageDetailLink = Url.PageLink("Detalhar");

        var should = true
            && RefererUrl != null
            && !RefererUrl.Contains(pageDetailLink)
            && !RefererUrl.Contains(pageLink);

        return should;
    }

    public RedirectResult RedirectToRefererPage()
    {
        return Redirect(RefererUrl);
    }

    public void AddTempSuccessMessage(string message)
    {
        TempData.Add("TextSuccessMessage", message);
    }

    public void AddTempSuccessMessageWithDetailLink(string message, string detailLink)
    {
        TempData.Add("TextSuccessMessage", message);
        TempData.Add("DetailLinkSuccessMessage", detailLink);
    }
}

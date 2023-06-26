using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MeuPonto.Helpers;

public static class PageModelExtensions
{
    public static void JavascriptIsEnabled(this HttpContext http, bool value)
    {
        http.Items["JavascriptIsEnabled"] = value;
    }

    public static bool JavascriptIsEnabled(this HttpContext http)
    {
        if (http.Items.ContainsKey("JavascriptIsEnabled"))
        {
            var javascriptIsEnabled = (bool)http.Items["JavascriptIsEnabled"];

            return javascriptIsEnabled;
        }
        else
        {
            return true;
        }
    }

    public static void ShowModal(this ViewDataDictionary viewData, bool value)
    {
        viewData["ShowModal"] = value;
    }

    public static bool ShowModal(this ViewDataDictionary viewData)
    {
        if (viewData.ContainsKey("ShowModal"))
        {
            var showModal = (bool)viewData["ShowModal"];

            return showModal;
        }
        else
        {
            return false;
        }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;

namespace MeuPonto.Helpers;

public static class HtmlHelperExtensions
{
    public static string ActiveClass(this IHtmlHelper htmlHelper, string route)
    {
        var routeData = htmlHelper.ViewContext.RouteData;

        var pageRoute = routeData.Values["page"].ToString();

        return pageRoute.Contains(route) ? "active" : "";
    }

    public static IEnumerable<SelectListItem> GetEnumSelectListWithEmptyValue<TEnum>(this IHtmlHelper htmlHelper) where TEnum : struct
    {
        var selectList = htmlHelper.GetEnumSelectList<TEnum>();

        var selectListWithEmptyValue = selectList.AddEmptyValue();

        return selectListWithEmptyValue;
    }

    public static IEnumerable<SelectListItem> AddEmptyValue(this IEnumerable<SelectListItem> selectList)
    {
        var selectListWithEmptyValue = new List<SelectListItem>(selectList);

        selectListWithEmptyValue.Insert(0, new SelectListItem()
        {
            Value = "",
            Text = ""
        });

        return selectListWithEmptyValue;
    }
}
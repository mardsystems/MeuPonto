using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace MeuPonto.Helpers;

public static class AngleSharpExtensions
{
    public static IHtmlFormElement GetForm(this IHtmlDocument document, string selector = "form")
    {
        var form = (IHtmlFormElement)document.QuerySelector(selector);

        return form;
    }

    public static IHtmlAnchorElement GetAnchor(this IHtmlDocument document, string selector)
    {
        var anchor = (IHtmlAnchorElement)document.QuerySelector($"a.{selector}");

        return anchor;
    }

    public static IHtmlAnchorElement GetAnchor(this IHtmlElement element, string selector)
    {
        var anchor = (IHtmlAnchorElement)element.QuerySelector($"a.{selector}");

        return anchor;
    }

    public static IHtmlTableElement GetTable(this IHtmlDocument document, string @class)
    {
        var table = (IHtmlTableElement)document.QuerySelector($"table.{@class}");

        return table;
    }

    public static IHtmlTableRowElement GetTableRowByDataId(this IHtmlTableElement table, Guid? dataId)
    {
        var tableRow = (IHtmlTableRowElement)table.QuerySelector($"tr[data-id='{dataId}']");

        return tableRow;
    }

    public static IHtmlTableRowElement GetTableRowByDataName(this IHtmlTableElement table, string name)
    {
        var tableRow = (IHtmlTableRowElement)table.QuerySelector($"tr[data-name='{name}']");

        return tableRow;
    }

    public static IHtmlInputElement GetInput(this IHtmlFormElement form, string name)
    {
        var input = (IHtmlInputElement)form.QuerySelector($"input[name='{name}']");

        return input;
    }

    public static IHtmlInputElement GetInput(this IHtmlFormElement form, string name, string labelText)
    {
        var elements = form.QuerySelectorAll($"input[name='{name}']");

        foreach (var element in elements)
        {
            var input = (IHtmlInputElement)element;

            var label = input.NextElementSibling as IHtmlLabelElement;

            if (label.TextContent.Trim() == labelText)
            {
                return input;
            }

            if (label.Title == labelText)
            {
                return input;
            }
        }

        return null;
    }

    public static IHtmlSelectElement GetSelect(this IHtmlFormElement form, string name)
    {
        var select = (IHtmlSelectElement)form.QuerySelector($"select[name='{name}']");

        return select;
    }

    public static IHtmlOptionElement GetOption(this IHtmlSelectElement select, string text)
    {
        var option = (IHtmlOptionElement)select.QuerySelectorAll("option")
            .Where(option => option.TextContent == text)
            .FirstOrDefault();

        return option;
    }

    public static IHtmlTextAreaElement GetTextArea(this IHtmlFormElement form, string name)
    {
        var textArea = (IHtmlTextAreaElement)form.QuerySelector($"textarea[name='{name}']");

        return textArea;
    }

    public static IHtmlCollection<IElement> GetValidationErrors(this IHtmlDocument document)
    {
        var erros = document.QuerySelectorAll("span.field-validation-error");

        return erros;
    }

    public static IHtmlButtonElement GetSubmitButton(this IHtmlFormElement form, string selector = "button")
    {
        var button = (IHtmlButtonElement)form.QuerySelector(selector);

        return button;
    }

    public static IHtmlElement GetDefinitionList(this IHtmlDocument document, string @class)
    {
        var definitionList = (IHtmlElement)document.QuerySelector($"dl.{@class}");

        return definitionList;
    }

    public static IHtmlElement GetTermListItem(this IHtmlElement element, string @class)
    {
        var dataListItem = (IHtmlElement)element.QuerySelector($"dt.{@class}");

        return dataListItem;
    }

    public static IHtmlElement GetDataListItem(this IHtmlElement element, string @class)
    {
        var dataListItem = (IHtmlElement)element.QuerySelector($"dd.{@class}");

        return dataListItem;
    }

    public static IHtmlInputElement GetInput(this IHtmlElement element)
    {
        var input = (IHtmlInputElement)element.QuerySelector("input");

        return input;
    }

    public static string GetString(this IHtmlElement element)
    {
        var @string = element.TextContent?.Trim();

        return @string;
    }
}

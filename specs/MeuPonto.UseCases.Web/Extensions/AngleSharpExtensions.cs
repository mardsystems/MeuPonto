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

        if (anchor == null)
        {
            throw new NullReferenceException($"a.{selector} not found");
        }

        return anchor;
    }

    public static IHtmlAnchorElement GetAnchor(this IHtmlElement element, string selector)
    {
        var anchor = (IHtmlAnchorElement)element.QuerySelector($"a.{selector}");

        if (anchor == null)
        {
            throw new NullReferenceException($"a.{selector} not found");
        }

        return anchor;
    }

    public static IHtmlTableElement GetTable(this IHtmlDocument document, string @class)
    {
        var table = (IHtmlTableElement)document.QuerySelector($"table.{@class}");

        if (table == null)
        {
            throw new NullReferenceException($"table.{@class} not found");
        }

        return table;
    }

    public static IHtmlTableRowElement GetTableRowByDataId(this IHtmlTableElement table, Guid? dataId)
    {
        var tableRow = (IHtmlTableRowElement)table.QuerySelector($"tr[data-id='{dataId}']");

        if (tableRow == null)
        {
            throw new NullReferenceException($"tr[data-id='{dataId}'] not found");
        }

        return tableRow;
    }

    public static IHtmlTableRowElement GetTableRowByDataName(this IHtmlTableElement table, string name)
    {
        var tableRow = (IHtmlTableRowElement)table.QuerySelector($"tr[data-name='{name}']");

        if (tableRow == null)
        {
            throw new NullReferenceException($"tr[data-name='{name}'] not found");
        }

        return tableRow;
    }

    public static IHtmlInputElement GetInput(this IHtmlElement element)
    {
        var input = (IHtmlInputElement)element.QuerySelector("input");

        if (input == null)
        {
            throw new NullReferenceException($"input not found");
        }

        return input;
    }

    public static IHtmlInputElement GetInput(this IHtmlFormElement form, string name)
    {
        var input = (IHtmlInputElement)form.QuerySelector($"input[name='{name}']");

        if (input == null)
        {
            throw new NullReferenceException($"input[name='{name}'] not found");
        }

        return input;
    }

    public static IHtmlInputElement? GetCheckedRadioInput(this IHtmlFormElement form, string name)
    {
        var elements = form.QuerySelectorAll($"input[name='{name}']");

        var inputs = elements.Select(element => (IHtmlInputElement)element);

        var input = inputs.FirstOrDefault(x => x.IsChecked);

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

        throw new NullReferenceException($"input[name='{name}'] and {labelText} not found");
    }

    public static IHtmlSelectElement GetSelect(this IHtmlFormElement form, string name)
    {
        var select = (IHtmlSelectElement)form.QuerySelector($"select[name='{name}']");

        if (select == null)
        {
            throw new NullReferenceException($"select[name='{name}'] not found");
        }

        return select;
    }

    public static IHtmlOptionElement GetOption(this IHtmlSelectElement select, string text)
    {
        var option = (IHtmlOptionElement)select.QuerySelectorAll("option")
            .Where(option => option.TextContent == text)
            .FirstOrDefault();

        if (option == null)
        {
            throw new NullReferenceException($"option '{text}' not found");
        }

        return option;
    }

    public static IHtmlTextAreaElement GetTextArea(this IHtmlFormElement form, string name)
    {
        var textArea = (IHtmlTextAreaElement)form.QuerySelector($"textarea[name='{name}']");

        if (textArea == null)
        {
            throw new NullReferenceException($"textarea '{name}' not found");
        }

        return textArea;
    }

    public static IHtmlCollection<IElement> GetValidationErrors(this IHtmlDocument document)
    {
        var erros = document.QuerySelectorAll("span.field-validation-error");

        return erros;
    }

    public static IHtmlSpanElement FirstSpan(this IEnumerable<IElement> elements)
    {
        var span = (IHtmlSpanElement)elements.First();

        return span;
    }

    public static IHtmlButtonElement GetSubmitButton(this IHtmlFormElement form, string selector = "button")
    {
        var button = (IHtmlButtonElement)form.QuerySelector(selector);

        if (button == null)
        {
            throw new NullReferenceException($"{selector} not found");
        }

        return button;
    }

    public static IHtmlElement GetDefinitionList(this IHtmlDocument document, string @class)
    {
        var definitionList = (IHtmlElement)document.QuerySelector($"dl.{@class}");

        return definitionList;
    }

    public static IHtmlCollection<IElement> GetDefinitionListCollection(this IHtmlDocument document, string @class)
    {
        var elements = document.QuerySelectorAll($"dl.{@class}");

        return elements;
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

    public static string GetString(this IHtmlElement element)
    {
        var @string = element.TextContent?.Trim();

        return @string;
    }
}

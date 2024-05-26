using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Io;
using AngleSharp;
using System.Net.Http.Headers;
using System.Xml.Linq;

namespace MeuPonto.Support;

public class AngleSharpContext
{
    private readonly HttpClient _httpClient;

    public AngleSharpContext(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public IHtmlDocument GetDocument(string url)
    {
        HttpResponseMessage response = _httpClient.GetAsync(url).Result;

        var document = GetDocument(response);

        return document;
    }

    public async Task<IHtmlDocument> GetDocumentAsync(string url)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(url);

        var document = await GetDocumentAsync(response);

        return document;
    }

    public IHtmlDocument GetDocument(HttpResponseMessage response)
    {
        var document = GetDocumentAsync(response).Result;

        return document;
    }

    public async Task<IHtmlDocument> GetDocumentAsync(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK, content);

        var document = await BrowsingContext.New()
            .OpenAsync(ResponseFactory, CancellationToken.None);
        return (IHtmlDocument)document;

        void ResponseFactory(VirtualResponse htmlResponse)
        {
            htmlResponse
                .Address(response.RequestMessage.RequestUri)
                .Status(response.StatusCode);

            MapHeaders(response.Headers);
            MapHeaders(response.Content.Headers);

            htmlResponse.Content(content);

            void MapHeaders(HttpHeaders headers)
            {
                foreach (var header in headers)
                {
                    foreach (var value in header.Value)
                    {
                        htmlResponse.Header(header.Key, value);
                    }
                }
            }
        }
    }

    public HttpResponseMessage Send(
        IHtmlFormElement form,
        IHtmlElement submitButton)
    {
        return SendAsync(form, submitButton, new Dictionary<string, string>()).Result;
    }

    public Task<HttpResponseMessage> SendAsync(
        IHtmlFormElement form,
        IHtmlElement submitButton)
    {
        return SendAsync(form, submitButton, new Dictionary<string, string>());
    }

    public Task<HttpResponseMessage> SendAsync(
        IHtmlFormElement form,
        IEnumerable<KeyValuePair<string, string>> formValues)
    {
        var submitElement = Assert.Single(form.QuerySelectorAll("[type=submit]"));
        var submitButton = Assert.IsAssignableFrom<IHtmlElement>(submitElement);

        return SendAsync(form, submitButton, formValues);
    }

    public async Task<HttpResponseMessage> SendAsync(
        IHtmlFormElement form,
        IHtmlElement submitButton,
        IEnumerable<KeyValuePair<string, string>> formValues)
    {
        foreach (var kvp in formValues)
        {
            var element = Assert.IsAssignableFrom<IHtmlInputElement>(form[kvp.Key]);
            element.Value = kvp.Value;
        }

        if (form.IsValid())
        {
            var submit = form.GetSubmission(submitButton);
            var target = (Uri)submit.Target;
            if (submitButton.HasAttribute("formaction"))
            {
                var formaction = submitButton.GetAttribute("formaction");
                target = new Uri(formaction, UriKind.Relative);
            }
            var submission = new HttpRequestMessage(new System.Net.Http.HttpMethod(submit.Method.ToString()), target)
            {
                Content = new StreamContent(submit.Body)
            };

            foreach (var header in submit.Headers)
            {
                submission.Headers.TryAddWithoutValidation(header.Key, header.Value);
                submission.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            var response = await _httpClient.SendAsync(submission);

            var content = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK, content);

            return response;
        }
        else
        {
            throw new Exception("Formulário não está válido");
        }
    }
}

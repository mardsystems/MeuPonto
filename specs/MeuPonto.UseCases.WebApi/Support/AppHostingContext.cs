using Microsoft.AspNetCore.Mvc.Testing;

namespace MeuPonto.Support;

public class AppHostingContext : IDisposable
{
    private static WebApplicationFactory<Program> _webApplicationFactory;

    public static WebApplicationFactory<Program> WebApplicationFactory => _webApplicationFactory;

    public HttpClient CreateClient()
    {
        StartApp();

        _webApplicationFactory.Should().NotBeNull("the app should be running");
        return _webApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost")
        });
    }

    public void Dispose()
    {
        //nop
    }

    public void StartApp()
    {
        if (_webApplicationFactory == null)
        {
            Console.WriteLine("Starting Web Application...");
            _webApplicationFactory = new WebApplicationFactory<Program>();
        }
    }

    public static void StopApp()
    {
        if (_webApplicationFactory != null)
        {
            Console.WriteLine("Shutting down Web Application...");
            _webApplicationFactory.Dispose();
            _webApplicationFactory = null;
        }
    }
}

using BoDi;
using MeuPonto.Data;
using Microsoft.AspNetCore.Mvc.Testing;
#if !INFRA_COSMOS
using Microsoft.EntityFrameworkCore;
#endif
using System.Net.Http.Headers;
using TechTalk.SpecFlow.Infrastructure;

namespace MeuPonto.Support;

[Binding]
public class WebHook : IClassFixture<MeuPontoWebFactory<Program>>
{
    private readonly IObjectContainer _objectContainer;

    private readonly ISpecFlowOutputHelper _specFlowOutputHelper;

    private readonly WebApplicationFactory<Program> _webFactory;

    private readonly HttpClient _httpClient;

    private readonly MeuPontoDbContext _db;

    private readonly IServiceScope _serviceScope;

    private int _counter = 0;

    public WebHook(
        IObjectContainer objectContainer,
        ISpecFlowOutputHelper specFlowOutputHelper,
        MeuPontoWebFactory<Program> webFactory)
    {
        _objectContainer = objectContainer;

        _specFlowOutputHelper = specFlowOutputHelper;

        _counter++;

        specFlowOutputHelper.WriteLine($"WebHook --> {_counter}");

        _webFactory = webFactory;

        _httpClient = _webFactory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost")
        });

        //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(scheme: "TestScheme");

        objectContainer.RegisterInstanceAs(_httpClient);

        //

        _serviceScope = webFactory.Services.CreateScope();

        var scopedServices = _serviceScope.ServiceProvider;

        _db = scopedServices.GetRequiredService<MeuPontoDbContext>();

        objectContainer.RegisterInstanceAs(_db);
    }

    [BeforeScenario]
    public void InitializeWeb(FeatureContext feature, ScenarioContext scenario)
    {
        _specFlowOutputHelper.WriteLine("EnsureDeleted");

        _db.Database.EnsureDeleted();

#if INFRA_COSMOS
        _specFlowOutputHelper.WriteLine("EnsureCreated");

        _db.Database.EnsureCreated();
#else
        _specFlowOutputHelper.WriteLine("Migrate");

        _db.Database.Migrate();
#endif

        //

        //objectContainer.RegisterInstanceAs<CalculoDeTaxaDeMarcacao>(calculoDeTaxaDeMarcacaoWeb);

        //if (feature.FeatureInfo.Title == "Cálculo (ou Calculadora) de Taxa de Marcação")
        //{
        //    var calculoDeTaxaDeMarcacaoWeb = new CalculoDeTaxaDeMarcacaoWeb(httpClient);

        //    objectContainer.RegisterInstanceAs<CalculoDeTaxaDeMarcacao>(calculoDeTaxaDeMarcacaoWeb);
        //}
        //else if (feature.FeatureInfo.Title == "Cadastro de Modelos")
        //{
        //    var cadastroDeModelosWeb = new CadastroDeModelosWeb(httpClient);

        //    objectContainer.RegisterInstanceAs<CadastroDeModelos>(cadastroDeModelosWeb);
        //}
    }

    [AfterScenario]
    public void Dispose()
    {
        _serviceScope.Dispose();

        _db.Dispose();
    }

    [BeforeTestRun]
    public static void BeforeTestRunInjection(ITestRunnerManager testRunnerManager, ITestRunner testRunner)
    {
        //All parameters are resolved from the test thread container automatically.
        //Since the global container is the base container of the test thread container, globally registered services can be also injected.

        //ITestRunManager from global container
        var location = testRunnerManager.TestAssembly.Location;

        //ITestRunner from test thread container
        var threadId = testRunner.ThreadId;
    }
}

using BoDi;
using MeuPonto.Data;
using MeuPonto.Modules;
using MeuPonto.Modules.Perfis;
using MeuPonto.Modules.Pontos;
using MeuPonto.Modules.Pontos.Comprovantes;
using MeuPonto.Modules.Pontos.Folhas;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using TechTalk.SpecFlow.Infrastructure;

namespace MeuPonto.Hooks;

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
        this._objectContainer = objectContainer;

        this._specFlowOutputHelper = specFlowOutputHelper;

        _counter++;

        specFlowOutputHelper.WriteLine($"WebHook --> {_counter}");

        this._webFactory = webFactory;

        _httpClient = this._webFactory.CreateClient(new WebApplicationFactoryClientOptions
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

        //

        objectContainer.RegisterTypeAs<HomePageDriver, HomeInterface>();
        objectContainer.RegisterTypeAs<CadastroPerfisPageDriver, CadastroPerfisInterface>();
        objectContainer.RegisterTypeAs<RegistroPontosPageDriver, RegistroPontosInterface>();
        objectContainer.RegisterTypeAs<GestaoFolhasPageDriver, GestaoFolhasInterface>();
        objectContainer.RegisterTypeAs<BackupComprovantesPageDriver, BackupComprovantesInterface>();
    }

    [BeforeScenario]
    public void InitializeWeb(FeatureContext feature, ScenarioContext scenario)
    {
        _specFlowOutputHelper.WriteLine("EnsureDeleted");

        _db.Database.EnsureDeleted();

        _specFlowOutputHelper.WriteLine("EnsureCreated");

        _db.Database.EnsureCreated();

        //_db.Database.Migrate();

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

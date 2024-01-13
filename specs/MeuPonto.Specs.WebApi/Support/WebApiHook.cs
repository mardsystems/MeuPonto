using BoDi;
using MeuPonto.Data;
using TechTalk.SpecFlow.Infrastructure;

namespace MeuPonto.Support;

[Binding]
public class WebApiHook
{
    private readonly IObjectContainer _objectContainer;

    private readonly ISpecFlowOutputHelper _specFlowOutputHelper;

    private readonly WebApiContext _webApiContext;
    private readonly TestFolders _testFolders;
    private readonly ScenarioContext _scenarioContext;

    private readonly MeuPontoDbContext _db;

    private readonly IServiceScope _serviceScope;

    public WebApiHook(
        IObjectContainer objectContainer,
        ISpecFlowOutputHelper specFlowOutputHelper,
        AppHostingContext _appHostingContext,
        WebApiContext webApiContext,
        TestFolders testFolders,
        ScenarioContext scenarioContext)
    {
        _objectContainer = objectContainer;

        _specFlowOutputHelper = specFlowOutputHelper;

        _webApiContext = webApiContext;
        _testFolders = testFolders;
        _scenarioContext = scenarioContext;

        //

        _appHostingContext.StartApp();

        _serviceScope = AppHostingContext.WebApplicationFactory.Services.CreateScope();

        var scopedServices = _serviceScope.ServiceProvider;

        _db = scopedServices.GetRequiredService<MeuPontoDbContext>();

        objectContainer.RegisterInstanceAs(_db);
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
    public void WriteLog()
    {
        _serviceScope.Dispose();

        _db.Dispose();

        if (_scenarioContext.TestError != null)
        {
            var fileName = _testFolders.GetScenarioSpecificFileName(".log");
            _webApiContext.SaveLog(_testFolders.OutputFolder, fileName);
        }
    }

    [AfterTestRun]
    public static void StopApp()
    {
        AppHostingContext.StopApp();
    }
}

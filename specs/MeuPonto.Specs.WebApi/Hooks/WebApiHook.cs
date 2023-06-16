using BoDi;
using MeuPonto.Data;
using MeuPonto.Modules.Perfis;
using MeuPonto.Support;
using Microsoft.EntityFrameworkCore;

namespace MeuPonto.Hooks;

[Binding]
public class WebApiHook
{
    private readonly WebApiContext _webApiContext;
    private readonly TestFolders _testFolders;
    private readonly ScenarioContext _scenarioContext;

    private readonly MeuPontoDbContext _db;

    private readonly IServiceScope _serviceScope;

    public WebApiHook(
        IObjectContainer objectContainer,
        AppHostingContext _appHostingContext,
        WebApiContext webApiContext,
        TestFolders testFolders,
        ScenarioContext scenarioContext)
    {
        _webApiContext = webApiContext;
        _testFolders = testFolders;
        _scenarioContext = scenarioContext;

        //

        _appHostingContext.StartApp();

        _serviceScope = AppHostingContext.WebApplicationFactory.Services.CreateScope();

        var scopedServices = _serviceScope.ServiceProvider;

        _db = scopedServices.GetRequiredService<MeuPontoDbContext>();

        objectContainer.RegisterInstanceAs(_db);

        //

        objectContainer.RegisterTypeAs<CadastroPerfisApiDriver, CadastroPerfisInterface>();
    }
    
    [BeforeScenario]
    public void InitializeWeb(FeatureContext feature, ScenarioContext scenario)
    {
        _db.Database.EnsureDeleted();

        _db.Database.Migrate();
    }

    [AfterScenario]
    public void WriteLog()
    {
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

using BoDi;
using MeuPonto.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SpecFlow.Actions.WindowsAppDriver;
using TechTalk.SpecFlow.Infrastructure;

namespace MeuPonto.Support;

[Binding]
public class DesktopHook
{
    private readonly ISpecFlowOutputHelper _specFlowOutputHelper;

    private readonly MeuPontoDbContext _db;

    public DesktopHook(
        IObjectContainer objectContainer,
        ISpecFlowOutputHelper specFlowOutputHelper
        )
    {
        _specFlowOutputHelper = specFlowOutputHelper;

        var dataSource = "C:\\github\\MeuPonto\\.net\\src\\MeuPonto.Desktop\\bin\\Debug\\net6.0-windows\\MeuPonto.db";

        var connection = new SqliteConnection($"Data Source={dataSource}");

        //connection.Open();

        var dbBuilder = new DbContextOptionsBuilder<MeuPontoDbContext>()
            .UseSqlite(connection);

        var options = dbBuilder.Options;

        _db = new MeuPontoDbContext();

        objectContainer.RegisterInstanceAs(_db);
    }

    [BeforeScenario]
    public void InitializeDesktop(AppDriver driver)
    {
        //var sistemaMenuItem = driver.Current.FindElementByAccessibilityId("sistemaMenuItem");

        //sistemaMenuItem.Click();

        //var configuracoesMenuItem = driver.Current.FindElementByAccessibilityId("configuracoesMenuItem");

        //_specFlowOutputHelper.WriteLine("Migrate");

        //configuracoesMenuItem.Click();

        Thread.Sleep(500);

        _specFlowOutputHelper.WriteLine("EnsureDeleted");

        _db.Database.EnsureDeleted();

        _specFlowOutputHelper.WriteLine("Migrate");

        _db.Database.Migrate();
    }

    [AfterScenario]
    public void FinalizeDesktop()
    {

    }
}

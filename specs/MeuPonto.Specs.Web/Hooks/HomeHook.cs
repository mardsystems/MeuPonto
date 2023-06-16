using MeuPonto.Modules;

namespace MeuPonto.Hooks;

//[Binding]
public class HomeHook
{
    private readonly HomePageDriver _homePageDriver;

    public HomeHook(HomePageDriver homePageDriver)
    {
        _homePageDriver = homePageDriver;
    }

    [BeforeScenario(Order = 2)]
    public void GoHome()
    {
        _homePageDriver.GoTo();
    }
}

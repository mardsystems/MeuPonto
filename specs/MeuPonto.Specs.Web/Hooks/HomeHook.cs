using MeuPonto.Modules;

namespace MeuPonto.Hooks;

//[Binding]
public class HomeHook
{
    private readonly HomeDriver _homePageDriver;

    public HomeHook(HomeDriver homePageDriver)
    {
        _homePageDriver = homePageDriver;
    }

    [BeforeScenario(Order = 2)]
    public void GoHome()
    {
        _homePageDriver.GoTo();
    }
}

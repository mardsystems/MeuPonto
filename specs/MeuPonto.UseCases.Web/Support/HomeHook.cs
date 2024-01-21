using MeuPonto.Drivers;

namespace MeuPonto.Support;

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

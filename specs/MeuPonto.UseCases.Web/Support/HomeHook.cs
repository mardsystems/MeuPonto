using MeuPonto.Drivers;

namespace MeuPonto.Support;

//[Binding]
public class HomeHook
{
    private readonly ApuracaoPontosDriver _homePageDriver;

    public HomeHook(ApuracaoPontosDriver homePageDriver)
    {
        _homePageDriver = homePageDriver;
    }

    [BeforeScenario(Order = 2)]
    public void GoHome()
    {
        _homePageDriver.GoTo();
    }
}

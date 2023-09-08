namespace MeuPonto.Helpers;
public static class ScenarioExtensions
{
    public static Guid GetUserId(this ScenarioContext scenario)
    {
        var userId = scenario.Get<Guid>("UserId");

        return userId;
    }
}

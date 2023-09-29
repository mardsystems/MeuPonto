namespace MeuPonto.Helpers;

public static class ScenarioExtensions
{
    public static string GetUserId(this ScenarioContext scenario)
    {
        var userId = scenario.Get<string>("UserId");

        return userId;
    }
}

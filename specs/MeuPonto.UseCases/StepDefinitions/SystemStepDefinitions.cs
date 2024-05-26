namespace MeuPonto.StepDefinitions;

[Binding]
public class SystemStepDefinitions
{
    private readonly ScenarioContext _scenario;

    public SystemStepDefinitions(ScenarioContext scenario)
    {
        _scenario = scenario;
    }

    [Given(@"que a data/hora do relógio é '([^']*)'")]
    public void GivenQueADataHoraDoRelogioE(DateTime dataHora)
    {
        _scenario["DataHora"] = dataHora;
    }
}

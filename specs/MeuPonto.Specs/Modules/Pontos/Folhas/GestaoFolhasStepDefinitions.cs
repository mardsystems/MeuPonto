using MeuPonto.Data;

namespace MeuPonto.Modules.Pontos.Folhas;

[Binding]
public class GestaoFolhasStepDefinitions
{
    private readonly ScenarioContext _scenario;

    private readonly GestaoFolhasContext _gestaoFolhas;

    private readonly MeuPontoDbContext _db;

    public GestaoFolhasStepDefinitions(
        ScenarioContext scenario,
        GestaoFolhasContext gestaoFolhas,
        MeuPontoDbContext db)
    {
        _scenario = scenario;

        _gestaoFolhas = gestaoFolhas;

        _db = db;
    }
}

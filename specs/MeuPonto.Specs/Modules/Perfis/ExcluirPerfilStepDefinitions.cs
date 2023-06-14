using MeuPonto.Data;

namespace MeuPonto.Modules.Perfis;

[Binding]
public class ExcluirPerfilStepDefinitions
{
    private readonly CadastroPerfisContext _cadastroPerfis;

    private readonly CadastroPerfisInterface _cadastroPerfisInterface;

    private readonly MeuPontoDbContext _db;

    public ExcluirPerfilStepDefinitions(
        CadastroPerfisContext cadastroPerfis,
        CadastroPerfisInterface cadastroPerfisInterface,
        MeuPontoDbContext db)
    {
        _cadastroPerfis = cadastroPerfis;

        _cadastroPerfisInterface = cadastroPerfisInterface;

        _db = db;
    }

    [When(@"o trabalhador excluir o perfil")]
    public async Task WhenOTrabalhadorExcluirOPerfil()
    {
        await _cadastroPerfisInterface.ExcluirPerfil(_cadastroPerfis.Perfil);
    }

    [Then(@"o perfil deverá ser excluído")]
    public void ThenOPerfilDeveraSerExcluido()
    {
        var perfil = _db.Perfis.FirstOrDefault(x => x.Matricula == _cadastroPerfis.Perfil.Matricula);

        perfil.Should().BeNull();
    }
}

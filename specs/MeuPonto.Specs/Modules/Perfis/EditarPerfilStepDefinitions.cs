using MeuPonto.Data;

namespace MeuPonto.Modules.Perfis;

[Binding]
public class EditarPerfilStepDefinitions
{
    private readonly CadastroPerfisContext _cadastroPerfis;

    private readonly CadastroPerfisInterface _cadastroPerfisInterface;

    private readonly MeuPontoDbContext _db;

    public EditarPerfilStepDefinitions(
        CadastroPerfisContext cadastroPerfis,
        CadastroPerfisInterface cadastroPerfisInterface,
        MeuPontoDbContext db)
    {
        _cadastroPerfis = cadastroPerfis;

        _cadastroPerfisInterface = cadastroPerfisInterface;
        
        _db = db;   
    }

    [When(@"o trabalhador editar o perfil")]
    public async Task WhenOTrabalhadorEditarOPerfil()
    {
        await _cadastroPerfisInterface.EditarPerfil(_cadastroPerfis.Perfil);

        var perfilEdidado = _db.Perfis.FirstOrDefault(x => x.Nome == _cadastroPerfis.Perfil.Nome);

        _cadastroPerfis.Define(perfilEdidado);
    }

    [Then(@"o perfil deverá ser editado")]
    public void ThenOPerfilDeveraSerEditado()
    {
        _cadastroPerfis.PerfilCadastrado.Should().NotBeNull();
    }
}

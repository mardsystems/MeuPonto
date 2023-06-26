namespace MeuPonto.Modules.Perfis;

[Binding]
public class DetalharPerfilStepDefinitions
{
    private readonly CadastroPerfisContext _cadastroPerfis;

    private readonly CadastroPerfisInterface _cadastroPerfisInterface;

    public DetalharPerfilStepDefinitions(
        CadastroPerfisContext cadastroPerfis,
        CadastroPerfisInterface cadastroPerfisInterface)
    {
        _cadastroPerfis = cadastroPerfis;

        _cadastroPerfisInterface = cadastroPerfisInterface;
    }

    [When(@"o trabalhador detalhar o perfil")]
    public void WhenOTrabalhadorDetalharOPerfil()
    {
        var perfilDetalhado = _cadastroPerfisInterface.DetalharPerfil(_cadastroPerfis.Perfil);

        _cadastroPerfis.Define(perfilDetalhado);
    }

    [Then(@"o perfil deverá ser detalhado")]
    public void ThenOPerfilDeveraSerDetalhado()
    {
        _cadastroPerfis.PerfilCadastrado.Should().NotBeNull();
    }
}

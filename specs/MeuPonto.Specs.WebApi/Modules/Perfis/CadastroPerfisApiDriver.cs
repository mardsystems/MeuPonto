using MeuPonto.Support;

namespace MeuPonto.Modules.Perfis;

public class CadastroPerfisApiDriver : CadastroPerfisInterface
{
    private readonly WebApiContext _webApiContext;
    public ActionAttempt<Concepts.Perfil, Perfil> CriaPerfil { get; }

    public CadastroPerfisApiDriver(WebApiContext webApiContext, ActionAttemptFactory actionAttemptFactory)
    {
        _webApiContext = webApiContext;

        CriaPerfil = actionAttemptFactory.CreateWithStatusCheck<Concepts.Perfil, Perfil>(
            nameof(CriaPerfil),
            perfil => webApiContext.ExecutePost<Perfil>("/api/Perfis", perfil));
    }

    public async Task CriarPerfil(Concepts.Perfil perfil)
    {
        CriaPerfil.Perform(perfil);

        await Task.CompletedTask;
    }

    public async Task<Concepts.Perfil> DetalharPerfil(Concepts.Perfil perfilCadastrado)
    {
        int perfilId = 0;

        var perfil = _webApiContext.ExecuteGet<Perfil>($"/api/Perfis/{perfilId}");

        return await Task.FromResult(perfil);
    }

    public Task EditarPerfil(Concepts.Perfil perfilCadastrado)
    {
        throw new NotImplementedException();
    }

    public Task ExcluirPerfil(Concepts.Perfil perfilCadastrado)
    {
        throw new NotImplementedException();
    }
}

using MeuPonto.Support;

namespace MeuPonto.Drivers;

public class CadastroPerfisDriver
{
    private readonly WebApiContext _webApiContext;
    public ActionAttempt<Perfil, Perfil> CriaPerfil { get; }

    public CadastroPerfisDriver(WebApiContext webApiContext, ActionAttemptFactory actionAttemptFactory)
    {
        _webApiContext = webApiContext;

        CriaPerfil = actionAttemptFactory.CreateWithStatusCheck<Perfil, Perfil>(
            nameof(CriaPerfil),
            perfil => webApiContext.ExecutePost<Perfil>("/api/Perfis", perfil),
            System.Net.HttpStatusCode.Created);
    }

    public void CriarPerfil(Perfil perfil)
    {
        CriaPerfil.Perform(perfil);
    }

    public Perfil DetalharPerfil(string nomePerfil)
    {
        int perfilId = 0;

        var perfil = _webApiContext.ExecuteGet<Perfil>($"/api/Perfis/{perfilId}");

        return perfil;
    }

    public void EditarPerfil(string nomePerfil, Perfil perfilCadastrado)
    {
        throw new NotImplementedException();
    }

    public void ExcluirPerfil(string nomePerfil)
    {
        throw new NotImplementedException();
    }
}

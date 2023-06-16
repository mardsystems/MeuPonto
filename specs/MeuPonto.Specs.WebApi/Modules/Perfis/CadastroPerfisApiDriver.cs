using MeuPonto.Support;

namespace MeuPonto.Modules.Perfis;

public class CadastroPerfisApiDriver : CadastroPerfisInterface
{
    private readonly WebApiContext _webApiContext;
    public ActionAttempt<Perfil, Perfil> CriaPerfil { get; }

    public CadastroPerfisApiDriver(WebApiContext webApiContext, ActionAttemptFactory actionAttemptFactory)
    {
        _webApiContext = webApiContext;

        CriaPerfil = actionAttemptFactory.CreateWithStatusCheck<Perfil, Perfil>(
            nameof(CriaPerfil),
            perfil => webApiContext.ExecutePost<Perfil>("/api/Perfis", perfil),
            System.Net.HttpStatusCode.Created);
    }

    public void CriarPerfil(Concepts.Perfil perfil)
    {
        CriaPerfil.Perform((Perfil)perfil);
    }

    public Concepts.Perfil DetalharPerfil(Concepts.Perfil perfilCadastrado)
    {
        int perfilId = 0;

        var perfil = _webApiContext.ExecuteGet<Perfil>($"/api/Perfis/{perfilId}");

        return perfil;
    }

    public void EditarPerfil(Concepts.Perfil perfilCadastrado)
    {
        throw new NotImplementedException();
    }

    public void ExcluirPerfil(Concepts.Perfil perfilCadastrado)
    {
        throw new NotImplementedException();
    }
}

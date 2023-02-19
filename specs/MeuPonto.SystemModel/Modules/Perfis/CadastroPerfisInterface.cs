namespace MeuPonto.Modules.Perfis;

public interface CadastroPerfisInterface
{
    Task CriarPerfil(Perfil_ perfil);
    Task<Perfil_> DetalharPerfil(Perfil_ perfilCadastrado);
    Task EditarPerfil(Perfil_ perfilCadastrado);
    Task ExcluirPerfil(Perfil_ perfilCadastrado);
}
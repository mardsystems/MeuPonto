using MeuPonto.Concepts;

namespace MeuPonto.Modules.Perfis;

public interface CadastroPerfisInterface
{
    Task CriarPerfil(Perfil perfil);
    Task<Perfil> DetalharPerfil(Perfil perfilCadastrado);
    Task EditarPerfil(Perfil perfilCadastrado);
    Task ExcluirPerfil(Perfil perfilCadastrado);
}
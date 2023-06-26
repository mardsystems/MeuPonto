using MeuPonto.Concepts;

namespace MeuPonto.Modules.Perfis;

public interface CadastroPerfisInterface
{
    void CriarPerfil(Perfil perfil);
    Perfil DetalharPerfil(Perfil perfilCadastrado);
    void EditarPerfil(Perfil perfilCadastrado);
    void ExcluirPerfil(Perfil perfilCadastrado);
}
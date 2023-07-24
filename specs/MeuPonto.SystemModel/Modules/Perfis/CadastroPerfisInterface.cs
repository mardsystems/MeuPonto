using MeuPonto.Concepts;

namespace MeuPonto.Modules.Perfis;

public interface CadastroPerfisInterface
{
    void CriarPerfil(Perfil perfil);
    Perfil DetalharPerfil(string nomePerfil);
    void EditarPerfil(string nomePerfil, Perfil perfilCadastrado);
    void ExcluirPerfil(string nomePerfil);
}
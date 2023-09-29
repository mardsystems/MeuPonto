using MeuPonto.Concepts;

namespace MeuPonto.Modules.Perfis;

public interface CadastroPerfisInterface
{
    void CriarPerfil(Contrato perfil);
    Contrato DetalharPerfil(string nomePerfil);
    void EditarPerfil(string nomePerfil, Contrato perfilCadastrado);
    void ExcluirPerfil(string nomePerfil);
}
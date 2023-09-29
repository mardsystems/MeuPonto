namespace MeuPonto.Modules.Perfis;

public class CadastroPerfisContext
{
    public CadastroPerfisContext()
    {
        //var perfil = new Perfil
        //{
        //    Nome = "Test user",
        //};

        //Perfil = perfil;
    }

    public void Inicia(Perfil perfil)
    {
        Perfil = perfil;

        NomePerfil = perfil.Nome;
    }

    public void ConsideraQueExiste(Perfil perfil)
    {
        Perfil = perfil;

        NomePerfil = perfil.Nome;
    }

    public Perfil Perfil { get; private set; }

    public void DefineNomePerfil(string nomePerfil)
    {
        Perfil.Nome = nomePerfil;

        NomePerfil = nomePerfil;
    }

    public string NomePerfil { get; private set; }

    public void Define(Perfil perfilCadastrado)
    {
        PerfilCadastrado = perfilCadastrado;

        NomePerfil = perfilCadastrado.Nome;
    }

    public Perfil PerfilCadastrado { get; private set; }
}

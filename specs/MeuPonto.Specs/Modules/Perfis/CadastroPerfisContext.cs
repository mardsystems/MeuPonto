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
    }

    public void ConsideraQueExiste(Perfil perfil)
    {
        Perfil = perfil;
    }

    public Perfil Perfil { get; private set; }

    public void Define(Concepts.Perfil perfilCadastrado)
    {
        PerfilCadastrado = perfilCadastrado;
    }

    public Concepts.Perfil PerfilCadastrado { get; private set; }
}

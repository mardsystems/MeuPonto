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

    public void Define(Perfil_ perfilCadastrado)
    {
        PerfilCadastrado = perfilCadastrado;
    }

    public Perfil_ PerfilCadastrado { get; private set; }
}

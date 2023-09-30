namespace MeuPonto.Modules.Pontos;

public class RegistroPontosDriver
{
    public void GoTo()
    {

    }

    public Ponto MarcarPonto(Ponto ponto)
    {
        GoTo();

        var pontoRegistrado = ObtemDetalhes();

        return pontoRegistrado;
    }

    private Ponto ObtemDetalhes()
    {
        var pontoRegistrado = new Ponto
        {
            Perfil = new()
            {

            },
        };

        return pontoRegistrado;
    }
}

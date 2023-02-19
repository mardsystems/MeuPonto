namespace MeuPonto.Modules.Pontos;

public interface RegistroPontosInterface
{
    Task<Ponto_> MarcarPonto(Ponto_ ponto);
}
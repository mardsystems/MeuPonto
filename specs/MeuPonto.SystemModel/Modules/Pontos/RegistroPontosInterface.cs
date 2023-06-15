using MeuPonto.Concepts;

namespace MeuPonto.Modules.Pontos;

public interface RegistroPontosInterface
{
    Task<Ponto> MarcarPonto(Ponto ponto);
}
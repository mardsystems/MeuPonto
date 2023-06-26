namespace MeuPonto.Concepts;

public interface Trabalhador
{
    string? Nome { get; }
    string? Pis { get; }

    Folha[] Gerencia();
    Ponto[] Registra();
    Perfil[] Cadastra();
}
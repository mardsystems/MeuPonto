namespace MeuPonto.Concepts;

public interface Trabalhador
{
    Folha[] Gerencia();
    Ponto[] Registra();
    Perfil[] Cadastra();
}
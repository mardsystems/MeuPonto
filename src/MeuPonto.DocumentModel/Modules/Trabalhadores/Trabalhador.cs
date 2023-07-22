using MeuPonto.Concepts;
using System.ComponentModel;

namespace MeuPonto.Modules.Trabalhadores;

public class Trabalhador : DocumentEntity, Concepts.Trabalhador
{
    Perfil[] Concepts.Trabalhador.Cadastra()
    {
        throw new NotImplementedException();
    }

    Folha[] Concepts.Trabalhador.Gerencia()
    {
        throw new NotImplementedException();
    }

    Ponto[] Concepts.Trabalhador.Registra()
    {
        throw new NotImplementedException();
    }
}

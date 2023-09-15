﻿using MeuPonto.Billing;
using MeuPonto.Concepts;
using System.ComponentModel;

namespace MeuPonto.Modules.Trabalhadores;

public class Trabalhador : GlobalTableEntity, Concepts.Trabalhador
{
    [DisplayName("Assinatura do Cliente")]
    public CustomerSubscription? CustomerSubscription { get; set; }

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
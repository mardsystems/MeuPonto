﻿using System.Transactions;

namespace MeuPonto.Models.Trabalhadores;

public static class TrabalhadorFactory
{
    public static Trabalhador CriaTrabalhador(TransactionContext transaction, Guid? id = null)
    {
        var trabalhador = new Trabalhador
        {
            Id = id ?? Guid.NewGuid(),
            UserId = transaction.UserId,
            PartitionKey = transaction.UserId.ToString(),
            CreationDate = transaction.DateTime
        };

        return trabalhador;
    }

    public static void RecontextualizaTrabalhador(this Trabalhador trabalhador, TransactionContext transaction, Guid? id = null)
    {
        trabalhador.Id = trabalhador.Id ?? id ?? Guid.NewGuid();
        trabalhador.UserId = transaction.UserId;
        trabalhador.PartitionKey = transaction.UserId.ToString();
        trabalhador.CreationDate ??= transaction.DateTime;
    }
}

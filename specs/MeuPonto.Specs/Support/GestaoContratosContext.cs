﻿using MeuPonto.Models.Contratos;

namespace MeuPonto.Support;

public class GestaoContratosContext
{
    public GestaoContratosContext()
    {
        //var contrato = new Contrato
        //{
        //    Nome = "Test user",
        //};

        //Contrato = contrato;
    }

    public void Inicia(Contrato contrato)
    {
        Contrato = contrato;

        NomeContrato = contrato.Nome;
    }

    public void ConsideraQueExiste(Contrato contrato)
    {
        Contrato = contrato;

        NomeContrato = contrato.Nome;
    }

    public Contrato Contrato { get; private set; }

    public void DefineNomeContrato(string nomeContrato)
    {
        Contrato.Nome = nomeContrato;

        NomeContrato = nomeContrato;
    }

    public string NomeContrato { get; private set; }

    public void Define(Contrato contratoCadastrado)
    {
        ContratoCadastrado = contratoCadastrado;

        NomeContrato = contratoCadastrado.Nome;
    }

    public Contrato ContratoCadastrado { get; private set; }
}

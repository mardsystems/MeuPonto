﻿namespace MeuPonto.Drivers;

public class GestaoFolhasDriver
{
    public void GoTo()
    {

    }

    private void Identifica(Folha folha)
    {

    }

    public Folha AbrirFolha(Folha folha)
    {
        GoTo();

        var folhaAberta = ObtemDetalhes();

        return folhaAberta;
    }

    public Folha FecharFolha(Folha folhaAberta)
    {
        GoTo();

        Identifica(folhaAberta);

        var folhaFechada = ObtemDetalhes();

        return folhaFechada;
    }

    private Folha ObtemDetalhes()
    {
        var folhaAberta = new Folha
        {
            Perfil = new()
            {

            },
            ApuracaoMensal = new ApuracaoMensal
            {

            },
        };

        return folhaAberta;
    }
}
﻿using MeuPonto.Models;

namespace MeuPonto.Facades;

public static class PerfilFacade
{
    public static void QualificaPonto(this Perfil perfil, Ponto ponto)
    {
        ponto.Perfil = perfil;

        ponto.PerfilId = perfil.Id;
    }
}


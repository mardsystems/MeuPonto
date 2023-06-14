﻿using System.ComponentModel;

namespace MeuPonto.Modules.Pontos.Comprovantes;

public class TipoImagem : TipoImagem_
{
    private readonly TipoImagemEnum _tipoImagemEnum;

    public TipoImagem(TipoImagemEnum tipoImagemEnum)
    {
        _tipoImagemEnum = tipoImagemEnum;
    }

    public string? Nome => _tipoImagemEnum.GetDisplayName();

    public override string ToString()
    {
        return Nome;
    }
}

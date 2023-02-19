using System.ComponentModel;

namespace MeuPonto.Modules.Pontos.Folhas;

public class Status : Status_
{
    private readonly StatusEnum _statusEnum;

    public Status(StatusEnum statusEnum)
    {
        _statusEnum = statusEnum;
    }

    public string? Nome => _statusEnum.GetDisplayName();

    public override string ToString()
    {
        return Nome;
    }
}

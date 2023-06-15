using System.ComponentModel;

namespace MeuPonto.Modules.Pontos.Folhas;

public class Status : Concepts.Status
{
    private readonly StatusEnum _status;

    public Status(StatusEnum status)
    {
        _status = status;
    }

    public string? Nome => _status.GetDisplayName();

    public override string ToString()
    {
        return Nome;
    }
}

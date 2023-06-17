using System.ComponentModel;

namespace MeuPonto.Modules.Pontos;

public class Pausa : Concepts.Pausa
{
    private readonly PausaEnum _pausa;

    public Pausa(PausaEnum pausa)
    {
        _pausa = pausa;
    }

    public string? Nome => _pausa.GetDisplayName();

    public override string ToString()
    {
        return Nome;
    }
}

using System.ComponentModel;

namespace MeuPonto.Modules.Pontos;

public class Pausa : Pausa_
{
    private readonly PausaEnum _pausa;

    public Pausa(PausaEnum pausa)
    {
        _pausa = pausa;
    }

    public string Nome => _pausa.GetDisplayName();

    public override string ToString()
    {
        return Nome;
    }
}

using System.ComponentModel;

namespace MeuPonto.Modules.Pontos;

public class Momento : Momento_
{
    private readonly MomentoEnum _momento;

    public Momento(MomentoEnum momento)
    {
        _momento = momento;
    }

    public string Nome => _momento.GetDisplayName();

    public override string ToString()
    {
        return Nome;
    }
}

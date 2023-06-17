using System.ComponentModel;

namespace MeuPonto.Modules.Pontos;

public class Momento : Concepts.Momento
{
    private readonly MomentoEnum _momento;

    public Momento(MomentoEnum momento)
    {
        _momento = momento;
    }

    public string? Nome => _momento.GetDisplayName();

    public override string ToString()
    {
        return Nome;
    }
}

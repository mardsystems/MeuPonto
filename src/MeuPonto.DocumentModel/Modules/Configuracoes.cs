namespace MeuPonto.Modules;

public class ConfiguracaoPorUsuario : Concepts.ConfiguracaoPorUsuario
{
    public Guid UserId { get; set; }

    public bool JavascriptIsEnabled { get; set; }
}

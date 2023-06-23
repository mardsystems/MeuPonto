namespace MeuPonto.Models;

public class ConfiguracaoPorUsuario : Concepts.ConfiguracaoPorUsuario
{
    public string UserName { get; set; }

    public bool JavascriptIsEnabled { get; set; }
}

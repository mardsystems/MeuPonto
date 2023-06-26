using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Modules.Pontos.Comprovantes;

public class TipoImagem
{
    public TipoImagemEnum Id { get; set; }

    [MaxLength(255)]
    public string? Nome { get; set; }
}

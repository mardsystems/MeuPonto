using MeuPonto.Modules.Pontos.Comprovantes;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models;

public class PontoComprovanteImagemTipo : TipoImagem_
{
    public int Id { get; set; }

    [MaxLength(255)]
    public string? Nome { get; set; }
}

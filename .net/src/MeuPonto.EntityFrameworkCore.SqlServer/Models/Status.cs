using MeuPonto.Modules.Pontos.Folhas;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models;

public class Status : Concepts.Status
{
    public StatusEnum Id { get; set; }

    [MaxLength(255)]
    public string? Nome { get; set; }
}

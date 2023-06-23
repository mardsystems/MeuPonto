using MeuPonto.Enums;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models;

public class Status
{
    public StatusEnum Id { get; set; }

    [MaxLength(255)]
    public string? Nome { get; set; }
}

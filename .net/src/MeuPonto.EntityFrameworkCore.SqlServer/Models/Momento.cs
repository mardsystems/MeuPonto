using MeuPonto.Modules.Pontos;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models;

public class Momento : Concepts.Momento
{
    public MomentoEnum Id { get; set; }

    [MaxLength(255)]
    public string? Nome { get; set; }
}

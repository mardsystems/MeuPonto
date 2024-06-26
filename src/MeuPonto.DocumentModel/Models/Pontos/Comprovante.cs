﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models.Pontos;

public class Comprovante : DocumentEntity
{
    [Required]
    [DisplayName("Ponto")]
    public Guid? PontoId { get; set; }

    [DisplayName("Ponto")]
    public PontoRef? Ponto { get; set; }

    [Required]
    [DisplayName("Imagem")]
    public byte[]? Imagem { get; set; }

    [Required]
    [DisplayName("Tipo Imagem")]
    public TipoImagemEnum? TipoImagemId { get; set; }

    public string? UserId { get; set; }
}

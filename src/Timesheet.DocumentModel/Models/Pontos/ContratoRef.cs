﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Timesheet.Models.Pontos;

[Owned]
public class ContratoRef
{
    [Required]
    [MinLength(3)]
    [MaxLength(35)]
    [DisplayName("Nome")]
    public string? Nome { get; set; }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuPonto.Models
{
    [Owned]
    public class PerfilEmpresa
    {
        [Required]
        [MaxLength(14)]
        public string? Cnpj { get; set; }

        [Required]
        [MaxLength(35)]
        public string? Nome { get; set; }
        
        [MaxLength(12)]
        public string? InscricaoEstadual { get; set; }

        [MaxLength(35)]
        public string? Endereco { get; set; }

    }
}

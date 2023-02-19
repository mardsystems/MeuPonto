using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.DocumentModel;
using System.Text;
using System.Threading.Tasks;

namespace MeuPonto.Modules.Perfis
{
    public class Perfil : Entity
    {
        [Required]
        [MaxLength(30)]
        public string? Matricula { get; set; }

        [Required]
        [MaxLength(35)]
        public string? Nome { get; set; }

        [MaxLength(12)]
        public string? Pis { get; set; }
        
        public PerfilEmpresa Empresa { get; set; } = default!;

        //public virtual ICollection<Ponto> Pontos { get; set; }

        public Perfil()
        {
            Empresa = new PerfilEmpresa();

            //Pontos = new HashSet<Ponto>();
        }
    }

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

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.TableModel;
using System.Text;
using System.Threading.Tasks;

namespace MeuPonto.Models
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

        public virtual ICollection<Ponto> Pontos { get; set; }

        public Perfil()
        {
            Empresa = new PerfilEmpresa();

            Pontos = new HashSet<Ponto>();
        }
    }
}

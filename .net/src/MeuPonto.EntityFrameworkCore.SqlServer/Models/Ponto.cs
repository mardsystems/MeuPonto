using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.TableModel;
using System.Text;
using System.Threading.Tasks;

namespace MeuPonto.Models
{
    public class Ponto : Entity
    {
        [Required]
        public DateTime? Data { get; set; }

        [Required]
        public int? PerfilId { get; set; }

        public virtual Perfil? Perfil { get; set; }

        [MaxLength(255)]
        public string? Observacao { get; set; }
    }
}

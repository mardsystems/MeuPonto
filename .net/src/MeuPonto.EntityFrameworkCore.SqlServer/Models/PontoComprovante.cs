using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.TableModel;
using System.Text;
using System.Threading.Tasks;

namespace MeuPonto.Models
{
    public class PontoComprovante : Entity
    {
        [Required]
        public int? PontoId { get; set; }

        public virtual Ponto? Ponto { get; set; }

        [MaxLength(16)]
        public string? Numero { get; set; }

        [Required]
        public byte[]? Imagem { get; set; }

        [Required]
        public int? ImagemTipoId { get; set; }

        public virtual PontoComprovanteImagemTipo? ImagemTipo { get; set; }

        public PontoComprovante()
        {
            Imagem = new byte[0];
        }
    }
}

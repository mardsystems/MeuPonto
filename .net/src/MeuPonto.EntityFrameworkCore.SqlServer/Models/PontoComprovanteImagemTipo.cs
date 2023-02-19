using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuPonto.Models
{
    public class PontoComprovanteImagemTipo
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public string Nome { get; set; } = default!;
    }
}

using MeuPonto.Modules.Perfis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.DocumentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MeuPonto.Modules.Pontos
{
    public class Ponto : Entity
    {
        [Required]
        public DateTime? Data { get; set; }

        [Required]
        public Guid? PerfilId { get; set; }

        public virtual PerfilRef? Perfil { get; set; }

        [MaxLength(255)]
        public string? Observacao { get; set; }
    }

    public class PerfilRef
    {
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Matricula { get; set; }
    }

    public class PontoComprovante : Entity
    {
        [Required]
        public Guid? PontoId { get; set; }

        public virtual PontoRef? Ponto { get; set; } = default!;

        [MaxLength(16)]
        public string? Numero { get; set; }

        [Required]
        public byte[]? Imagem { get; set; }

        [Required]
        public int? ImagemTipoId { get; set; }

        public virtual PontoComprovanteImagemTipo? ImagemTipo { get; set; } = default!;
    }

    public class PontoRef
    {
        public Guid? Id { get; set; }

        [Required]
        public DateTime? Data { get; set; }
    }

    public class PontoComprovanteImagemTipo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Nome { get; set; } = default!;
    }

    public enum PontoComprovanteImagemTipoEnum
    {
        Original = 1,

        Tratada = 2
    }
}

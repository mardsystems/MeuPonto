using MeuPonto.Modules.Perfis;
using Microsoft.EntityFrameworkCore;
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

        public PontoPerfilRef? Perfil { get; set; }

        [MaxLength(255)]
        public string? Observacao { get; set; }
    }

    [Owned]
    public class PontoPerfilRef
    {
        [Required]
        [MaxLength(30)]
        public string? Matricula { get; set; }
    }

    public class PontoComprovante : Entity
    {
        [Required]
        public Guid? PontoId { get; set; }

        public PontoRef? Ponto { get; set; }

        [MaxLength(16)]
        public string? Numero { get; set; }

        //[Required]
        public byte[]? Imagem { get; set; }

        [Required]
        public int? ImagemTipoId { get; set; }

        public PontoComprovanteImagemTipoRef? ImagemTipo { get; set; }
    }

    [Owned]
    public class PontoRef
    {
        [Required]
        public DateTime? Data { get; set; }
    }

    [Owned]
    public class PontoComprovanteImagemTipoRef
    {
        [MaxLength(255)]
        public string Nome { get; set; } = default!;
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

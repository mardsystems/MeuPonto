using MeuPonto.Modules.Perfis;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Modules.Pontos.Folhas;

public class Folha : GlobalTableEntity
{
    [Required]
    [DisplayName("Perfil")]
    public Guid? PerfilId { get; set; }

    [DisplayName("Perfil")]
    public Perfil? Perfil { get; set; }

    [Required]
    [DisplayName("Competência")]
    [DisplayFormat(DataFormatString = "{0:y}")]
    public DateTime? Competencia { get; set; }

    [Required]
    [DisplayName("Status")]
    public StatusEnum? StatusId { get; set; }

    [MinLength(3)]
    [MaxLength(255)]
    [DisplayName("Observação")]
    public string? Observacao { get; set; }

    [DisplayName("Apuração Mensal")]
    public ApuracaoMensal ApuracaoMensal { get; set; }

    public string? UserId { get; set; }

    public Folha()
    {
        ApuracaoMensal = new ApuracaoMensal();
    }
}

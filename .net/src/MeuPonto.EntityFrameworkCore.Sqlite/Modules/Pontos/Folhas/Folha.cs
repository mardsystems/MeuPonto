using MeuPonto.Modules.Perfis;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Modules.Pontos.Folhas;

public class Folha : GlobalTableEntity, Concepts.Folha
{
    [Required]
    [DisplayName("Perfil")]
    public Guid? PerfilId { get; set; }

    [DisplayName("Perfil")]
    public Perfil? Perfil { get; set; }
    Concepts.Perfil? Concepts.Folha.Perfil => Perfil;

    [Required]
    [DisplayName("Competência")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM (MMMM)}")]
    public DateTime? Competencia { get; set; }

    [Required]
    [DisplayName("Status")]
    public StatusEnum? Status { get; set; }
    Concepts.Status? Concepts.Folha.Status => Status == null ? null : new Status(Status.Value);

    [MinLength(3)]
    [MaxLength(255)]
    [DisplayName("Observação")]
    public string? Observacao { get; set; }

    [DisplayName("Apuração Mensal")]
    public ApuracaoMensal ApuracaoMensal { get; set; }
    Concepts.ApuracaoMensal Concepts.Folha.ApuracaoMensal => throw new NotImplementedException();

    public Folha()
    {
        ApuracaoMensal = new ApuracaoMensal();
    }
}

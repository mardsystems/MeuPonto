using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Modules.Pontos.Folhas;

public class Folha : DocumentEntity, Folha_
{
    [Required]
    [DisplayName("Perfil")]
    public Guid? PerfilId { get; set; }

    [DisplayName("Perfil")]
    public PerfilRef? Perfil { get; set; }
    Perfil_? Folha_.Perfil => Perfil;

    [Required]
    [DisplayName("Competência")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM (MMMM)}")]
    public DateTime? Competencia { get; set; }

    [Required]
    [DisplayName("Status")]
    public StatusEnum? Status { get; set; }
    Status_? Folha_.Status => Status == null ? null : new Status(Status.Value);

    [MinLength(3)]
    [MaxLength(255)]
    [DisplayName("Observação")]
    public string? Observacao { get; set; }

    [DisplayName("Apuração Mensal")]
    public ApuracaoMensal ApuracaoMensal { get; set; }
    ApuracaoMensal_ Folha_.ApuracaoMensal => ApuracaoMensal;

    public Folha()
    {
        ApuracaoMensal = new ApuracaoMensal();
    }
}

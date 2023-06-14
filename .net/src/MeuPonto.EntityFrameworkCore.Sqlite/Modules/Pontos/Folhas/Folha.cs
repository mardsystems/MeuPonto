using MeuPonto.Modules.Perfis;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Modules.Pontos.Folhas;

public class Folha : GlobalTableEntity, Folha_
{
    [Required]
    [DisplayName("Perfil")]
    public Guid? PerfilId { get; set; }

    [DisplayName("Perfil")]
    public Perfil? Perfil { get; set; }
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
    ApuracaoMensal_ Folha_.ApuracaoMensal => throw new NotImplementedException();

    public Folha()
    {
        ApuracaoMensal = new ApuracaoMensal();
    }
}

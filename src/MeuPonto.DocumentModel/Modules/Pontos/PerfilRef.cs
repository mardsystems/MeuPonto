using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MeuPonto.Modules.Pontos;

[Owned]
public class PerfilRef : Concepts.Perfil
{
    [Required]
    [MaxLength(30)]
    [DisplayName("Nome")]
    public string? Nome { get; set; }

    bool Concepts.Perfil.Ativo => throw new NotImplementedException();

    Concepts.Contrato? Concepts.Perfil.IdentificaVinculo()
    {
        throw new NotImplementedException();
    }
}

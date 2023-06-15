using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MeuPonto.Concepts;

namespace MeuPonto.Modules.Pontos;

[Owned]
public class PerfilRef : Concepts.Perfil
{
    [Required]
    [MaxLength(30)]
    [DisplayName("Nome")]
    public string? Nome { get; set; }

    bool Concepts.Perfil.Ativo => throw new NotImplementedException();

    string? Concepts.Perfil.Matricula => throw new NotImplementedException();

    JornadaTrabalhoSemanal Perfil.Preve()
    {
        throw new NotImplementedException();
    }

    Empresa? Perfil.Vincula()
    {
        throw new NotImplementedException();
    }
}

using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Models.Pontos;

public enum PausaEnum
{
    [Display(Name = "Almoço")]
    Almoco = 1,

    [Display(Name = "Café/Lanche")]
    CafeLanche = 2,

    [Display(Name = "Banheiro")]
    Banheiro = 3,

    [Display(Name = "Conversa/Reunião")]
    ConversaReuniao = 4,

    [Display(Name = "Telefonema")]
    Telefonema = 5,

    [Display(Name = "Genérica")]
    Generica = 6
}

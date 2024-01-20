using Timesheet.Models.Contratos;
using Timesheet.Models.Pontos;

namespace Timesheet.Models.Folhas;

public static class ContratoFacade
{
    public static void QualificaFolha(this Contrato contrato, Folha folha)
    {
        folha.Contrato = new ContratoRef
        {
            Nome = contrato.Nome
        };

        folha.ContratoId = contrato.Id;
    }
}

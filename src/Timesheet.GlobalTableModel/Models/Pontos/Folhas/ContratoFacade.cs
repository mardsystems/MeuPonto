using Timesheet.Models.Contratos;

namespace Timesheet.Models.Pontos.Folhas;

public static class ContratoFacade
{
    public static void QualificaFolha(this Contrato contrato, Folha folha)
    {
        folha.Contrato = contrato;

        folha.ContratoId = contrato.Id;
    }
}

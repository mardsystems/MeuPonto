using Timesheet.Models.Empregadores;

namespace Timesheet.Models.Contratos;

public static class ContratoFacade
{
    public static void VinculaEmpregador(this Contrato contrato, Empregador empregador)
    {
        contrato.Empregador = empregador;

        contrato.EmpregadorId = empregador.Id;
    }
}

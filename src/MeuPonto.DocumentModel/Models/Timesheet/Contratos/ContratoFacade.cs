using MeuPonto.Models.Timesheet.Empregadores;

namespace MeuPonto.Models.Timesheet.Contratos;

public static class ContratoFacade
{
    public static void VinculaEmpregador(this Contrato contrato, Empregador empregador)
    {
        contrato.Empregador = new EmpregadorRef
        {
            Nome = empregador.Nome
        };

        contrato.EmpregadorId = empregador.Id;
    }
}

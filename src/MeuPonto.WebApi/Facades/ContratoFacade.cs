using MeuPonto.Models;

namespace MeuPonto.Facades;

public static class ContratoFacade
{
    public static void VinculaEmpregador(this Contrato contrato, Empregador empregador)
    {
        contrato.Empregador = empregador;

        contrato.EmpregadorId = empregador.Id;
    }

    public static void QualificaPonto(this Contrato contrato, Ponto ponto)
    {
        ponto.Contrato = contrato;

        ponto.ContratoId = contrato.Id;
    }
    
    public static void QualificaFolha(this Contrato contrato, Folha folha)
    {
        folha.Contrato = contrato;

        folha.ContratoId = contrato.Id;
    }
}


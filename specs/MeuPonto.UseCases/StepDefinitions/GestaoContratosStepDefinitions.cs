using System;
using Reqnroll;

namespace MeuPonto.StepDefinitions
{
    [Binding]
    public class GestaoContratosStepDefinitions
    {
        [When("o trabalhador solicitar a abertura de um contrato")]
        public void WhenOTrabalhadorSolicitarAAberturaDeUmContrato()
        {
            throw new PendingStepException();
        }

        [Then("o sistema deverá apresentar um contrato novo")]
        public void ThenOSistemaDeveraApresentarUmContratoNovo()
        {
            throw new PendingStepException();
        }

        [Then("o contrato deverá ser ativo")]
        public void ThenOContratoDeveraSerAtivo()
        {
            throw new PendingStepException();
        }

        [Then("o contrato deverá prever a seguinte jornada de trabalho semanal:")]
        public void ThenOContratoDeveraPreverASeguinteJornadaDeTrabalhoSemanal(DataTable dataTable)
        {
            throw new PendingStepException();
        }

        [When("o trabalhador abrir o contrato como:")]
        public void WhenOTrabalhadorAbrirOContratoComo(DataTable dataTable)
        {
            throw new PendingStepException();
        }

        [Then("o sistema deverá registrar o contrato como esperado")]
        public void ThenOSistemaDeveraRegistrarOContratoComoEsperado()
        {
            throw new PendingStepException();
        }
    }
}

using MeuPonto.Support;

namespace MeuPonto.Drivers;

public class GestaoContratosDriver
{
    private readonly WebApiContext _webApiContext;
    public ActionAttempt<Contrato, Contrato> CriaContrato { get; }

    public GestaoContratosDriver(WebApiContext webApiContext, ActionAttemptFactory actionAttemptFactory)
    {
        _webApiContext = webApiContext;

        CriaContrato = actionAttemptFactory.CreateWithStatusCheck<Contrato, Contrato>(
            nameof(CriaContrato),
            contrato => webApiContext.ExecutePost<Contrato>("/api/Contratos", contrato),
            System.Net.HttpStatusCode.Created);
    }

    public void CriarContrato(Contrato contrato)
    {
        CriaContrato.Perform(contrato);
    }

    public Contrato DetalharContrato(string nomeContrato)
    {
        int contratoId = 0;

        var contrato = _webApiContext.ExecuteGet<Contrato>($"/api/Contratos/{contratoId}");

        return contrato;
    }

    public void EditarContrato(string nomeContrato, Contrato contratoCadastrado)
    {
        throw new NotImplementedException();
    }

    public void ExcluirContrato(string nomeContrato)
    {
        throw new NotImplementedException();
    }
}

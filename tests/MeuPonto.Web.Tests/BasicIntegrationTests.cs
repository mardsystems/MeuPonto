namespace MeuPonto;

public class BasicIntegrationTests
    : IClassFixture<MeuPontoWebFactory<Program>>
{
    private readonly MeuPontoWebFactory<Program> _factory;

    public BasicIntegrationTests(MeuPontoWebFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("/")]
    [InlineData("/Termos")]
    [InlineData("/Privacidade")]
    [InlineData("/Sobre")]
    [InlineData("/Configuracoes")]
    [InlineData("/Contratos")]
    [InlineData("/Contratos/Criar")]
    [InlineData("/Contratos/Empregadores")]
    [InlineData("/Contratos/Empregadores/Criar")]
    [InlineData("/Pontos")]
    [InlineData("/Pontos/Criar")]
    [InlineData("/Pontos/Marcar")]
    [InlineData("/Folhas")]
    [InlineData("/Folhas/Criar")]
    [InlineData("/Folhas/Abrir")]
    [InlineData("/Pontos/Comprovantes")]
    [InlineData("/Pontos/Comprovantes/Criar")]
    [InlineData("/Pontos/Comprovantes/Guardar")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        //var userId = Guid.Parse("d2fc8313-9bdc-455c-bf29-ccf709a2a692");

        //var transaction = new TransactionContext(userId);

        //var trabalhador = TrabalhadorFactory.CriaTrabalhador(transaction);

        //Trabalhador.Default = trabalhador;

        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        try
        {
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
        catch (Exception ex)
        {
            var message = await response.Content.ReadAsStringAsync();

            throw new Exception(message, ex);
        }
    }
}
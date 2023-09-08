using MeuPonto.Modules.Trabalhadores;

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
    [InlineData("/Empregadores")]
    [InlineData("/Empregadores/Criar")]
    [InlineData("/Perfis")]
    [InlineData("/Perfis/Criar")]
    [InlineData("/Pontos")]
    [InlineData("/Pontos/Criar")]
    [InlineData("/Pontos/Marcar")]
    [InlineData("/Pontos/Folhas")]
    [InlineData("/Pontos/Folhas/Criar")]
    [InlineData("/Pontos/Folhas/Abrir")]
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
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("text/html; charset=utf-8",
            response.Content.Headers.ContentType.ToString());
    }
}
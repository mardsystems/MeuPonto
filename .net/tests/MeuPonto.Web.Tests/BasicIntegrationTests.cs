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
    [InlineData("/Index")]
    [InlineData("/Sobre")]
    [InlineData("/Privacidade")]
    [InlineData("/Perfis")]
    [InlineData("/Pontos")]
    [InlineData("/Pontos/Folhas")]
    [InlineData("/Pontos/Comprovantes")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
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
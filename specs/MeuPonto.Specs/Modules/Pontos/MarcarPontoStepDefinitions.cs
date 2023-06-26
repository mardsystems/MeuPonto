using MeuPonto.Data;
using MeuPonto.Modules.Perfis;

namespace MeuPonto.Modules.Pontos;

[Binding]
public class MarcarPontoStepDefinitions
{
    private readonly ScenarioContext _scenario;

    private readonly RegistroPontosContext _registroPontos;

    private readonly RegistroPontosInterface _registroPontosInterface;

    private readonly CadastroPerfisContext _cadastroPerfis;

    private readonly MeuPontoDbContext _db;

    public MarcarPontoStepDefinitions(
        ScenarioContext scenario,
        RegistroPontosContext registroPontos,
        RegistroPontosInterface registroPontosInterface,
        CadastroPerfisContext cadastroPerfis,
        MeuPontoDbContext db)
    {
        _scenario = scenario;

        _registroPontos = registroPontos;

        _registroPontosInterface = registroPontosInterface;

        _cadastroPerfis = cadastroPerfis;

        _db = db;
    }

    [Given(@"que a data/hora do relógio é '([^']*)'")]
    public void GivenQueADataHoraDoRelogioE(DateTime dataHora)
    {
        _registroPontos.DataHora = dataHora;
    }

    [Given(@"que o trabalhador qualifica o ponto com o perfil '([^']*)'")]
    public void GivenQueOTrabalhadorQualificaOPontoComOPerfil(string nome)
    {
        var perfil = _db.Perfis.FirstOrDefault(x => x.Nome == nome);

        perfil.QualificaPonto(_registroPontos.Ponto);
    }

    [When(@"o trabalhador marcar o ponto")]
    public void WhenOTrabalhadorMarcarOPonto()
    {
        if (_registroPontos.Ponto.EstaSemQualificacao())
        {
            var perfil = _db.Perfis.FirstOrDefault();

            if (perfil == default)
            {
                perfil = _cadastroPerfis.Perfil;

                _db.Perfis.Add(perfil);
                _db.SaveChanges();
            }

            perfil.QualificaPonto(_registroPontos.Ponto);
        }

        var pontoMarcado = _registroPontosInterface.MarcarPonto(_registroPontos.Ponto);

        _registroPontos.Define(pontoMarcado);
    }

    [Then(@"o ponto deverá ser marcado")]
    public void ThenOPontoDeveraSerMarcado()
    {
        _registroPontos.PontoRegistrado.Should().NotBeNull();
    }
}

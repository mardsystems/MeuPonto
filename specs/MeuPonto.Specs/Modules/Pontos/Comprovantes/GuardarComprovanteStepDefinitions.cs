using MeuPonto.Data;
using MeuPonto.Modules.Perfis;

namespace MeuPonto.Modules.Pontos.Comprovantes;

[Binding]
public class GuardarComprovanteStepDefinitions
{
    private readonly ScenarioContext _scenario;

    private readonly BackupComprovantesContext _backupComprovantes;

    private readonly BackupComprovantesInterface _backupComprovantesInterface;

    private readonly CadastroPerfisContext _cadastroPerfis;

    private readonly MeuPontoDbContext _db;

    public GuardarComprovanteStepDefinitions(
        ScenarioContext scenario,
        BackupComprovantesContext backupComprovantes,
        BackupComprovantesInterface backupComprovantesInterface,
        CadastroPerfisContext cadastroPerfis,
        MeuPontoDbContext db)
    {
        _scenario = scenario;

        _backupComprovantes = backupComprovantes;

        _backupComprovantesInterface = backupComprovantesInterface;

        _cadastroPerfis = cadastroPerfis;

        _db = db;
    }

    [Given(@"que o trabalhador tem um comprovante de ponto com a data '([^']*)'")]
    public async Task GivenQueOTrabalhadorTemUmComprovanteDePontoComAData(DateTime data)
    {
        _db.Perfis.Add(_cadastroPerfis.Perfil);
        await _db.SaveChangesAsync();

        var file = new FileStream("C:\\temp\\20230222_104351.jpg", FileMode.Open);

        _backupComprovantes.Define(file);

        _backupComprovantes.Comprovante.TipoImagemId = TipoImagemEnum.Original;

        _cadastroPerfis.Perfil.QualificaPonto(_backupComprovantes.Ponto);
        _backupComprovantes.Ponto.DataHora = new DateTime(2023, 02, 17, 17, 07, 0);
        _backupComprovantes.Ponto.MomentoId = MomentoEnum.Saida;
    }

    [Given(@"que o trabalhador tem um comprovante de ponto guardado com a data '([^']*)'")]
    public async Task GivenQueOTrabalhadorTemUmComprovanteDePontoGuardadoComAData(DateTime data)
    {
        _db.Perfis.Add(_cadastroPerfis.Perfil);
        await _db.SaveChangesAsync();

        var transaction = new TransactionContext("Test user");

        var ponto = PontoFactory.CriaPonto(transaction);

        _cadastroPerfis.Perfil.QualificaPonto(ponto);
        
        ponto.DataHora = data;
        ponto.MomentoId = MomentoEnum.Entrada;

        var comprovante = ComprovanteFactory.CriaComprovante(transaction);

        comprovante.ComprovaPonto(ponto);

        _db.Comprovantes.Add(comprovante);
        await _db.SaveChangesAsync();
    }

    [Given(@"que o trabalhador escaneou um comprovante de ponto com a data '([^']*)'")]
    public async Task GivenQueOTrabalhadorEscaneouUmComprovanteDePontoComAData(string p0)
    {
        _db.Perfis.Add(_cadastroPerfis.Perfil);
        await _db.SaveChangesAsync();

        var file = new FileStream("C:\\temp\\20230222_104351.jpg", FileMode.Open);

        _backupComprovantes.Define(file);

        _backupComprovantes.Comprovante.TipoImagemId = TipoImagemEnum.Original;

        _cadastroPerfis.Perfil.QualificaPonto(_backupComprovantes.Ponto);
        _backupComprovantes.Ponto.DataHora = new DateTime(2023, 02, 17, 17, 07, 0);
        _backupComprovantes.Ponto.MomentoId = MomentoEnum.Saida;
    }

    [When(@"o trabalhador escanear o comprovante de ponto")]
    public void WhenOTrabalhadorEscanearOComprovanteDePonto()
    {
        var comprovante = _backupComprovantesInterface.EscanearComprovante(
            _backupComprovantes.Imagem,
            _backupComprovantes.Comprovante,
            _backupComprovantes.Ponto);

        _backupComprovantes.Define(comprovante);
    }

    [When(@"o trabalhador guardar o comprovante de ponto")]
    public void WhenOTrabalhadorGuardarOComprovanteDePonto()
    {
        var comprovanteGuardado = _backupComprovantesInterface.GuardarComprovante(
            _backupComprovantes.Imagem,
            _backupComprovantes.Comprovante,
            _backupComprovantes.Ponto);

        _backupComprovantes.Define(comprovanteGuardado);
    }

    [Then(@"o comprovante de ponto deverá ser guardado")]
    public void ThenOComprovanteDePontoDeveraSerGuardado()
    {
        _backupComprovantes.ComprovanteGuardado.Should().NotBeNull();
    }

    [Then(@"a data do ponto do comprovante deverá ser '([^']*)'")]
    public void ThenADataDoPontoDoComprovanteDeveraSer(DateTime data)
    {
        _backupComprovantes.ComprovanteGuardado.Ponto.DataHora.Should().Be(data);
    }
}

using MeuPonto.Data;
using MeuPonto.Drivers;
using MeuPonto.Support;
using System.Transactions;
using Timesheet.Models.Pontos;
using Timesheet.Models.Pontos.Comprovantes;

namespace MeuPonto.StepDefinitions;

[Binding]
public class BackupComprovantesStepDefinitions
{
    private readonly ScenarioContext _scenario;

    private readonly BackupComprovantesContext _backupComprovantes;

    private readonly BackupComprovantesDriver _backupComprovantesDriver;

    private readonly GestaoContratosContext _gestaoContratos;

    private readonly MeuPontoDbContext _db;

    public BackupComprovantesStepDefinitions(
        ScenarioContext scenario,
        BackupComprovantesContext backupComprovantes,
        BackupComprovantesDriver backupComprovantesDriver,
        GestaoContratosContext gestaoContratos,
        MeuPontoDbContext db)
    {
        _scenario = scenario;

        _backupComprovantes = backupComprovantes;

        _backupComprovantesDriver = backupComprovantesDriver;

        _gestaoContratos = gestaoContratos;

        _db = db;
    }

    [Given(@"que o trabalhador tem um comprovante de ponto com a data '([^']*)'")]
    public async Task GivenQueOTrabalhadorTemUmComprovanteDePontoComAData(DateTime data)
    {
        _db.Contratos.Add(_gestaoContratos.Contrato);
        await _db.SaveChangesAsync();

        var basePath = Directory.GetCurrentDirectory();

        var path = Directory.GetParent(
            Directory.GetParent(
                Directory.GetParent(
                    Directory.GetParent(
                        Directory.GetParent(basePath).FullName
                    ).FullName
                ).FullName
            ).FullName
        ).FullName;

        var filePath = Path.Combine(basePath, path, "img", "20230222_104351.jpg");

        var file = new FileStream(filePath, FileMode.Open);

        _backupComprovantes.Define(file);

        _backupComprovantes.Comprovante.TipoImagemId = TipoImagemEnum.Original;

        _gestaoContratos.Contrato.QualificaPonto(_backupComprovantes.Ponto);
        _backupComprovantes.Ponto.DataHora = new DateTime(2023, 02, 17, 17, 07, 0);
        _backupComprovantes.Ponto.MomentoId = MomentoEnum.Saida;
    }

    [Given(@"que o trabalhador tem um comprovante de ponto guardado com a data '([^']*)'")]
    public async Task GivenQueOTrabalhadorTemUmComprovanteDePontoGuardadoComAData(DateTime data)
    {
        _db.Contratos.Add(_gestaoContratos.Contrato);
        await _db.SaveChangesAsync();

        var userId = Guid.NewGuid();

        var transaction = new TransactionContext(userId.ToString());

        var ponto = PontoFactory.CriaPonto(transaction);

        _gestaoContratos.Contrato.QualificaPonto(ponto);

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
        _db.Contratos.Add(_gestaoContratos.Contrato);
        await _db.SaveChangesAsync();

        var basePath = Directory.GetCurrentDirectory();

        var path = Directory.GetParent(
            Directory.GetParent(
                Directory.GetParent(
                    Directory.GetParent(
                        Directory.GetParent(basePath).FullName
                    ).FullName
                ).FullName
            ).FullName
        ).FullName;

        var filePath = Path.Combine(basePath, path, "img", "20230222_104351.jpg");

        var file = new FileStream(filePath, FileMode.Open);

        _backupComprovantes.Define(file);

        _backupComprovantes.Comprovante.TipoImagemId = TipoImagemEnum.Original;

        _gestaoContratos.Contrato.QualificaPonto(_backupComprovantes.Ponto);
        _backupComprovantes.Ponto.DataHora = new DateTime(2023, 02, 17, 17, 07, 0);
        _backupComprovantes.Ponto.MomentoId = MomentoEnum.Saida;
    }

    #region Guardar Comprovante

    [When(@"o trabalhador escanear o comprovante de ponto")]
    public void WhenOTrabalhadorEscanearOComprovanteDePonto()
    {
        var comprovante = _backupComprovantesDriver.EscanearComprovante(
            _backupComprovantes.Imagem,
            _backupComprovantes.Comprovante,
            _backupComprovantes.Ponto);

        _backupComprovantes.Define(comprovante);
    }

    [When(@"o trabalhador guardar o comprovante de ponto")]
    public void WhenOTrabalhadorGuardarOComprovanteDePonto()
    {
        var comprovanteGuardado = _backupComprovantesDriver.GuardarComprovante(
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

    #endregion

    [Then(@"a data do ponto do comprovante deverá ser '([^']*)'")]
    public void ThenADataDoPontoDoComprovanteDeveraSer(DateTime data)
    {
        _backupComprovantes.ComprovanteGuardado.Ponto.DataHora.Should().Be(data);
    }
}

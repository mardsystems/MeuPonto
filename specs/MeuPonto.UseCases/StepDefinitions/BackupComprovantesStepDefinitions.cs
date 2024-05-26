using MeuPonto.Data;
using MeuPonto.Drivers;
using MeuPonto.Support;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using MeuPonto.Features.BackupComprovantes;
using MeuPonto.Features.RegistroPontos;
using MeuPonto.Models.Pontos;

namespace MeuPonto.StepDefinitions;

[Binding]
public class BackupComprovantesStepDefinitions
{
    private readonly ScenarioContext _scenario;
    //private readonly RegistroPontosContext _registroPontos;
    //private readonly RegistroPontosDriver _registroPontosInterface;
    private readonly BackupComprovantesContext _backupComprovantes;
    private readonly BackupComprovantesDriver _backupComprovantesInterface;
    private readonly GestaoContratosContext _gestaoContratos;
    private readonly MeuPontoDbContext _db;

    public BackupComprovantesStepDefinitions(
        ScenarioContext scenario,
        //RegistroPontosContext registroPontos,
        //RegistroPontosDriver registroPontosInterface,
        BackupComprovantesContext backupComprovantes,
        BackupComprovantesDriver backupComprovantesInterface,
        GestaoContratosContext gestaoContratos,
        MeuPontoDbContext db)
    {
        _scenario = scenario;
        //_registroPontos = registroPontos;
        //_registroPontosInterface = registroPontosInterface;
        _backupComprovantes = backupComprovantes;
        _backupComprovantesInterface = backupComprovantesInterface;
        _gestaoContratos = gestaoContratos;
        _db = db;
    }

    [When(@"o trabalhador solicitar o backup de um comprovante")]
    public void WhenOTrabalhadorSolicitarOBackupDeUmComprovante()
    {
        var comprovante = _backupComprovantesInterface.SolicitarBackupComprovante();

        _backupComprovantes.Contextualizar(comprovante);
    }

    [Then(@"o sistema deverá apresentar as opções de backup de um comprovante")]
    public void ThenOSistemaDeveraApresentarAsOpcoesDeBackupDeUmComprovante()
    {
        
    }
    
    [Then(@"um comprovante deverá ser criado")]
    public void ThenUmComprovanteDeveraSerCriado()
    {
        _backupComprovantes.Comprovante.Should().NotBeNull();
    }

    [When(@"o trabalhador escanear a imagem do comprovante com a data '([^']*)'")]
    public void WhenOTrabalhadorEscanearAImagemDoComprovanteComAData(DateTime data)
    {
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

        _backupComprovantes.Contextualizar(file);

        _backupComprovantes.Comprovante.TipoImagemId = TipoImagemEnum.Original;

        _backupComprovantesInterface.EscanearComprovante(
            _backupComprovantes.Imagem,
            _backupComprovantes.Comprovante,
            _backupComprovantes.Ponto);

        var comprovanteGuardado = _db.Comprovantes
            .Include(x => x.Ponto)
            .FirstOrDefault(x => x.PontoId == _backupComprovantes.Ponto.Id);

        _backupComprovantes.Contextualizar(comprovanteGuardado);
    }

    [Given(@"que o trabalhador tem um comprovante de ponto com a data '([^']*)'")]
    public void GivenQueOTrabalhadorTemUmComprovanteDePontoComAData(DateTime data)
    {
        _db.Contratos.Add(_gestaoContratos.Contrato);
        _db.SaveChanges();

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

        _backupComprovantes.Contextualizar(file);

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

        var ponto = RegistroPontosFacade.CriaPonto(transaction);

        _gestaoContratos.Contrato.QualificaPonto(ponto);

        ponto.DataHora = data;
        ponto.MomentoId = MomentoEnum.Entrada;

        var comprovante = BackupComprovantesFacade.CriaComprovante(transaction);

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

        _backupComprovantes.Contextualizar(file);

        _backupComprovantes.Comprovante.TipoImagemId = TipoImagemEnum.Original;

        _gestaoContratos.Contrato.QualificaPonto(_backupComprovantes.Ponto);
        _backupComprovantes.Ponto.DataHora = new DateTime(2023, 02, 17, 17, 07, 0);
        _backupComprovantes.Ponto.MomentoId = MomentoEnum.Saida;
    }

    #region Guardar Comprovante

    [When(@"o trabalhador escanear o comprovante de ponto")]
    public void WhenOTrabalhadorEscanearOComprovanteDePonto()
    {
        _backupComprovantesInterface.EscanearComprovante(
            _backupComprovantes.Imagem,
            _backupComprovantes.Comprovante,
            _backupComprovantes.Ponto);

        var comprovanteGuardado = _db.Comprovantes.FirstOrDefault(x => x.PontoId == _backupComprovantes.Ponto.Id);

        _backupComprovantes.Contextualizar(comprovanteGuardado);
    }

    [When(@"o trabalhador guardar o comprovante de ponto")]
    public void WhenOTrabalhadorGuardarOComprovanteDePonto()
    {
        var comprovanteGuardado = _backupComprovantesInterface.GuardarComprovante(
            _backupComprovantes.Imagem,
            _backupComprovantes.Comprovante,
            _backupComprovantes.Ponto);

        _backupComprovantes.Contextualizar(comprovanteGuardado);
    }

    [Then(@"o sistema deverá registrar o comprovante de ponto")]
    public void ThenOSistemaDeveraRegistrarOComprovanteDePonto()
    {
        _backupComprovantes.Comprovante.Should().NotBeNull();
    }

    #endregion

    [Then(@"o sistema deverá processar a imagem do comprovante")]
    public void ThenOSistemaDeveraProcessarAImagemDoComprovante()
    {
        
    }

    [Then(@"a data do ponto do comprovante deverá ser '([^']*)'")]
    public void ThenADataDoPontoDoComprovanteDeveraSer(DateTime data)
    {
        _backupComprovantes.Comprovante.Ponto.DataHora.Should().Be(data);
    }

    [Then(@"o comprovante '([^']*)' deverá ser associado ao ponto")]
    public void ThenOComprovanteDeveraSerAssociadoAoPonto(DateTime data)
    {
        _backupComprovantes.Comprovante.Ponto.Should().NotBeNull(); // TODO: inverter a associação!?
    }
}

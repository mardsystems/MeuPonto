using BoDi;
using MeuPonto.Modules.Perfis;
using MeuPonto.Modules.Pontos;
using MeuPonto.Modules.Pontos.Comprovantes;
using MeuPonto.Modules.Pontos.Folhas;

namespace MeuPonto.Hooks;

[Binding]
public class SeedHook
{
    public SeedHook(IObjectContainer objectContainer)
    {

    }

    [BeforeScenario]
    public void SetupTestUsers(
        CadastroPerfisContext cadastroPerfis,
        RegistroPontosContext registroPontos,
        BackupComprovantesContext backupComprovantes,
        GestaoFolhasContext gestaoFolhas)
    {
        var perfil = new Perfil
        {
            Nome = "Test user",
        };

        cadastroPerfis.Inicia(perfil);

        var ponto = new Ponto
        {
            MomentoId = MomentoEnum.Entrada,
            PausaId = null
        };

        registroPontos.Inicia(ponto);

        var comprovante = new Comprovante();

        backupComprovantes.Inicia(comprovante);

        backupComprovantes.Inicia(ponto);

        var hoje = DateTime.Today;

        var folhaNova = new Folha
        {
            Competencia = new DateTime(hoje.Year, hoje.Month, 1)
        };

        gestaoFolhas.Inicia(folhaNova);
    }
}

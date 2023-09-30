using OpenQA.Selenium;
using SpecFlow.Actions.WindowsAppDriver;

namespace MeuPonto.Modules.Perfis;

public class CadastroPerfisDriver
{
    private readonly AppDriver _driver;

    public CadastroPerfisDriver(AppDriver driver)
    {
        _driver = driver;
    }

    public void GoTo()
    {
        var current = _driver.Current;

        var perfisMenuItem = current.FindElementByAccessibilityId("perfisMenuItem");

        perfisMenuItem.Click();

        var cadastroPerfisMenuItem = current.FindElementByAccessibilityId("cadastroPerfisMenuItem");

        cadastroPerfisMenuItem.Click();
    }

    public void CriarPerfil(Perfil perfil)
    {
        GoTo();

        var windowName = _driver.Current.WindowHandles[0];

        _driver.Current.Close();

        _driver.Current.SwitchTo().Window(windowName);

        var novoPerfilButton = _driver.Current.FindElementByAccessibilityId("novoPerfilButton");

        novoPerfilButton.Click();

        //var nomeTextBox = _driver.Current.FindElementByAccessibilityId("nomeTextBox");

        //var a = nomeTextBox.FindElementsByXPath("//*");

        //foreach (var item in a)
        //{
        //    var teste = item.TagName;
        //}

        //nomeTextBox.SendKeys("Teste");


        //var registroMenuItem = _driver.Current.FindElementByAccessibilityId("registroMenuItem");

        //registroMenuItem.Click();

        //var dataGrid = _driver.Current.FindElementByClassName("DataGrid");

        //var rows = dataGrid.FindElementByClassName("DataGridRow");

        //rows.SendKeys("Teste");


        var perfilViewModelDataGrid = _driver.Current.FindElementByName("Grade de Perfis");

        var x = perfilViewModelDataGrid.FindElementsByClassName(typeof(Perfil).Name);

        x[x.Count - 2].Click();

        var y = x[x.Count - 2].FindElementsByXPath("//*");

        y[2].Click();

        y[2].Click();

        var z = x[x.Count - 2].FindElementByClassName("TextBox");

        z.SendKeys(perfil.Nome);

        z.SendKeys(Keys.Enter);

        //y[2].Click();

        //z = x[0].FindElementByClassName("TextBox");

        //z.SendKeys("Teste");



        //var nomeTextBox = _driver.Current.FindElementByAccessibilityId("nomeTextBox");

        //nomeTextBox.SendKeys("Teste");



        //foreach (var item in y)
        //{
        //    var teste = item.TagName;
        //}

        //perfilViewModelDataGrid.Click();

        //perfilViewModelDataGrid.SendKeys("F2");

        //perfilViewModelDataGrid.SendKeys("Teste");

        //var perfilGroupBox = _driver.Current.FindElementByAccessibilityId("perfilGroupBox");

        var semanaItemsControl = _driver.Current.FindElementByName("Semana");

        var textBoxList = semanaItemsControl.FindElementsByClassName("TextBox");

        var t = textBoxList.Count;

        var daysOfWeek = Enum.GetValues<DayOfWeek>();

        foreach (var dayOfWeek in daysOfWeek)
        {
            var jornadaTrabalhoDiaria = perfil.JornadaTrabalhoSemanalPrevista.Semana.SingleOrDefault(x => x.DiaSemana == dayOfWeek);

            var i = (int)dayOfWeek;

            var tempoTextBox = textBoxList[i].FindElementByClassName("TextBox");

            tempoTextBox.Clear();

            if (jornadaTrabalhoDiaria == default)
            {
                tempoTextBox.SendKeys("00:00");
            }
            else
            {
                tempoTextBox.SendKeys(jornadaTrabalhoDiaria.Tempo.Value.ToString("hh\\:mm"));
            }
        }

        var nomeTextBox = _driver.Current.FindElementByAccessibilityId("nomeTextBox");

        nomeTextBox.Click();

        var saveButton = _driver.Current.FindElementByAccessibilityId("saveButton");

        saveButton.Click();

        Thread.Sleep(500);
    }

    public Perfil DetalharPerfil(string nomePerfil)
    {
        throw new NotImplementedException();
    }

    public void EditarPerfil(string nomePerfil, Perfil perfilCadastrado)
    {
        throw new NotImplementedException();
    }

    public void ExcluirPerfil(string nomePerfil)
    {
        throw new NotImplementedException();
    }
}

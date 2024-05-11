using MeuPonto.Models.Contratos;
using OpenQA.Selenium;
using SpecFlow.Actions.WindowsAppDriver;

namespace MeuPonto.Drivers;

public class GestaoContratosDriver
{
    private readonly AppDriver _driver;

    public GestaoContratosDriver(AppDriver driver)
    {
        _driver = driver;
    }

    public void GoTo()
    {
        var current = _driver.Current;

        var contratosMenuItem = current.FindElementByAccessibilityId("contratosMenuItem");

        contratosMenuItem.Click();

        var gestaoContratosMenuItem = current.FindElementByAccessibilityId("gestaoContratosMenuItem");

        gestaoContratosMenuItem.Click();
    }

    public void CriarContrato(Contrato contrato)
    {
        GoTo();

        var windowName = _driver.Current.WindowHandles[0];

        _driver.Current.Close();

        _driver.Current.SwitchTo().Window(windowName);

        var novoContratoButton = _driver.Current.FindElementByAccessibilityId("novoContratoButton");

        novoContratoButton.Click();

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


        var contratoViewModelDataGrid = _driver.Current.FindElementByName("Grade de Contratos");

        var x = contratoViewModelDataGrid.FindElementsByClassName(typeof(Contrato).Name);

        x[x.Count - 2].Click();

        var y = x[x.Count - 2].FindElementsByXPath("//*");

        y[2].Click();

        y[2].Click();

        var z = x[x.Count - 2].FindElementByClassName("TextBox");

        z.SendKeys(contrato.Nome);

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

        //contratoViewModelDataGrid.Click();

        //contratoViewModelDataGrid.SendKeys("F2");

        //contratoViewModelDataGrid.SendKeys("Teste");

        //var contratoGroupBox = _driver.Current.FindElementByAccessibilityId("contratoGroupBox");

        var semanaItemsControl = _driver.Current.FindElementByName("Semana");

        var textBoxList = semanaItemsControl.FindElementsByClassName("TextBox");

        var t = textBoxList.Count;

        var daysOfWeek = Enum.GetValues<DayOfWeek>();

        foreach (var dayOfWeek in daysOfWeek)
        {
            var jornadaTrabalhoDiaria = contrato.JornadaTrabalhoSemanalPrevista.Semana.SingleOrDefault(x => x.DiaSemana == dayOfWeek);

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

    public Contrato DetalharContrato(string nomeContrato)
    {
        throw new NotImplementedException();
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

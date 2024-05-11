using MeuPonto.Models.Contratos;

namespace MeuPonto.Pages.Contratos;

public class ContratosSearchHandler : SearchHandler
{
    public IEnumerable<Contrato> Contratos { get; set; }
    public Type SelectedItemNavigationTarget { get; set; }

    protected override void OnQueryChanged(string oldValue, string newValue)
    {
        base.OnQueryChanged(oldValue, newValue);

        if (string.IsNullOrWhiteSpace(newValue))
        {
            ItemsSource = null;
        }
        else
        {
            ItemsSource = Contratos
                .Where(contrato => contrato.Nome.ToLower().Contains(newValue.ToLower()))
                .ToList();
        }
    }

    protected override async void OnItemSelected(object item)
    {
        base.OnItemSelected(item);

        // Let the animation complete
        await Task.Delay(1000);

        ShellNavigationState state = (App.Current.MainPage as Shell).CurrentState;
        // The following route works because route names are unique in this app.
        await Shell.Current.GoToAsync($"{GetNavigationTarget()}?nome={((Contrato)item).Nome}");
    }

    string GetNavigationTarget()
    {
        return "";
        //return (Shell.Current as AppShell).Routes.FirstOrDefault(route => route.Value.Equals(SelectedItemNavigationTarget)).Key;
    }
}

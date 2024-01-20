using Timesheet.Models.Contratos;

namespace MeuPonto.Pages.Empregadores;

public class EmpregadoresSearchHandler : SearchHandler
{
    public IEnumerable<Empregador> Empregadores { get; set; }
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
            ItemsSource = Empregadores
                .Where(empregador => empregador.Nome.ToLower().Contains(newValue.ToLower()))
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
        await Shell.Current.GoToAsync($"{GetNavigationTarget()}?nome={((Empregador)item).Nome}");
    }

    string GetNavigationTarget()
    {
        return "";
        //return (Shell.Current as AppShell).Routes.FirstOrDefault(route => route.Value.Equals(SelectedItemNavigationTarget)).Key;
    }
}

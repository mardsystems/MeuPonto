namespace MeuPonto.Helpers;

public static class TranslationHelper
{
    private static readonly Dictionary<DayOfWeek, string> _map = new()
    {
        { DayOfWeek.Sunday, "Domingo" },
        { DayOfWeek.Monday, "Segunda-feira" },
        { DayOfWeek.Tuesday, "Terça-feira" },
        { DayOfWeek.Wednesday, "Quarta-feira" },
        { DayOfWeek.Thursday, "Quinta-feira" },
        { DayOfWeek.Friday, "Sexta-feira" },
        { DayOfWeek.Saturday, "Sábado" }
    };

    public static string Translate(this DayOfWeek? dayOfWeek)
    {
        if (dayOfWeek.HasValue)
        {
            return dayOfWeek.Value.Translate();
        }

        return null;
    }

    public static string Translate(this DayOfWeek dayOfWeek)
    {
        return _map[dayOfWeek];
    }
}

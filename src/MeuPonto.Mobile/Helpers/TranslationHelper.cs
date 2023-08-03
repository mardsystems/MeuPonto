namespace MeuPonto.Helpers;

public static class TranslationHelper
{
    private static readonly Dictionary<DayOfWeek, string> _map = new()
    {
        { DayOfWeek.Sunday, "Domingo" },
        { DayOfWeek.Monday, "Segunda" },
        { DayOfWeek.Tuesday, "Terça" },
        { DayOfWeek.Wednesday, "Quarta" },
        { DayOfWeek.Thursday, "Quinta" },
        { DayOfWeek.Friday, "Sexta" },
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

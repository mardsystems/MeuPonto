namespace System;

public class TimeSpanFormatter : IFormatProvider, ICustomFormatter
{
    public object? GetFormat(Type? formatType)
    {
        if (formatType == typeof(ICustomFormatter))
            return this;
        else
            return null;
    }

    public string Format(string? format, object? arg, IFormatProvider? formatProvider)
    {
        if (!formatProvider.Equals(this)) return null;

        string output;

        if (format == "hhh\\:mm")
        {
            if (arg is TimeSpan timeSpan)
            {
                int totalHours = (Math.Abs(timeSpan.Days) * 24) + Math.Abs(timeSpan.Hours);

                output = $"{totalHours:00}:{Math.Abs(timeSpan.Minutes):00}";
            }
            else if (arg == null)
            {
                output = $"00:00";
            }
            else
            {
                output = String.Format(format, arg);
            }
        }
        else
        {
            output = String.Format(format, arg);
        }

        return output;
    }
}

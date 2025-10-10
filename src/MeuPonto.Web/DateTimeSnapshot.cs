namespace System;

public class DateTimeSnapshot
{
    private readonly Func<DateTime> _getDateTime;

    public DateTimeSnapshot(Func<DateTime> getDateTime)
    {
        _getDateTime = getDateTime;
    }

    public DateTime GetDateTime()
    {
        return _getDateTime();
    }

    public DateTime GetDateTimeUntilMinutes()
    {
        var datetime = _getDateTime();

        return new DateTime(datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, 0);
    }
}

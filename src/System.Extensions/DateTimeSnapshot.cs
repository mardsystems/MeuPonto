namespace System;

public class DateTimeSnapshot
{
    private readonly DateTime _dateTime;

    public DateTimeSnapshot(DateTime dateTime)
    {
        _dateTime = dateTime;
    }

    public DateTime GetDateTime()
    {
        return _dateTime;
    }

    public DateTime GetDateTimeUntilMinutes()
    {
        return new DateTime(_dateTime.Year, _dateTime.Month, _dateTime.Day, _dateTime.Hour, _dateTime.Minute, 0);
    }
}

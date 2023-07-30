namespace System;

public static class DateTimeExtensions
{
    public static int GetWeekNumber(this DateTime dateTime)
    {
        var firstYearDayDateTime = new DateTime(dateTime.Year, 1, 1);

        var totalDays = (dateTime - firstYearDayDateTime).TotalDays + 1;

        int integerWeekNumber = ((int)totalDays - (int)firstYearDayDateTime.DayOfWeek) / 7;

        int restDays = ((int)totalDays - (int)firstYearDayDateTime.DayOfWeek) % 7;

        int weekNumber;

        if (restDays > 0)
        {
            weekNumber = integerWeekNumber + 1;
        }
        else
        {
            weekNumber = integerWeekNumber;
        }

        return weekNumber;
    }
}

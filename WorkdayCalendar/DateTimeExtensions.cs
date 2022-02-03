namespace WorkdayCalendar;

public static class DateTimeExtensions
{
    public static bool IsWeekend(this DateTime date)
    {
        return date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
    }

    private static bool IsValidWorkday(this DateTime date, WorkdayCalendar calendar )
    {
        if (calendar.GetRecurringHolidays().Any(rh => rh.Day == date.Day && rh.Month == date.Month))
        {
            return false;
        }

        if (date.IsWeekend())
            return false;

        return !calendar.GetHolidays().Any(h => h.Month == date.Month && h.Day == date.Day && h.Year == date.Year);
    }
}
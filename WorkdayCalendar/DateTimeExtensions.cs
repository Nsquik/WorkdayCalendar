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

    public static DateTime AddValidWorkdays(this DateTime date, WorkdayCalendar calendar, int daysToAdd)
    {
        // daysToAdd can be a negative number
        int counter = Math.Abs((int) daysToAdd);
        
        while (counter > 0)
        {
            date = date.AddDays(daysToAdd / Math.Abs(daysToAdd));
            if (date.IsValidWorkday(calendar))
            {
                counter -= 1;
            }
        }

        return date;
    }
}
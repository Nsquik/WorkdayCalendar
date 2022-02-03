namespace WorkdayCalendar;

public static class DateTimeExtensions
{
    public static bool IsWeekend(this DateTime date)
    {
        return date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
    }

    private static bool IsValidWorkday(this DateTime date, WorkdayCalendarService calendar )
    {
        if (calendar.GetRecurringHolidays().Any(rh => rh.Day == date.Day && rh.Month == date.Month))
        {
            return false;
        }

        if (date.IsWeekend())
            return false;

        return !calendar.GetHolidays().Any(h => h.Month == date.Month && h.Day == date.Day && h.Year == date.Year);
    }
    
  

    public static DateTime AddValidWorkdays(this DateTime date, WorkdayCalendarService calendar, int daysToAdd, bool direction)
    {
        // daysToAdd can be a negative number
        int counter = Math.Abs((int) daysToAdd);

        date = NextValidWorkday(date, calendar, direction);
        
        
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
    
    private static DateTime NextValidWorkday(this DateTime startDate, WorkdayCalendarService calendar, bool direction)
    {
        bool isValid = startDate.IsValidWorkday(calendar);
        while (!isValid)
        {
            startDate = startDate.AddDays(direction ? 1 : -1);
            isValid = startDate.IsValidWorkday(calendar);
        }

        return startDate;
    }
    
    public static DateTime AddValidWorkhours(this DateTime startDate, WorkdayCalendarService calendar, WorkdayHours workdayHours, bool canAddDirectly, bool direction, double hoursToAdd)
    {
        startDate = startDate.NextValidWorkday(calendar, direction);
        
        if (canAddDirectly)
        {
            return startDate.AddHours(hoursToAdd);
        }
        startDate = startDate.NextValidWorkday(calendar, direction);
        var test = startDate.AddHours(hoursToAdd);
        var hrs = hoursToAdd;
        if (!workdayHours.CheckIfWorkingHour(test))
        {
            hrs = workdayHours.HoursInWeekday - hrs;
            test = workdayHours.NextWorkday(test, direction);
            test = AddValidWorkhours(test, calendar, workdayHours, canAddDirectly, direction, hrs);
            return test;
        };
        return startDate.AddHours(hrs);
    }

}
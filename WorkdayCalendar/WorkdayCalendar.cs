using WorkdayCalendar.Interfaces;

namespace WorkdayCalendar;

public class RecurringHoliday
{
    public int Month;
    public int Day;
}

public class WorkdayCalendarService : IWorkdayCalendarService
{
    private readonly List<DateTime> _holidays = new();
    private readonly List<RecurringHoliday> _recurringHolidays = new();
    private WorkdayHours _workdayHours = new();


    public void SetHoliday(DateTime date)
    {
        var foundSameDate = _holidays.Any(d => d.Date == date);
        if (foundSameDate)
            throw new Exception("Holiday already exists");

        _holidays.Add(date);
    }

    public List<DateTime> GetHolidays()
    {
        return _holidays;
    }

    public void SetRecurringHoliday(int month, int day)
    {
        var foundSameDate = _recurringHolidays.Any(x => x.Day == day && x.Month == month);
        if (foundSameDate)
            throw new Exception("Holiday already exists");

        _recurringHolidays.Add(new RecurringHoliday {Day = day, Month = month});
    }

    public List<RecurringHoliday> GetRecurringHolidays()
    {
        return _recurringHolidays;
    }


    public void SetWorkdayStartAndStop(int startHours, int startMinutes, int stopHours, int stopMinutes)
    {
        _workdayHours = new WorkdayHours(startHours, stopHours, startMinutes, stopMinutes);
    }


    public DateTime GetWorkdayIncrement(DateTime startDate, decimal incrementInWorkdays)
    {
        var daysToAdd = (int) incrementInWorkdays;
        var hoursToAdd = (decimal.ToDouble(incrementInWorkdays) - daysToAdd) * _workdayHours.HoursInWeekday;
        var direction = incrementInWorkdays > 0;

        var difference = direction
            ? _workdayHours.GetDifferenceWithEndHour(startDate)
            : _workdayHours.GetDifferenceWithStartHour(startDate);
        var canAddDirectly = direction ? difference > hoursToAdd : hoursToAdd > difference;


        // Move hours to valid workhours when not within WorkingHours.
        if (!_workdayHours.CheckIfWorkingHour(startDate)) startDate = _workdayHours.NextWorkday(startDate, direction);

        // Add workdays 
        startDate = startDate.AddValidWorkdays(this, daysToAdd, direction);

        // Add rest of workhours
        startDate = startDate.AddValidWorkhours(this, _workdayHours, canAddDirectly, direction, hoursToAdd);

        return startDate;
    }
}
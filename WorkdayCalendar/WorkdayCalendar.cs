using WorkdayCalendar.Interfaces;

namespace WorkdayCalendar;

public class RecurringHoliday
{
    public int Month;
    public int Day;
}


public class WorkdayCalendar : IWorkdayCalendar
{
    private readonly List<DateTime> _holidays = new List<DateTime>();
    private readonly List<RecurringHoliday> _recurringHolidays = new List<RecurringHoliday>();
    
    
    public void SetHoliday(DateTime date)
    {
        throw new NotImplementedException();
    }

    public void SetRecurringHoliday(int month, int day)
    {
        throw new NotImplementedException();
    }

    public void SetWorkdayStartAndStop(int startHours, int startMinutes, int stopHours, int stopMinutes)
    {
        throw new NotImplementedException();
    }

    public DateTime GetWorkdayIncrement(DateTime startDate, decimal incrementInWorkdays)
    {
        throw new NotImplementedException();
    }
}
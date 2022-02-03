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
    private WorkdayHours _workdayHours = new WorkdayHours();
    
    
    public void SetHoliday(DateTime date)
    {
        bool foundSameDate = _holidays.Any((d) => d.Date == date);
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
        bool foundSameDate = _recurringHolidays.Any(x => x.Day == day && x.Month == month);
        if (foundSameDate)
            throw new Exception("Holiday already exists");
        
        _recurringHolidays.Add(new RecurringHoliday(){Day = day, Month = month});
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
        int daysToAdd = (int) incrementInWorkdays;
        double hoursToAdd = (Decimal.ToDouble(incrementInWorkdays) - daysToAdd) * _workdayHours.HoursInWeekday;
        bool direction = incrementInWorkdays > 0;
        
        double difference = direction ? _workdayHours.GetDifferenceWithEndHour(startDate) : _workdayHours.GetDifferenceWithStartHour(startDate);
        bool canAddDirectly = direction ? difference > hoursToAdd : hoursToAdd > difference;


        
        // Move hours to valid when not within WorkingHours.
        if (!_workdayTimes.CheckIfWorkingHour(startDate))
        {
            startDate = _workdayTimes.NextWorkdayHour(startDate, direction);
        }

        startDate = startDate.AddValidWorkdays(this, daysToAdd);
        startDate = startDate.AddValidWorkhours(this, canAddDirectly, direction, hoursToAdd);
        
        return startDate;
    }
}
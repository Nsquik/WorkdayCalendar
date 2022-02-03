﻿using WorkdayCalendar.Interfaces;

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
        throw new NotImplementedException();
    }

    public DateTime GetWorkdayIncrement(DateTime startDate, decimal incrementInWorkdays)
    {
        throw new NotImplementedException();
    }
}
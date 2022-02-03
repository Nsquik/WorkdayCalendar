namespace WorkdayCalendar;

public class WorkdayHours
{
    private int _startHour;
    private int _endHour;

    private int _startMinute;
    private int _endMinute;

    public readonly double HoursInWeekday;

    public WorkdayHours()
    {
        
    }
    
    public WorkdayHours(int startHour, int endHour, int startMinute, int endMinute)
    {
        DateTime localDate = DateTime.Now;
        _startHour = startHour;
        _endHour = endHour;
        _startMinute = startMinute;
        _endMinute = endMinute;
        
        HoursInWeekday =  (new DateTime(localDate.Year, localDate.Month, localDate.Day, endHour, endMinute,
            0) - new DateTime(localDate.Year, localDate.Month, localDate.Day, startHour,
            startMinute, 0)).TotalHours;
    }
    
    public double GetDifferenceWithEndHour(DateTime date)
    {
        return (new DateTime(date.Year, date.Month, date.Day, _endHour, _endMinute, 0) - date).TotalHours;
    }
    
    public double GetDifferenceWithStartHour(DateTime date)
    {
        return (new DateTime(date.Year, date.Month, date.Day, _startHour, _startMinute, 0) - date).TotalHours;
    }
    
    public bool CheckIfWorkingHour(DateTime date)
    {
        return date <= new DateTime(date.Year, date.Month, date.Day, _endHour, _endMinute, 0) &&
               date >= new DateTime(date.Year, date.Month, date.Day, _startHour, _startMinute, 0);
    }

}
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
    
    public DateTime NextWorkday(DateTime startDate, bool direction)
    {
        if (direction)
        {
            if (startDate.Hour > _endHour)
            {
                startDate = startDate.AddDays(1);
            }
            startDate = new DateTime(startDate.Year,startDate.Month, startDate.Day, _startHour, _startMinute, 0);
        }
        else
        {
            if (startDate.Hour < _startHour)
            {
                startDate = startDate.AddDays(-1);
            }
            startDate = new DateTime(startDate.Year,startDate.Month, startDate.Day, _endHour, _endMinute, 0);
        }
        return startDate;
    }

}
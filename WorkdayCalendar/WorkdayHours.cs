namespace WorkdayCalendar;

public class WorkdayHours
{
    public int StartHour;
    public int EndHour;

    public int StartMinute;
    public int EndMinute;


    public WorkdayHours()
    {
        
    }
    
    public WorkdayHours(int startHour, int endHour, int startMinute, int endMinute)
    {
        StartHour = startHour;
        EndHour = endHour;
        StartMinute = startMinute;
        EndMinute = endMinute;
    }

}
// See https://aka.ms/new-console-template for more information


using WorkdayCalendar;

WorkdayCalendarService calendarService = new WorkdayCalendarService();

calendarService.SetWorkdayStartAndStop(8,0,16,0);


var start = new DateTime(2022, 2, 4, 16, 01, 0);
var increment =  1.5m;


var endDate = calendarService.GetWorkdayIncrement(start, increment);



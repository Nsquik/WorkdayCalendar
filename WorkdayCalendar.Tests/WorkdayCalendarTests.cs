using System;
using System.Linq;
using NUnit.Framework;

namespace WorkdayCalendar.Tests;

public class Tests
{
    private WorkdayCalendarService _workdayCalendar;

    
    [SetUp]
    public void Setup()
    {
        _workdayCalendar = new WorkdayCalendarService();
        _workdayCalendar.SetWorkdayStartAndStop(8, 0, 16, 0);
        _workdayCalendar.SetRecurringHoliday(5, 17);
        _workdayCalendar.SetHoliday(new DateTime(2004, 5, 27)); 
    }

    [Test]
    public void Calendar_WithoutDuplicate_AddsHoliday()
    {

        DateTime dateToAdd = new DateTime(2022, 8, 13);
        _workdayCalendar.SetHoliday(dateToAdd);
        var holidays =_workdayCalendar.GetHolidays();
        Assert.Contains(dateToAdd, holidays);
    }
    
       
    [Test]
    public void Calendar_WithDuplicate_ShouldNotAddHoliday()
    {
        try
        {
            
            DateTime dateToAdd = new DateTime(2022, 8, 13);
            _workdayCalendar.SetHoliday(dateToAdd);
            _workdayCalendar.SetHoliday(dateToAdd);
            Assert.Fail();
        }
        catch (Exception e)
        {
            Assert.AreEqual(e.Message, "Holiday already exists");
        }
    }
    
    
    [Test]
    public void Calendar_WithoutDuplicate_AddsRecurringHoliday()
    {

        int Day = 8;
        int Month = 12;
        
        _workdayCalendar.SetRecurringHoliday(Month, Day);
        var holidays = _workdayCalendar.GetRecurringHolidays();
        var contains = holidays.Any(h => h.Day == Day && h.Month == Month);

        Assert.True(contains);
    }
    
    [Test]
    public void Calendar_WithDuplicate_ShouldNotAddRecurringHoliday()
    {
        try
        {
            
            _workdayCalendar.SetRecurringHoliday(12, 8);
            _workdayCalendar.SetRecurringHoliday(12, 8);
            Assert.Fail();
        }
        catch (Exception e)
        {
            Assert.AreEqual(e.Message, "Holiday already exists");
        }
    }
    
    [Test]
    public void Calendar_Increment_WithinScope()
    {
        var start = new DateTime(2004, 5, 24, 8, 03, 0);
        var increment = 12.782709m;
        string format = "dd-MM-yyyy HH:mm"; 

        var incrementedDate = _workdayCalendar.GetWorkdayIncrement(start, increment);
        Assert.AreEqual("24-05-2004 08:03 with an addition of 12,782709 work days is 10-06-2004 14:18",
            start.ToString(format) +
            " with an addition of " +
            increment +
            " work days is " +
            incrementedDate.ToString(format));

    }
    
        [Test]
    public void Calendar_Extract_OutsideScope()
    {
        var start = new DateTime(2004, 5, 24, 18, 5, 0);
        var increment = -5.5m;
        string format = "dd-MM-yyyy HH:mm"; 

        var incrementedDate = _workdayCalendar.GetWorkdayIncrement(start, increment);
        Assert.AreEqual("24-05-2004 18:05 with an addition of -5,5 work days is 14-05-2004 12:00",
            start.ToString(format) +
            " with an addition of " +
            increment +
            " work days is " +
            incrementedDate.ToString(format));

    }
    
    [Test]
    public void Calendar_Increment_OutsideScope()
    {
        var start = new DateTime(2004, 5, 24, 19, 03, 0);
        var increment =  44.723656m;
        string format = "dd-MM-yyyy HH:mm"; 

        var incrementedDate = _workdayCalendar.GetWorkdayIncrement(start, increment);
        Assert.AreEqual("24-05-2004 19:03 with an addition of 44,723656 work days is 27-07-2004 13:47",
            start.ToString(format) +
            " with an addition of " +
            increment +
            " work days is " +
            incrementedDate.ToString(format));

    }
    
    
    
    [Test]
    public void Calendar_Extract_OutsideScope_V2()
    {
        var start = new DateTime(2004, 5, 24, 18, 03, 0);
        var increment = -6.7470217m;
        string format = "dd-MM-yyyy HH:mm"; 

        var incrementedDate = _workdayCalendar.GetWorkdayIncrement(start, increment);
        Assert.AreEqual("24-05-2004 18:03 with an addition of -6,7470217 work days is 13-05-2004 10:02",
            start.ToString(format) +
            " with an addition of " +
            increment +
            " work days is " +
            incrementedDate.ToString(format));

    }
    
    

    
    [Test]
    public void Calendar_Increment_WithinScope_V2()
    {
        var start = new DateTime(2004, 5, 24, 7, 03, 0);
        var increment =  8.276628m;
        string format = "dd-MM-yyyy HH:mm"; 

        var incrementedDate = _workdayCalendar.GetWorkdayIncrement(start, increment);
        Assert.AreEqual("24-05-2004 07:03 with an addition of 8,276628 work days is 04-06-2004 10:12",
            start.ToString(format) +
            " with an addition of " +
            increment +
            " work days is " +
            incrementedDate.ToString(format));
    }
    
    [Test]
    public void Calendar_Increment_WithinScope_Weekend()
    {
        var start = new DateTime(2022, 2, 4, 8, 00, 0);
        var increment =  1.5m;
        string format = "dd-MM-yyyy HH:mm"; 

        var incrementedDate = _workdayCalendar.GetWorkdayIncrement(start, increment);
        Assert.AreEqual("04-02-2022 08:00 with an addition of 1,5 work days is 07-02-2022 12:00",
            start.ToString(format) +
            " with an addition of " +
            increment +
            " work days is " +
            incrementedDate.ToString(format));
    }

    
    [Test]
    public void Calendar_Increment_OutsideScope_Weekend()
    {
        var start = new DateTime(2022, 2, 4, 16, 01, 0);
        var increment =  1.5m;
        string format = "dd-MM-yyyy HH:mm"; 

        var incrementedDate = _workdayCalendar.GetWorkdayIncrement(start, increment);
        Assert.AreEqual("04-02-2022 16:01 with an addition of 1,5 work days is 08-02-2022 12:00",
            start.ToString(format) +
            " with an addition of " +
            increment +
            " work days is " +
            incrementedDate.ToString(format));
    }
    
    [Test]
    public void Calendar_Increment_OutsideScope_Weekend_v2()
    {
        var start = new DateTime(2022, 2, 4, 7, 01, 0);
        var increment =  1.5m;
        string format = "dd-MM-yyyy HH:mm"; 

        var incrementedDate = _workdayCalendar.GetWorkdayIncrement(start, increment);
        Assert.AreEqual("04-02-2022 07:01 with an addition of 1,5 work days is 07-02-2022 12:00",
            start.ToString(format) +
            " with an addition of " +
            increment +
            " work days is " +
            incrementedDate.ToString(format));
    }
    
    [Test]
    public void Calendar_Extract_WithinScope_Weekend()
    {
        var start = new DateTime(2022, 2, 7, 8, 00, 0);
        var increment =  -1.5m;
        string format = "dd-MM-yyyy HH:mm"; 

        var incrementedDate = _workdayCalendar.GetWorkdayIncrement(start, increment);
        Assert.AreEqual("07-02-2022 08:00 with an addition of -1,5 work days is 03-02-2022 12:00",
            start.ToString(format) +
            " with an addition of " +
            increment +
            " work days is " +
            incrementedDate.ToString(format));
    }
    
    
   
    
    

    
   
}
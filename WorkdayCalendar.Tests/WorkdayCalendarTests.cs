using System;
using System.Linq;
using NUnit.Framework;

namespace WorkdayCalendar.Tests;

public class Tests
{
    private WorkdayCalendar _workdayCalendar;

    
    [SetUp]
    public void Setup()
    {
        _workdayCalendar = new WorkdayCalendar();
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

}
using System;
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

}
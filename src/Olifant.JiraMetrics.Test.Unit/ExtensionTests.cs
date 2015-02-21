using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

using Olifant.JiraMetrics.Lib;

using NUnit.Framework;

namespace Olifant.JiraMetrics.Test.Unit
{
    public class ExtensionTests
    {
        [TestCase("2014-01-29", 5)]
        [TestCase("2014-12-29", 1)]
        public void DateTimeReturnsWeekNumber(string givenDate, int expectedWeekNumber)
        {
            var date = DateTime.Parse(givenDate);
            date.GetIso8601WeekOfYear().ShouldBeEquivalentTo(expectedWeekNumber);
        }
    }
}

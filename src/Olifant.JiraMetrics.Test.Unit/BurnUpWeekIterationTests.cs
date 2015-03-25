using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

using Olifant.JiraMetrics.Lib.Metrics.BurnUp;

using NUnit.Framework;

namespace Olifant.JiraMetrics.Test.Unit
{
    public class BurnUpWeekIterationTests
    {
        [Test]
        public void AddingWeekHandlesNewYearIsPassed()
        {
            //arrange
            var iteration = new BurnUpWeekIteration("2014-12-27");

            //act
            var target = iteration.AddWeek();


            //assert
            target.WeekLabel.ShouldBeEquivalentTo("y15w1");
        }

        [TestCase("2014-12-27", "y14w52")]
        [TestCase("2014-12-30", "y15w1")]
        public void SuppliesWeeklabel(string givenDate, string expectedWeekLabel)
        {
            var iteration = new BurnUpWeekIteration(givenDate);
            iteration.WeekLabel.ShouldBeEquivalentTo(expectedWeekLabel);
        }
    }
}

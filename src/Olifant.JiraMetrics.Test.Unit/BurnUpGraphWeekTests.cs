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
    public class BurnUpGraphWeekTests
    {
        [Test]
        public void AddingWeekHandlesNewYearIsPassed()
        {
            //arrange
            var iteration = new BurnUpGraphWeek("2014-12-27");

            //act
            var target = iteration.AddWeek();


            //assert
            target.WeekLabel.ShouldBeEquivalentTo("y15w1");
        }

        [TestCase("2014-12-27", "y14w52")]
        [TestCase("2014-12-30", "y15w1")]
        public void SuppliesWeeklabel(string givenDate, string expectedWeekLabel)
        {
            var iteration = new BurnUpGraphWeek(givenDate);
            iteration.WeekLabel.ShouldBeEquivalentTo(expectedWeekLabel);
        }

        [Test]
        public void CreatesRangeOfWeeks()
        {
            var startWeek = new BurnUpGraphWeek("2014-12-11");
            var endWeek = new BurnUpGraphWeek("2015-01-20");
            var actualWeeks = startWeek.To(endWeek).Select(w => w.WeekLabel).ToList();

            actualWeeks.Should().ContainInOrder("y14w50", "y14w51", "y14w52", "y15w1", "y15w2", "y15w3", "y15w4");
        }
    }
}

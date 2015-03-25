using FluentAssertions;

using Olifant.JiraMetrics.Lib.Metrics.BurnUp;

using NUnit.Framework;

namespace Olifant.JiraMetrics.Test.Unit
{
    public class BurnUpValueTests
    {
        [Test]
        public void SuppliesStoryPoints()
        {
            const int GivenPoints = 4;
            var target = new BurnUpValue(GivenPoints);
            
            target.StoryPoints
                .ShouldBeEquivalentTo(GivenPoints);
        }
    }
}

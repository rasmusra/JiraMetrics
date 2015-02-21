using FluentAssertions;

using HM.JiraMetrics.Lib.Metrics.BurnUpGraph;

using NUnit.Framework;

namespace HM.JiraMetrics.Test.Unit
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

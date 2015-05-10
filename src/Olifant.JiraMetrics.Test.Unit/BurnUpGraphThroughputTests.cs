using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

using Olifant.JiraMetrics.Lib.Metrics.BurnUp;

using NUnit.Framework;
using Olifant.JiraMetrics.Lib.Metrics.Model;
using Olifant.JiraMetrics.Test.Unit.Mocks;

namespace Olifant.JiraMetrics.Test.Unit
{
    public class BurnUpGraphThroughputTests
    {
        [Test]
        public void SuppliesTotalPointsInThroughput()
        {
            var issues = new List<IIssueReportModel>  
            {
                IssueReportModelMockFactory.Create(3, "2015-05-10"),
                IssueReportModelMockFactory.Create(5, "1995-05-10"),
                IssueReportModelMockFactory.Create(13, "2017-05-10")
            };
            var throughput = new BurnUpGraphThroughput(issues);
            var expectedPoints = issues.Sum(i => i.StoryPoints);

            throughput.StoryPoints.ShouldBeEquivalentTo(expectedPoints);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

using NUnit.Framework;
using Olifant.JiraMetrics.Lib.Metrics.BurnUp;
using Olifant.JiraMetrics.Lib.Metrics.Model;
using Olifant.JiraMetrics.Test.Unit.Mocks;

namespace Olifant.JiraMetrics.Test.Unit
{
    public class BurnUpGraphTests
    {
        [Test]
        public void CreatesListOfWeeks()
        {
            var issueInWeek19 = IssueReportModelMockFactory.Create(2, "2015-05-04");
            var issue1InWeek21 = IssueReportModelMockFactory.Create(3, "2015-05-18");
            var issue2InWeek21 = IssueReportModelMockFactory.Create(5, "2015-05-20");
            var issueInWeek22 = IssueReportModelMockFactory.Create(8, "2015-05-26");

            var target =
                new BurnUpGraph(new List<IIssueReportModel>
                {
                    issueInWeek19,
                    issue1InWeek21,
                    issueInWeek22,
                    issue2InWeek21
                });

            var expectedWeeks = new[]
            {
                "y15w19",
                "y15w20",
                "y15w21",
                "y15w22"
            };

            target.Weeks.Select(xvalue => xvalue.WeekLabel).Should().ContainInOrder(expectedWeeks);
        }

        [Test]
        public void CreatesListOfAcumulatedPoints()
        {
            var issueInWeek19 = IssueReportModelMockFactory.Create(2, "2015-05-04");
            var issue1InWeek21 = IssueReportModelMockFactory.Create(3, "2015-05-18");
            var issue2InWeek21 = IssueReportModelMockFactory.Create(5, "2015-05-20");
            var issueInWeek22 = IssueReportModelMockFactory.Create(8, "2015-05-26");

            var expctedAccumulatedStoryPoints = new[]
            {
                issueInWeek19.StoryPoints, //w19
                issueInWeek19.StoryPoints, //w20
                issueInWeek19.StoryPoints + issue1InWeek21.StoryPoints + issue2InWeek21.StoryPoints, //w21
                issueInWeek19.StoryPoints + issue1InWeek21.StoryPoints + issue2InWeek21.StoryPoints + issueInWeek22.StoryPoints //w22
            };

            var target =
                new BurnUpGraph(new List<IIssueReportModel>
                {
                    issueInWeek19,
                    issue1InWeek21,
                    issueInWeek22,
                    issue2InWeek21
                });

            target.AccumulatedPointsList.Should().ContainInOrder(expctedAccumulatedStoryPoints);
        }

        [Test]
        public void CreatesEmptyAccumulatedPointsListsWhenNoWeeksAreGiven()
        {
            var target = new BurnUpGraph(new List<IIssueReportModel>());

            target.AccumulatedPointsList.Should().BeEmpty();
        }

        [Test]
        public void CreatesEmptyWeeksListsWhenNoWeeksAreGiven()
        {
            var target = new BurnUpGraph(new List<IIssueReportModel>());

            target.Weeks.Select(xvalue => xvalue.WeekLabel).Should().BeEmpty();
        }
    }
}

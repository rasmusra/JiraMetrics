using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using Olifant.JiraMetrics.Lib.Metrics;
using Olifant.JiraMetrics.Lib.Metrics.BurnUp;
using Olifant.JiraMetrics.Lib.Metrics.Filters;
using Olifant.JiraMetrics.Lib.Metrics.Model;
using Olifant.JiraMetrics.Lib.Metrics.TextReport;
using Olifant.JiraMetrics.Test.Unit.Mocks;

using NUnit.Framework;
using Olifant.JiraMetrics.Lib.Jira.Model;
using Olifant.JiraMetrics.Test.Utilities.Fakes;

namespace Olifant.JiraMetrics.Test.Unit
{
    public class ReportManagerTests
    {
        [Test]
        public void CarriageReturnAndSimilarCharactersInHeaderIsReplacedBySpaceCharacter()
        {
            // arrange
            var fakeTextEditorProxy = new FakeTextEditorProxy();
            var target = new TextReportManager(fakeTextEditorProxy);
            const string givenJql = "some jql\nhaving linebreaks and\rother nasties";

            // act
            target.GenerateCycleTimeReport(
                new List<IssueReportModel>(),
                new CycleTimeRule(Status.Create(new[] { "Implement", "Implementing", "Review" })),
                givenJql,
                new List<IIssueFilter>());

            // assert
            fakeTextEditorProxy.ActualHeader.Should().Contain("some jql having linebreaks and other nasties", "linebreaks and similar chars should be replaced with spaces");
        }

        [Test]
        public void ReturnsWeeksOnXaxisFromListOfIssues()
        {
            // arrange
            var issueReportModels = new List<IIssueReportModel>() 
            { 
                IssueReportModelMockFactory.Create(3, "2014-04-15"), 
                IssueReportModelMockFactory.Create(2, "2014-04-14"), 
                IssueReportModelMockFactory.Create(5, "2014-04-29") 
            };

            //act
            var burnUpData = new BurnUpGraph(issueReportModels);

            // assert
            burnUpData.Weeks.Select(bugw => bugw.WeekLabel)
                .Should().ContainInOrder(new[] { "y14w16", "y14w17", "y14w18" });
        }

        [Test]
        public void ReturnsAccumulatedStoryPointsOnYaxisFromListOfIssues()
        {
            // arrange
            var issueReportModels = new List<IIssueReportModel>() 
            { 
                IssueReportModelMockFactory.Create(3, "2014-04-15"), 
                IssueReportModelMockFactory.Create(2, "2014-04-14"), 
                IssueReportModelMockFactory.Create(5, "2014-04-29") 
            };

            //act
            var burnUpData = new BurnUpGraph(issueReportModels);

            // assert
            burnUpData.AccumulatedPointsList
                .Should().ContainInOrder(new decimal[] { 5, 5, 10 });
        }
    }
}

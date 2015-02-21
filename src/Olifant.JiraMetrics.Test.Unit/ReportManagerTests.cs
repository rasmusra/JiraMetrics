using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

using Olifant.JiraMetrics.Lib.Metrics;
using Olifant.JiraMetrics.Lib.Metrics.BurnUpGraph;
using Olifant.JiraMetrics.Lib.Metrics.Filters;
using Olifant.JiraMetrics.Lib.Metrics.Model;
using Olifant.JiraMetrics.Lib.Metrics.TextReport;
using Olifant.JiraMetrics.Test.Fakes;
using Olifant.JiraMetrics.Test.Unit.Mocks;

using NUnit.Framework;

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
            const string GivenJql = "some jql\nhaving linebreaks and\rother nasties";

            // act
            target.GenerateCycleTimeReport(
                new List<IssueReportModel>(),
                new CycleTimeRule(new[] { "Implement", "Implementing", "Review" }),
                GivenJql,
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
                IssueReportModelMock.Create("3", "2014-04-15"), 
                IssueReportModelMock.Create("2", "2014-04-14"), 
                IssueReportModelMock.Create("5", "2014-04-29") 
            };

            //act
            var burnUpData = BurnUpGraphManager.SummonData(issueReportModels);

            // assert
            burnUpData.Keys.Select(k => k.WeekLabel).Should().ContainInOrder(new[] { "y14w16", "y14w17", "y14w18" });
        }

        [Test]
        public void ReturnsAccumulatedStoryPointsOnYaxisFromListOfIssues()
        {
            // arrange
            var issueReportModels = new List<IIssueReportModel>() 
            { 
                IssueReportModelMock.Create("3", "2014-04-15"), 
                IssueReportModelMock.Create("2", "2014-04-14"), 
                IssueReportModelMock.Create("5", "2014-04-29") 
            };

            //act
            var burnUpData = BurnUpGraphManager.SummonData(issueReportModels);
            var points = burnUpData.Values.Select(v => v.StoryPoints);

            // assert
            points.Should().ContainInOrder(new decimal[] { 5, 5, 10 });
        }
    }
}

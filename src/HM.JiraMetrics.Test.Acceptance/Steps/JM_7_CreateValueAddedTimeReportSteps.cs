using System;
using System.Linq;

using FluentAssertions;

using HM.JiraMetrics.Lib.Metrics.Model;
using HM.JiraMetrics.Lib.Metrics.TextReport;
using HM.JiraMetrics.Test.Acceptance.Steps.Helpers;

using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace HM.JiraMetrics.Test.Acceptance.Steps
{
    [Binding]
    public class JM_7_CreateValueAddedTimeReportSteps
    {
        [Then(@"I should be able to see the following title header in the report: ""(.*)""")]
        public void ThenIShouldBeAbleToSeeTheFollowingTitleHeaderInTheReport(string title)
        {
            ScenarioWrapper.FakeTextEditorProxy.ActualHeader.ShouldBeEquivalentTo(title);
        }

        [Then(@"I should be presented the following value added times in the report:")]
        public void ThenIShouldBePresentedTheFollowingValueAddedTimesInTheReport(Table table)
        {
            var valueAddedTimeReportSpec = table.CreateSet<ValueAddedTimeReportSpec>();
            
            ScenarioWrapper.FakeTextEditorProxy.ActualRowsExclHeader
                .Should().ContainInOrder(valueAddedTimeReportSpec);
        }
    }
}

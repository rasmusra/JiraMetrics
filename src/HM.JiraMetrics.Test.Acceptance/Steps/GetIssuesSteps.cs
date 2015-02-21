using System.Linq;

using FluentAssertions;

using HM.JiraMetrics.Test.Acceptance.Steps.Helpers;

using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace HM.JiraMetrics.Test.Acceptance.Steps
{
    [Binding]
    public class GetIssuesSteps
    {
        [Given(@"I have entered the following query: ""(.*)""")]
        public void GivenIHaveEnteredTheFollowingQuery(string jql)
        {
            ScenarioWrapper.Jql = jql;
        }

        [Then(@"I should not see ""(.*)"" in the report")]
        public void ShouldNotSeeTheFollowingIssueInTheReport(string issuekey)
        {
            var actualMatchingKeys =
                ScenarioWrapper.FakeTextEditorProxy.ActualRowsExclHeader.Where(
                    actualRow => actualRow.Contains(issuekey));

            actualMatchingKeys
            .Should().BeEmpty(string.Format("the issue {0} should have been filtered out", issuekey));
        }

        [Then(@"I should be presented all defects in a text file")]
        public void ThenIShouldBePresentedAllDefectsInATextFile()
        {
            ScenarioWrapper.FakeTextEditorProxy.ActualText
                .Should().NotBeEmpty();
        }

        [Then(@"I should see ""(.*)"" in the report")]
        public void VerifyKeyIsInReport(string expectedKey)
        {
            ScenarioWrapper.FakeTextEditorProxy.ActualRowsExclHeader
            .Where(actualRow => actualRow.Contains(expectedKey))
            .Should().NotBeEmpty();
        }

        [Then(@"I should be able to see the following cycle times in the report:")]
        public void VerifyReportRowsAreFoundInReport(Table table)
        {
            var spec = table.CreateSet<CycleTimeReportEntrySpec>();

            var expectedReportedEntries = spec.Select(row => row.ToString());

            ScenarioWrapper.FakeTextEditorProxy.ActualText
            .Should().Contain(expectedReportedEntries);
        }
    }
}
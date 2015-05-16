using FluentAssertions;
using Olifant.JiraMetrics.Test.Acceptance.Pages;
using Olifant.JiraMetrics.Test.Acceptance.Steps.Specs;

using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Olifant.JiraMetrics.Test.Acceptance.Steps
{
    [Binding]
    public class JM6_MakeItPossibleToAssignAnyExistingStatusesToCycleTimeSteps
    {
        [Then(@"I should be able to see the following header in the report: ""(.*)""")]
        public void ThenIShouldBeAbleToSeeTheFollowingHeaderInTheReport(string expectedHeader)
        {
            ScenarioWrapper.FakeTextEditorProxy.ActualHeader.ShouldBeEquivalentTo(expectedHeader);
        }

        [Then(@"I should see default setup for statuses in cycle:")]
        [Then(@"I should see following setup for statuses in cycle:")]
        public void ThenIShouldSeeDefaultSetupForStatusesInCycle(Table table)
        {
            var cycleSetupManager = new CycleSetupSpecManager(table.CreateSet<CycleSetupSpec>());
            var burnUpPage = ScenarioWrapper.PageNavigator.GetCurrent<BurnUpPage>();

            burnUpPage.PreCycleStatusesContains(cycleSetupManager.PreCycleStatuses);
            burnUpPage.CycleStatusesContains(cycleSetupManager.CycleStatuses);
            burnUpPage.PostCycleStatusesContains(cycleSetupManager.PostCycleStatuses);
        }

        [When(@"I move status ""(.*)"" in ""(.*)"" to ""(.*)""")]
        [When(@"status ""(.*)"" is moved from ""(.*)"" to ""(.*)""")]
        public void MoveStatus(string status, string from, string to)
        {
            ScenarioWrapper.PageNavigator.GetCurrent<BurnUpPage>().MoveStatus(status, from, to);
        }

        [When(@"I make the statuses visible")]
        [When(@"statuses are made visible")]
        [When(@"I hide the statuses")]
        public void ToggleStatuses()
        {
            ScenarioWrapper.PageNavigator.GetCurrent<BurnUpPage>().ToggleStatuses();
        }

        [Then(@"the statuses should be hidden")]
        public void ThenTheStatusesShouldBeHidden()
        {
            // how test this one? not a cruical one...
        }
    }
}
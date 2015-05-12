using System;
using System.Linq;
using Olifant.JiraMetrics.Test.Acceptance.Steps.Specs;
using Olifant.JiraMetrics.Test.Utilities.Fakes;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Olifant.JiraMetrics.Test.Acceptance.Steps
{
    [Binding]
    public sealed class GeneralSteps
    {
        [Given(@"this is not pending anymore")]
        public void Pending()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I am logged in as ""(.*)""")]
        [Given(@"a team member named ""(.*)""")]
        [Given(@"a project lead named ""(.*)""")]
        [Given(@"a stakeholder named ""(.*)""")]
        [Given(@"a system named ""(.*)""")]
        [Given(@"a project named ""(.*)""")]
        [Given(@"a project site named ""(.*)""")]
        public void NoAction(string value)
        {
            // TODO: find out how to manage authorization... 
        }

        [Given(@"I navigate to ""(.*)"" page")]
        [When(@"I navigate to ""(.*)"" page")]
        public void NavigateTo(string page)
        {
            ScenarioWrapper.PageNavigator.GoTo(page);
        }

        [Given(@"Jira contains additional issues:")]
        public void SetupIssuesAvailableInJira(Table table)
        {
            // It gets really messy to setup scenarios Jira during runtime (mock), because in order to achieve 
            // inter-process communication with web-server we need to write json files to webservers stub directory. 
            // Instead, let's settle with making sure that the expected issues are available in the stub directory
            // that is already in place.
            var expectedIssues = table.CreateSet<IssueSpec>().ToList();
            var fakeClient = new FakeJiraRestClient("Stubs");

            // this will fail on missing stub files
            var missingIssue = expectedIssues
                .Select(issue => issue.Key)
                .FirstOrDefault(key => !fakeClient.MatchingFileExists(string.Format("*{0}*.json", key)));

            if (missingIssue != null)
            {
                throw new NotImplementedException(string.Format("Cannot find stub-file for key: {0}", missingIssue));
            }
        }

        [When(@"I wait, but not longer than (.*) second")]
        [When(@"I wait, but not longer than (.*) seconds")]
        public void SetPageLoadTimeout(int timeoutInSeconds)
        {
            ScenarioWrapper.PageNavigator.Current.LoadTimeout = TimeSpan.FromSeconds(timeoutInSeconds);
        }
    }
}

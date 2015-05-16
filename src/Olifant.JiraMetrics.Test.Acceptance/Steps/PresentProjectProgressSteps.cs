using System;
using System.Linq;
using FluentAssertions;
using Olifant.JiraMetrics.Lib.Jira.Model;
using Olifant.JiraMetrics.Test.Acceptance.Pages;
using Olifant.JiraMetrics.Test.Acceptance.Steps.Specs;
using Olifant.JiraMetrics.Test.Utilities.Helpers;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Olifant.JiraMetrics.Test.Acceptance.Steps
{
    [Binding]
    public class PresentProjectProgressSteps
    {
        [When(@"I load JiraMetrics with issues from Jira project ""(.*)""")]
        public void LoadFromJira(string project)
        {
            ScenarioWrapper.PageNavigator.GetCurrent<AdminPage>().Load(project);
        }

        [When(@"I load JiraMetrics with project ""(.*)"" having the following issue:")]
        [When(@"I load JiraMetrics with project ""(.*)"" having the following issues:")]
        public void WhenILoadJiraMetricsWithProjectHavingTheFollowingIssue(string project, Table table)
        {
            // the issues in the supplied table needs to be in the test data population, 
            // table is actually only in scenario for explaining example in the spec
            LoadFromJira(project);
        }

        [Then(@"within (.*) seconds I should be able to see the following values for project ""(.*)"" in the burn-up graph:")]
        public void GoToBurnUpAndVerifyGraph(int timeoutInSeconds, string project , Table table)
        {
            ScenarioWrapper.PageNavigator.GoTo("burn-up");
            ScenarioWrapper.PageNavigator.GetCurrent<BurnUpPage>().SearchForProject(project);
            ScenarioWrapper.PageNavigator.Current.LoadTimeout = TimeSpan.FromSeconds(timeoutInSeconds);
            VerifyGraphValues(table);
        }

        [Given(@"JiraMetrics contains the following issues:")]
        public void ClearAndInsertIssuesIntoMongo(Table table)
        {
            // TODO: move to MongoWrapper
            var givenIssues = table.CreateSet<IssueSpec>();
            var issuesCollection = MongoWrapper.Instance.GetCollection<Issue>();

            var issues = givenIssues
                .Select(issue => JiraStubFactory.Create(issue.Key, issue.StoryPoints));

            issuesCollection.Drop();
            issuesCollection.InsertBatch(issues);

            if (ScenarioWrapper.PageNavigator.Current != null)
            {
                ScenarioWrapper.PageNavigator.Current.Refresh();
            }
        }

        [Then(@"I should see a burn-up graph")]
        public void VerifyDefaultGraph()
        {
            ScenarioWrapper.PageNavigator.GetCurrent<BurnUpPage>().ChartContains("Week", "Burnup", "start")
                .Should().BeTrue("the chart component were not found on page.");
        }

        [Then(@"I should be presented a message ""(.*)""")]
        public void VerifyLoadedIssuesReport(string expectedMessage)
        {
            ScenarioWrapper.PageNavigator.GetCurrent<AdminPage>().LoadedIssuesReportContains(expectedMessage)
                .Should().BeTrue("expected issues should be found in page");
        }

        [Then(@"I should be presented a list of all issues that has been added:")]
        public void VerifyLoadedIssuesReport(Table table)
        {
            var expectedIssues = table.CreateSet<LoadedIssueSpec>();
            ScenarioWrapper.PageNavigator.GetCurrent<AdminPage>().LoadedIssuesReportContains(expectedIssues)
                .Should().BeTrue("expected issues should be found in page");
        }

        [When(@"I select project '(.*)' and search for issues")]
        [When(@"I query ""(.*)""")]
        public void SearchForProject(string project)
        {
            ScenarioWrapper.PageNavigator.GetCurrent<BurnUpPage>().SearchForProject(project);
        }

        [Then(@"I should see the following values in the graph:")]
        public void VerifyGraphValues(Table table)
        {
            var graphSpecList = table.CreateSet<BurnUpGraphSpec>();

            ScenarioWrapper.PageNavigator.GetCurrent<BurnUpPage>()
                .ChartContains(graphSpecList.SelectMany(gr => gr.Fields))
                .Should().BeTrue();
        }

        [Then(@"I should see a dropdown with selectable projects:")]
        public void VerifyProjectDropdown(Table table)
        {
            var projectDropdown = ScenarioWrapper.PageNavigator.GetCurrent<BurnUpPage>().ProjectDropdown;

            foreach (var expectedProject in table.Rows.Select(row => row[0]))
            {
                projectDropdown.Should().Contain(expectedProject);
            }
        }

        [Then(@"the accumulated story points of all issues should be (.*)")]
        public void VerifyGraphValues(double expectedStoryPoints)
        {
            var expectedText = string.Format("data: [0, {0:0.0}]", expectedStoryPoints);

            ScenarioWrapper.PageNavigator.GetCurrent<BurnUpPage>().ChartDivContains(expectedText)
                .Should().BeTrue();
        }

        [Given(@"JiraMetrics contains all the latest versions of issues in Jira")]
        public void GivenJiraMetricsContainsAllTheLatestVersionsOfIssuesInJira()
        {
            // we will hereby ASSUME that default db setup means jirametrics is up-to-date with Jira
        }
    }
}

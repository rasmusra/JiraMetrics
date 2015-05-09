using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Olifant.JiraMetrics.Lib.Jira.Model;
using Olifant.JiraMetrics.Test.Acceptance.Pages;
using Olifant.JiraMetrics.Test.Acceptance.Steps.Specs;
using Olifant.JiraMetrics.Test.Utilities.Fakes;
using Olifant.JiraMetrics.Test.Utilities.Helpers;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Olifant.JiraMetrics.Test.Acceptance.Steps
{
    [Binding]
    public class PresentProjectProgressSteps
    {
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

        [When(@"I load JiraMetrics with issues from Jira project ""(.*)""")]
        public void LoadFromJira(string project)
        {
            ScenarioWrapper.PageNavigator.GetCurrent<AdminPage>().Load(project);
        }

        [When(@"I choose to load JiraMetrics with project ""(.*)""")]
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
                .Select(issue => IssueStubFactory.Create(issue.Key, issue.StoryPoints));

            issuesCollection.Drop();
            issuesCollection.InsertBatch(issues);

            if (ScenarioWrapper.PageNavigator.Current != null)
            {
                ScenarioWrapper.PageNavigator.Current.Refresh();
            }
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

        [Given(@"there exists a Jira project called '(.*)' with (.*) issues where each has story point of (.*)")]
        public void InsertManyIssuesIntoMongo(string projectName, int noofIssues, int storyPoint)
        {
            // TODO: move to MongoWrapper
            var issuesCollection = MongoWrapper.Instance.GetCollection<Issue>();
            var issues = IssueStubFactory.CreateMany("DISCO-620", noofIssues, storyPoint);
            issues.ToList().ForEach(i => i.Fields.Project.Name = projectName);
            issuesCollection.InsertBatch(issues);

            if (ScenarioWrapper.PageNavigator.Current != null)
            {
                ScenarioWrapper.PageNavigator.Current.Refresh();
            }
        }

        [When(@"I wait, but not longer than (.*) second")]
        [When(@"I wait, but not longer than (.*) seconds")]
        public void SetPageLoadTimeout(int timeoutInSeconds)
        {
            ScenarioWrapper.PageNavigator.Current.LoadTimeout = TimeSpan.FromSeconds(timeoutInSeconds);
        }

        [Then(@"I should see a burn-up graph")]
        public void VerifyDefaultGraph()
        {
            ScenarioWrapper.PageNavigator.GetCurrent<BurnUpPage>().ChartContains("Week", "Burnup", "start")
                .Should().BeTrue("the chart component were not found on page.");
        }

        [Then(@"I should be presented a list of all issues that has been added:")]
        public void ThenIShouldBePresentedAListOfIssuesBeenAdded(Table table)
        {
            var expectedIssues = table.CreateSet<LoadedIssueSpec>();
            ScenarioWrapper.PageNavigator.GetCurrent<AdminPage>().LoadedIssuesReportContains(expectedIssues)
                .Should().BeTrue("expected issues should be found in page");
        }

        [When(@"I select project '(.*)' and search for issues")]
        [When(@"I query ""(.*)""")]
        public void Search(string project)
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
    }
}

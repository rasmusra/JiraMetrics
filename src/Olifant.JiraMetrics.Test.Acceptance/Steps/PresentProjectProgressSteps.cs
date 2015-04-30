using System;
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

        [When(@"I choose to load JiraMetrics with project ""(.*)""")]
        public void WhenIChooseToLoadJiraMetricsWithProject(string project)
        {
            ScenarioWrapper.PageNavigator.GetCurrent<AdminPage>().Load(project);
        }

        [Then(@"I should be presented a list of issues been added:")]
        public void ThenIShouldBePresentedAListOfIssuesBeenAdded(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"the system contains the following issues:")]
        public void ClearAndInsertIssuesIntoMongo(Table table)
        {
            // TODO: move to MongoWrapper
            var givenIssues = table.CreateSet<IssueSpec>();
            var issuesCollection = MongoWrapper.Instance.GetCollection<Issue>();

            var issues = givenIssues
                .Select(issue => IssueStubFactory.Create(issue.Key, issue.StoryPoints));

            issuesCollection.Drop();
            issuesCollection.InsertBatch(issues);

            ScenarioWrapper.PageNavigator.Current.Refresh();
        }
   
        [Given(@"Jira contains additional issues:")]
        public void SetupIssuesAvailableInJira(Table table)
        {
            var givenIssues = table.CreateSet<IssueSpec>();
            ScenarioWrapper.FakeJiraRestClient = new FakeJiraRestClient();

            foreach (var issue in givenIssues)
            {
                ScenarioWrapper.FakeJiraRestClient
                    .Jql2KeysLookup[issue.Project].Add(issue.Key);
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

            ScenarioWrapper.PageNavigator.Current.Refresh();
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
            ScenarioWrapper.PageNavigator.GetCurrent<BurnUpPage>().ChartContainerContains("Week", "Burnup", "start");
        }

        [When(@"I select project '(.*)' and search for issues")]
        [When(@"I query ""(.*)""")]
        public void WhenISearch(string project)
        {
            ScenarioWrapper.PageNavigator.GetCurrent<BurnUpPage>().SearchForProject(project);
        }

        [Then(@"I should see the following values in the graph:")]
        public void VerifyGraphValues(Table table)
        {
            var graphSpecList = table.CreateSet<BurnUpGraphSpec>().ToList();
            ScenarioWrapper.PageNavigator.GetCurrent<BurnUpPage>()
                .ChartContainerContains(graphSpecList.SelectMany(gr => gr.Fields))
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

using System;
using System.Linq;

using FluentAssertions;
using Olifant.JiraMetrics.Lib.Jira.Model;
using Olifant.JiraMetrics.Test.Acceptance.Steps.Specs;
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

        [Given(@"I navigate to burn-up page")]
        [When(@"I navigate to burn-up page")]
        public void NavigateToBurnUpPage()
        {
            var url = string.Format("http://localhost:{0}", WebServer.Port);
            FeatureWrapper.PhantomJsDriver.Navigate().GoToUrl(url);
        }

        [Given(@"there exists a Jira project called '(.*)' with (.*) issues where each has story point of (.*)")]
        public void InsertIssuesIntoMongo(string projectName, int noofIssues, int storyPoint)
        {
            var issuesCollection = MongoWrapper.Instance.GetCollection<Issue>();
            var issues = IssueStubFactory.CreateMany("DISCO-620", noofIssues, storyPoint);

            issuesCollection.InsertBatch(issues);
        }

        [Then(@"I should see a burn-up graph within (.*) seconds")]
        public void VerifyDefaultGraph(int timeoutInSeconds)
        {
            FeatureWrapper.PhantomJsDriver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(timeoutInSeconds)); 
            var chartDiv = FeatureWrapper.JQuery.Find("#chart_container");
            chartDiv.Text().Should().Contain("Week");
            chartDiv.Text().Should().Contain("Burnup");
            chartDiv.Text().Should().Contain("start");
        }

        [When(@"I search for issues with jql query '(.*)'")]
        [When(@"I query ""(.*)""")]
        public void WhenISearchForIssuesWithJqlQuery(string jql)
        {
            FeatureWrapper.PhantomJsDriver.FindElementById("jql").SendKeys(jql);
            FeatureWrapper.JQuery.Find("#Search").Click();
        }

        [Then(@"I should see the following values in the graph:")]
        public void VerifyGraphValues(Table table)
        {
            var chartDiv = FeatureWrapper.JQuery.Find("#chart_container");
            var graphData = table.CreateSet<BurnUpGraphSpec>().ToList();
            graphData.ForEach(data =>
            {
                chartDiv.Text().Should().Contain(data.StartX);
                chartDiv.Text().Should().Contain(data.EndX);
                chartDiv.Text().Should().Contain(data.StartY);
                chartDiv.Text().Should().Contain(data.EndY);
            });
        }

        [Then(@"I should see an empty search field")]
        public void VerifyEmptySearchField()
        {
            FeatureWrapper.JQuery.Find("#jql").Text().
                Should().BeEmpty();
        }

        [Then(@"the accumulated story points of all issues should be (.*)")]
        public void VerifyGraphValues(int expectedStoryPoints)
        {
            var chartDiv = FeatureWrapper.JQuery.Find("#chart_container");
            
            chartDiv.Text()
                .Should().Contain(expectedStoryPoints.ToString());
        }
    }
}

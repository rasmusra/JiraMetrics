using System;
using System.Linq;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;
using Olifant.JiraMetrics.Lib.Jira.Model;
using Olifant.JiraMetrics.Test.Acceptance.Steps.Specs;
using Olifant.JiraMetrics.Test.Utilities.Helpers;
using OpenQA.Selenium.Support.UI;
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
            var url = string.Format("http://localhost:{0}", IisExpressManager.Port);
            FeatureWrapper.PhantomJsDriver.Navigate().GoToUrl(url);
        }

        [Given(@"there exists a Jira project called '(.*)' with (.*) issues where each has story point of (.*)")]
        public void InsertIssuesIntoMongo(string projectName, int noofIssues, int storyPoint)
        {
            var issuesCollection = MongoWrapper.Instance.GetCollection<Issue>();
            var issues = IssueStubFactory.CreateMany("DISCO-620", noofIssues, storyPoint);
            issues.ToList().ForEach(i => i.Fields.Project.Name = projectName);
            issuesCollection.InsertBatch(issues);
            FeatureWrapper.PhantomJsDriver.Navigate().Refresh();
        }

        [Then(@"I should see a burn-up graph within (.*) seconds")]
        public void VerifyDefaultGraph(int timeoutInSeconds)
        {
            ScenarioWrapper.PageLoadTimeout = timeoutInSeconds;

            WaitForRendering(() =>
            {
                var chartContainer = FeatureWrapper.JQuery.Find("#chart_container");
                return 
                    chartContainer.Text().Contains("Week") &&
                    chartContainer.Text().Contains("Burnup") &&
                    chartContainer.Text().Contains("start");
            });
        }

        [When(@"I select project '(.*)' and search for issues")]
        [When(@"I query ""(.*)""")]
        public void WhenISearch(string project)
        {
            var projectDropdown = FeatureWrapper.PhantomJsDriver
                .FindElementById("ProjectList");

            new SelectElement(projectDropdown)
                .SelectByText(project);

            FeatureWrapper.JQuery.Find("#Search")
                .Click();
        }

        [Then(@"I should see the following values in the graph:")]
        public void VerifyGraphValues(Table table)
        {
            WaitForRendering(() =>
            {
                var chartContainer = FeatureWrapper.JQuery.Find("#chart_container");
                var graphSpecList = table.CreateSet<BurnUpGraphSpec>().ToList();
                return graphSpecList.All(graphSpec =>
                    chartContainer.Text().Contains(graphSpec.StartX) &&
                    chartContainer.Text().Contains(graphSpec.EndX) &&
                    chartContainer.Text().Contains(graphSpec.StartY) &&
                    chartContainer.Text().Contains(graphSpec.EndY));
            });
        }

        [Then(@"I should see a dropdown with selectable projects:")]
        public void VerifyProjectDropdown(Table table)
        {
            var projectDropdown = FeatureWrapper.JQuery.Find("#ProjectList");
            foreach (var row in table.Rows)
            {
                projectDropdown.Text()
                    .Should().Contain(row[0]);
            }
        }

        [Then(@"the accumulated story points of all issues should be (.*)")]
        public void VerifyGraphValues(double expectedStoryPoints)
        {
            var expectedText = string.Format("data: [0, {0:0.0}]", expectedStoryPoints);

            WaitForRendering(() =>
            {
                var chartDiv = FeatureWrapper.JQuery.Find("#chartDiv");
                return chartDiv.Text().Contains(expectedText);
            });
        }

        private static void WaitForRendering(Func<bool> shouldBeTrue)
        {
            bool found = false;

            var timeout = TimeSpan.FromSeconds(60);
            var startTime = DateTime.Now;

            while (DateTime.Now - startTime < timeout)
            {
                found = shouldBeTrue();

                if (found)
                {
                    break;
                }
                Thread.Sleep(500);
            }

            Assert.That(found, Is.True, "Timeout before assertion was fulfilled");
        }
    }
}

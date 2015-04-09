using System.Linq;

using FluentAssertions;
using JQSelenium;
using MongoDB.Driver;
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

        [Given(@"there exists a Jira project called '(.*)' with (.*) issues")]
        public void SetupMongo(string projectName, int noofIssues)
        {
            var issues = new MongoHelper<Issue>();
            issues.Collection.Insert(new Issue());
            var issuesList = issues.Collection.FindAll().ToList();
        }

        [Then(@"I should see a burn-up graph")]
        public void VerifyDefaultGraph()
        {
            // TODO: wait for page instead.
            //             var wait = new WebDriverWait(FeatureWrapper.PhantomJsDriver, TimeSpan.FromSeconds(60));
            //             var chartDiv = wait.Until(x => x.FindElement(By.Id("chart_container")));
            System.Threading.Thread.Sleep(500);

            var chartDiv = FeatureWrapper.PhantomJsDriver.FindElementById("chart_container");
            chartDiv.Text.Should().Contain("Week");
            chartDiv.Text.Should().Contain("Burnup");
            chartDiv.Text.Should().Contain("start");
        }

        [When(@"I search for issues with jql query '(.*)'")]
        public void WhenISearchForIssuesWithJqlQuery(string jql)
        {
            var jqlTextField = FeatureWrapper.PhantomJsDriver.FindElementById("jql");
            jqlTextField.SendKeys(jql);
            var jquery = new JQuery(FeatureWrapper.PhantomJsDriver);
            
            jquery.Find("#Search").Click();
        }

        [Then(@"I should see a burn-up graph with values:")]
        public void VerifyGraph(Table table)
        {
            var graphData = table.CreateSet<BurnUpGraphSpec>().ToList();
            VerifyDefaultGraph();

            var chartDiv = FeatureWrapper.PhantomJsDriver.FindElementById("chart_container");
            graphData.ForEach(data =>
            {
                chartDiv.Text.Should().Contain(data.StartX);
                chartDiv.Text.Should().Contain(data.EndX);
                chartDiv.Text.Should().Contain(data.StartY);
                chartDiv.Text.Should().Contain(data.EndY);
            });
        }

        [Then(@"I should see an empty search field")]
        public void VerifyEmptySearchField()
        {
            var textField = FeatureWrapper.PhantomJsDriver.FindElementById("jql");
            textField.Text.Should().BeEmpty();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Olifant.JiraMetrics.Lib.Jira.Model;
using Olifant.JiraMetrics.Test.Acceptance.Pages;
using Olifant.JiraMetrics.Test.Utilities.Helpers;
using TechTalk.SpecFlow;

namespace Olifant.JiraMetrics.Test.Acceptance.Steps
{
    [Binding]
    public class PlotGraphOf1000JiraIssuesOnWebPage
    {

        [Given(@"there exists a Jira project called '(.*)' with (.*) issues where each has story point of (.*)")]
        public void InsertManyIssuesIntoMongo(string projectName, int noofIssues, int storyPoint)
        {
            // TODO: move to MongoWrapper
            var issuesCollection = MongoWrapper.Instance.GetCollection<Issue>();
            var issues = JiraStubFactory.CreateMany("DISCO-620", noofIssues, storyPoint);
            issues.ToList().ForEach(i => i.Fields.Project.Name = projectName);
            issuesCollection.InsertBatch(issues);

            if (ScenarioWrapper.PageNavigator.Current != null)
            {
                ScenarioWrapper.PageNavigator.Current.Refresh();
            }
        }
    }
}

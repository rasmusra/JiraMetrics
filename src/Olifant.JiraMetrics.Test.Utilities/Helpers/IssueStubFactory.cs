using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using MoreLinq;
using Newtonsoft.Json;
using Olifant.JiraMetrics.Lib.Jira.Model;
using Olifant.JiraMetrics.Test.Utilities.Fakes;

namespace Olifant.JiraMetrics.Test.Utilities.Helpers
{
    public static class IssueStubFactory
    {
        public static Issue Create(string issueKey, int givenStoryPoints)
        {
            var json = new FakeJiraRestClient("Stubs").ReadJsonFile(string.Format("key={0}", issueKey));
            var issue = JsonConvert.DeserializeObject<Issues>(json).IssueList.First();
            issue.Fields.StoryPoints = givenStoryPoints;
            return issue;
        }

        public static IList<Issue> CreateMany(string issueKey, int noofIssues, int storyPoint)
        {
            var issue = Create(issueKey, storyPoint);
            var issues = issue.CloneMany(noofIssues);
            return issues;                      
        }

        public static IList<Issue> CreateFromFiles()
        {
            var jsonList = new FakeJiraRestClient("Stubs").ReadAllJsonFiles();
            var issueList = jsonList
                .Select(JsonConvert.DeserializeObject<Issues>)
                .SelectMany(issues => issues.IssueList)
                .DistinctBy(issue => issue.Key)
                .ToList();

            return issueList;
        }

    }
}

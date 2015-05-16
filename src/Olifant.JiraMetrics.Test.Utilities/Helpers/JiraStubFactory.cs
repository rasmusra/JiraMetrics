using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using Newtonsoft.Json;
using Olifant.JiraMetrics.Lib.Jira.Model;
using Olifant.JiraMetrics.Test.Utilities.Fakes;

namespace Olifant.JiraMetrics.Test.Utilities.Helpers
{
    public static class JiraStubFactory
    {
        private static FakeJiraRestClient _fakeJiraClient;

        public static void Setup(string stubsDir)
        {
            _fakeJiraClient = new FakeJiraRestClient(stubsDir);
        }

        public static FakeJiraRestClient FakeJiraRestClient
        {
            get { return _fakeJiraClient ?? (_fakeJiraClient = new FakeJiraRestClient("Stubs")); }
        }

        public static Issue Create(string issueKey, int givenStoryPoints)
        {
            var json = FakeJiraRestClient.ReadJsonFile(string.Format("key={0}", issueKey));
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

        public static IList<Issue> CreateFromIssueFiles()
        {
            return CreateFromFiles("key=*.json");
        }

        public static IList<Issue> CreateFromFiles(string pattern)
        {
            var jsonList = FakeJiraRestClient.ReadMatchingJsonFiles(pattern);
            var issueList = jsonList
                .Select(JsonConvert.DeserializeObject<Issues>)
                .SelectMany(issues => issues.IssueList)
                .DistinctBy(issue => issue.Key)
                .ToList();

            return issueList;
        }

    }
}

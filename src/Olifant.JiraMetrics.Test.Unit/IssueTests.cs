using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Olifant.JiraMetrics.Test.Unit
{
    public class IssueTests
    {
        [Test]
        public void ClonesItselfManyTimesWithUniqueKeys()
        {
            var issue = IssueStubFactory.CreateIssue("originalKey");
            var originalJson = JsonConvert.SerializeObject(issue);
            var clonedIssues = issue.CloneMany(2);

            foreach (var clonedIssue in clonedIssues)
            {
                var clonedJson = JsonConvert.SerializeObject(clonedIssue);
                
                originalJson.Replace("originalKey", "")
                    .ShouldBeEquivalentTo(clonedJson.Replace(clonedIssue.Key, ""));
            }
        }
    }
}

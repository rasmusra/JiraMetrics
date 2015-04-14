using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;
using Newtonsoft.Json;
using Olifant.JiraMetrics.Lib;

using NUnit.Framework;
using Olifant.JiraMetrics.Lib.Jira.Model;

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

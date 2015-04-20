using System.Diagnostics;
using System.Linq;

using FluentAssertions;

using Olifant.JiraMetrics.Lib.Jira.Model;
using Olifant.JiraMetrics.Lib.Metrics;
using Olifant.JiraMetrics.Lib.Metrics.Model;
using Olifant.JiraMetrics.Test.Unit.Fakes;

using Newtonsoft.Json;

using NUnit.Framework;
using Olifant.JiraMetrics.Test.Utilities.Fakes;
using Olifant.JiraMetrics.Test.Utilities.Helpers;

namespace Olifant.JiraMetrics.Test.Unit
{
    [TestFixture]
    internal class IssueReportModelTests
    {
        [TestCase("DISCO-620", "2014-10-20 13:02:08", "Dev")]
        [TestCase("DISCO-620", "2014-11-03 13:53:59", "Test")]
        [TestCase("DISCO-665", "2014-07-15 08:33:00", "Full process")]
        public void ProvidesDoneDateTimeOfCycle(string jiraKey, string expected, string cycle)
        {
            var issue = ReadFirstIssue(jiraKey, CycleName2StatusesDict.Lookup(cycle));

            issue.DoneDateTime.ShouldBeEquivalentTo(expected);
        }
        
        [TestCase("DISCO-620", "2014-10-02 12:45:35", "Dev", null)]
        [TestCase("DISCO-620", "2014-10-20 13:02:08", "Test", null)]
        [TestCase("DISCO-665", "2014-07-04 10:51:01", "Full process", "started")]
        [TestCase("DISCO-665", "2014-07-02 14:50:44", "Backlog", null)]
        public void ProvidesStartDateTimeOfCycle(string jiraKey, string expected, string cycle, string startLabel)
        {
            var issue = ReadFirstIssue(jiraKey, CycleName2StatusesDict.Lookup(cycle), startLabel);

            issue.StartDateTime.ShouldBeEquivalentTo(expected);
        }

        [TestCase("CR is assigned 7 story points", "DISCO-620", 7)]
        [TestCase("Defect is assigned 1 points", "DISCO-665", 1)]
        [TestCase("CR is not assigned any points", "OFU-1462", 1)]
        [TestCase("Defect is not assigned any points", "OFU-676", 1)]
        public void ProvidesStoryPointsOfIssue(string scenarioDescription, string jiraKey, decimal expectedStoryPoints)
        {
            // arrange
            var issue = ReadFirstIssue(jiraKey, CycleName2StatusesDict.Lookup("Dev"));

            // act
            var actual = issue.StoryPoints;

            // assert
            actual.ShouldBeEquivalentTo(expectedStoryPoints);
        }

        [TestCase("Calculate with weekends occuring on end date", "2015-01-29 15:30", "2015-02-01 20:33", 3.21)]
        [TestCase("Calculate with same weekends occuring on start and end date", "2015-01-31 15:30", "2015-02-01 20:33", 1.21)]
        [TestCase("No weekend days", "2015-01-26 15:30", "2015-01-30 20:33", 4.21)]
        [TestCase("One work week", "2015-01-26 15:30", "2015-02-02 15:30", 5)]
        [TestCase("Includes three weekends", "2015-01-29 15:30", "2015-02-19 20:33", 15.21)]
        [TestCase("Less than a day", "2014-12-08 16:17", "2014-12-09 08:03", 0.66)]
        public void CyckeTimeIgnoresWeekends(string description, string startDate, string endDate, decimal expectedDateDiff)
        {
            var cycleTime = new FakeCycleTimeRule(startDate, endDate);
            var target = new IssueReportModel(null, cycleTime, false);

            target.CycleTime
                .ShouldBeEquivalentTo(expectedDateDiff);
        }

        [TestCase("A Change request", "DISCO-620", "Change Request - Technical")]
        [TestCase("A Change request", "DISCO-838", "Change Request")]
        [TestCase("A technical change request", "OFU-1462", "Change Request - Functional")]
        [TestCase("A fixed defect", "DISCO-665", "Defect")]
        [TestCase("A defect not fixed", "SCSC-974", "Defect - Not a defect")]
        public void ReportsTypeOfIssue(string scenarioDescription, string jiraKey, string expectedType)
        {
            // arrange
            var issue = ReadFirstIssue(jiraKey, CycleName2StatusesDict.Lookup("Dev"));

            // act
            var actual = issue.IssueType;

            // assert
            actual.ShouldBeEquivalentTo(expectedType);
        }

        private static IIssueReportModel ReadFirstIssue(string jiraKey, Status[] statuses, string startedLabel = "")
        {
            var query = string.Format("key={0}", jiraKey);
            var json = new FakeJiraRestClient().GetJsonChunks(query);
            var issues = JsonConvert.DeserializeObject<Issues>(json.First());

            var target = IssueReportModelFactory.Create(issues.IssueList, new CycleTimeRule(statuses, startedLabel))
                .First();

            return target;
        }
    }
}
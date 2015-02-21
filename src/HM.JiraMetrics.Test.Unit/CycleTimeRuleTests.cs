using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

using HM.JiraMetrics.Lib.Jira.Model;
using HM.JiraMetrics.Lib.Metrics;
using HM.JiraMetrics.Test.Fakes.Helpers;

using NUnit.Framework;

namespace HM.JiraMetrics.Test.Unit
{
    public class CycleTimeRuleTests
    {
        [TestCase("Requirement Analysis", "Test")]
        public void MaintainsStatuses(params string[] givenStatuses)
        {
            var target = new CycleTimeRule(givenStatuses);
            target.Statuses.Select(s => s.Name).ShouldAllBeEquivalentTo(givenStatuses);
        }

        [TestCase("Some Pr-ccycle status", "Implementation")]
        public void MaintainsPreCycleStatuses(params string[] givenPreCycleStatuses)
        {
            var target = new CycleTimeRule(new string[] { }, givenPreCycleStatuses);
            target.PreCycleStatuses.Select(s => s.Name).ShouldAllBeEquivalentTo(givenPreCycleStatuses);
        }

        [TestCase("Full process", "implementing", "2014-01-01 12:34:56")]
        [TestCase("Dev", "implementing", "2014-01-01 12:34:56")]
        public void DeterminesStartDateFromHistoryWithoutStartLabel(string cycleName, string givenStatusHistory, string givenHistoryCreatedDate)
        {
            // arrange
            var cycles = CycleName2StatusesDict.Lookup(cycleName);
            var givenHistory = IssueStubFactory.CreateStatusHistory(givenHistoryCreatedDate, givenStatusHistory);
            var givenIssue = IssueStubFactory.CreateIssue(givenHistory);

            // act
            var target = new CycleTimeRule(cycles);
            var actualStartedDate = target.GetStartDateTime(givenIssue);

            // assert
            actualStartedDate.ShouldBeEquivalentTo(givenHistoryCreatedDate);
        }

        [TestCase("Full process", "started", "2014-01-01 12:34:56")]
        public void DeterminesStartDateFromIssueWithStartLabel(string cycleName, string givenStartedLabel, string givenHistoryCreatedDate)
        {
            // arrange
            var cycles = CycleName2StatusesDict.Lookup(cycleName);
            var givenHistory = IssueStubFactory.CreateLabelHistory(givenHistoryCreatedDate, givenStartedLabel);
            var givenIssue = IssueStubFactory.CreateIssue(givenHistory);

            // act
            var target = new CycleTimeRule(cycles, givenStartedLabel);
            var actualStartedDate = target.GetStartDateTime(givenIssue);

            // assert
            actualStartedDate.ShouldBeEquivalentTo(givenHistoryCreatedDate);
        }
    }
}

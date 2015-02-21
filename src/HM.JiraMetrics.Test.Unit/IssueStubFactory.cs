using System.Collections.Generic;

using HM.JiraMetrics.Lib.Jira.Model;

namespace HM.JiraMetrics.Test.Unit
{
    internal static class IssueStubFactory
    {
        internal static Issue CreateIssue(History givenHistory, string createdDate = "1900-01-01")
        {
            var issue = new Issue()
            {
                ChangeLog = IssueStubFactory.CreateChangeLog(givenHistory),
                Fields = new Fields { Created = createdDate }
            };
            return issue;
        }

        internal static ChangeLog CreateChangeLog(History givenHistory)
        {
            var changeLog = new ChangeLog { Histories = IssueStubFactory.CreateHistories(givenHistory) };
            return changeLog;
        }

        internal static List<History> CreateHistories(History givenHistory)
        {
            return new List<History> { givenHistory };
        }

        internal static History CreateStatusHistory(string givenHistoryCreatedDate, string givenHistoryStatus)
        {
            var historyItem = new HistoryItem { Field = "status", FromValue = "open", ToValue = givenHistoryStatus };
            var historyItems = new List<HistoryItem> { historyItem };
            return new History { HistoryItems = historyItems, Created = givenHistoryCreatedDate };
        }

        internal static History CreateLabelHistory(string givenHistoryCreatedDate, string givenStartedLabel)
        {
            var historyItem = new HistoryItem { Field = "labels", FromValue = string.Empty, ToValue = givenStartedLabel };
            var historyItems = new List<HistoryItem> { historyItem };
            return new History { HistoryItems = historyItems, Created = givenHistoryCreatedDate };
        }
    }
}

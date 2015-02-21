using System;

using Olifant.JiraMetrics.Lib.Metrics.Model;

namespace Olifant.JiraMetrics.Lib.Metrics.Filters
{
    public class WorkDoneFilter : IIssueFilter
    {
        public string Description
        {
            get
            {
                return "WorkDoneFilter";
            }
        }

        public bool IsOk(IIssueReportModel issueReportModel)
        {
            DateTime parsedDateTime;

            return DateTime.TryParse(issueReportModel.StartDateTime, out parsedDateTime) &&
                DateTime.TryParse(issueReportModel.DoneDateTime, out parsedDateTime);
        }
    }
}
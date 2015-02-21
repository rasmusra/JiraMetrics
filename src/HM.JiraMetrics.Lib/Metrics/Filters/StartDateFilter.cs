using System;

using HM.JiraMetrics.Lib.Metrics.Model;

namespace HM.JiraMetrics.Lib.Metrics.Filters
{
    public class StartDateFilter : IIssueFilter
    {
        private readonly DateTime minStartDateTime;

        private readonly DateTime maxStartDateTime;

        public StartDateFilter(DateTime minStartDateTime, DateTime maxStartDateTime)
        {
            this.minStartDateTime = minStartDateTime;
            this.maxStartDateTime = maxStartDateTime;
        }

        public StartDateFilter()
        {
            minStartDateTime = new DateTime(2000, 1, 1);
            maxStartDateTime = new DateTime(2050, 1, 1);
        }

        public DateTime MinStartDateTime
        {
            get
            {
                return minStartDateTime;
            }
        }

        public DateTime MaxStartDateTime
        {
            get
            {
                return maxStartDateTime;
            }
        }

        public string Description
        {
            get
            {
                return string.Format("StartDateFilter({0:yyyy-MM-dd},{1:yyyy-MM-dd})", minStartDateTime, maxStartDateTime);
            }
        }

        public bool IsOk(IIssueReportModel issueReportModel)
        {
            DateTime parsedDateTime;

            // TODO: let filters act on jira model instead of report model
            return DateTime.TryParse(issueReportModel.StartDateTime, out parsedDateTime)
                   && parsedDateTime >= minStartDateTime 
                   && parsedDateTime <= maxStartDateTime;
        }
    }
}
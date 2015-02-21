using System;

using HM.JiraMetrics.Lib.Metrics.Model;

namespace HM.JiraMetrics.Lib.Metrics.Filters
{
    public class DoneDateFilter : IIssueFilter
    {
        private readonly DateTime minDateTime;

        private readonly DateTime maxDateTime;

        public DoneDateFilter(DateTime minDateTime, DateTime maxDateTime)
        {
            this.minDateTime = minDateTime;
            this.maxDateTime = maxDateTime;
        }

        public string Description
        {
            get
            {
                return string.Format("DoneDateFilter({0:yyyy-MM-dd},{1:yyyy-MM-dd})", minDateTime, maxDateTime);
            }
        }

        public bool IsOk(IIssueReportModel issueReportModel)
        {
            DateTime parsedDateTime;

            if (!DateTime.TryParse(issueReportModel.DoneDateTime, out parsedDateTime))
            {
                return false;
            }

            return parsedDateTime > minDateTime && parsedDateTime <= maxDateTime;
        }
    }
}
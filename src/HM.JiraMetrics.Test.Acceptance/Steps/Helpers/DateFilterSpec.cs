using System;

using HM.JiraMetrics.Lib.Metrics.Filters;

namespace HM.JiraMetrics.Test.Acceptance.Steps.Helpers
{
    internal class DateFilterSpec
    {
        public string MaxDate
        {
            get;
            set;
        }

        public string MinDate
        {
            get;
            set;
        }

        public IIssueFilter CreateDoneDateFilter()
        {
            return new DoneDateFilter(DateTime.Parse(MinDate), DateTime.Parse(MaxDate));
        }

        public IIssueFilter CreateStartDateFilter()
        {
            return new StartDateFilter(DateTime.Parse(MinDate), DateTime.Parse(MaxDate));
        }
    }
}
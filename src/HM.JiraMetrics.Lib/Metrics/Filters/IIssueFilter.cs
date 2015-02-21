using System;
using System.Dynamic;

using HM.JiraMetrics.Lib.Metrics.Model;

namespace HM.JiraMetrics.Lib.Metrics.Filters
{
    public interface IIssueFilter
    {
        string Description { get; }

        bool IsOk(IIssueReportModel issueReportModel);
    }
}
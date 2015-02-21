using System;
using System.Dynamic;

using Olifant.JiraMetrics.Lib.Metrics.Model;

namespace Olifant.JiraMetrics.Lib.Metrics.Filters
{
    public interface IIssueFilter
    {
        string Description { get; }

        bool IsOk(IIssueReportModel issueReportModel);
    }
}
using System.Collections.Generic;
using System.Linq;

using Olifant.JiraMetrics.Lib.Jira.Model;

namespace Olifant.JiraMetrics.Lib.Metrics.Model
{
    public class IssueReportModelFactory
    {
        public static IList<IIssueReportModel> Create(IList<Issue> issues, CycleTimeRule cycleTimeRule)
        {
            return issues
                .Select(issue => new IssueReportModel(issue, cycleTimeRule))
                .Cast<IIssueReportModel>()
                .ToList();
        }
    }
}
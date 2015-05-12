using System.Collections.Generic;
using System.Linq;
using Olifant.JiraMetrics.Lib.Metrics.Model;

namespace Olifant.JiraMetrics.Lib.Metrics.BurnUp
{
    public class BurnUpGraphThroughput
    {
        private readonly IEnumerable<IIssueReportModel> _issues;

        public BurnUpGraphThroughput(IEnumerable<IIssueReportModel> issues)
        {
            _issues = issues;
        }

        public decimal StoryPoints
        {
            get
            {
                return _issues
                    .Select(issue => issue.StoryPoints)
                    .Sum();
            }
        }
    }
}
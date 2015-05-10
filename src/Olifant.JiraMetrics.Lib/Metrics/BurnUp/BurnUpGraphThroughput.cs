using System;
using System.Collections.Generic;
using System.Linq;
using Olifant.JiraMetrics.Lib.Metrics.Model;

namespace Olifant.JiraMetrics.Lib.Metrics.BurnUp
{
    public class BurnUpGraphThroughput
    {
        private readonly IList<IIssueReportModel> _issues = new List<IIssueReportModel>();

        public BurnUpGraphThroughput(IList<IIssueReportModel> issues)
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
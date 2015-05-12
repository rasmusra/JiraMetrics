using System.Collections.Generic;
using System.Linq;

using Olifant.JiraMetrics.Lib.Jira;
using Olifant.JiraMetrics.Lib.Jira.Model;
using Olifant.JiraMetrics.Lib.Metrics;
using Olifant.JiraMetrics.Lib.Metrics.Filters;
using Olifant.JiraMetrics.Lib.Metrics.Model;

using Newtonsoft.Json;

namespace Olifant.JiraMetrics.Lib
{
    public class JiraMetricsFacade
    {        
        private readonly IJiraRestClient jiraRestClient;

        public JiraMetricsFacade(IJiraRestClient jiraRestClient)
        {
            this.jiraRestClient = jiraRestClient;
        }

        public IList<IIssueReportModel> GetIssues(string jql, CycleTimeRule cycleTimeRule)
        {
            return GetIssues(jql, cycleTimeRule, new List<IIssueFilter>());
        }

        public IList<IIssueReportModel> GetIssues(string jql, CycleTimeRule cycleTimeRule, List<IIssueFilter> filters)
        {
            var issues = new List<Issue>();
            var jsonChunks = this.jiraRestClient.GetJsonChunks(jql);

            jsonChunks.ForEach(
                chunk =>
                {
                    var chunkOfIssues = JsonConvert.DeserializeObject<Issues>(chunk);
                    issues.AddRange(chunkOfIssues.IssueList);
                });

            var reportItemModels = IssueReportModelFactory.Create(issues, cycleTimeRule);

            filters.ForEach(
                f => reportItemModels = reportItemModels.Where(
                    ri =>
                    {
                        var isOk = f.IsOk(ri);
                        return isOk;
                    }).ToList());

            return reportItemModels;
        }
    }
}
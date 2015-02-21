using System.Collections.Generic;
using System.Linq;

using HM.JiraMetrics.Lib.Jira;
using HM.JiraMetrics.Lib.Jira.Model;
using HM.JiraMetrics.Lib.Metrics;
using HM.JiraMetrics.Lib.Metrics.Filters;
using HM.JiraMetrics.Lib.Metrics.Model;
using HM.JiraMetrics.Lib.Metrics.TextReport;

using Newtonsoft.Json;

namespace HM.JiraMetrics.Lib
{
    public class JiraMetricsFacade
    {        
        private readonly IJiraRestClient jiraRestClient;

        public JiraMetricsFacade(IJiraRestClient jiraRestClient)
        {
            this.jiraRestClient = jiraRestClient;
        }

        public void GenerateCycleTimeReport(string jql, CycleTimeRule cycleTimeRule, List<IIssueFilter> filters, int chunkSizeInDays, ITextEditorProxy textEditorProxy)
        {
            var reportItemModels = GetIssues(jql, cycleTimeRule, filters);
            var reportManager = new TextReportManager(textEditorProxy);

            reportManager.GenerateCycleTimeReport(
                reportItemModels, 
                cycleTimeRule, 
                jql, 
                filters);
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

        public void GenerateValueAddedTimeReport(string jql, CycleTimeRule cycleTimeRule, List<IIssueFilter> filters, int chunkSizeInDays, ITextEditorProxy textEditorProxy)
        {
            var reportItemModels = GetIssues(jql, cycleTimeRule, filters);
            var reportManager = new TextReportManager(textEditorProxy);

            reportManager.GenerateValueAddedTimeReport(
                reportItemModels,
                cycleTimeRule,
                jql,
                filters);
        }
    }
}
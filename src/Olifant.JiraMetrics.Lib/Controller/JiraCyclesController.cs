using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using HM.OT.JiraMetrics.Lib.Jira;
using HM.OT.JiraMetrics.Lib.Jira.Model;
using HM.OT.JiraMetrics.Lib.Report;
using HM.OT.JiraMetrics.Lib.Report.CycleTimeRules;
using HM.OT.JiraMetrics.Lib.Report.Filters;
using HM.OT.JiraMetrics.Lib.Report.Model;

using Newtonsoft.Json;

namespace HM.OT.JiraMetrics.Lib.Controller
{
    public class JiraCyclesController
    {        
        private readonly IRestClient restClient;

        private readonly ReportManager reportManager;

        public JiraCyclesController(IRestClient restClient, ITextEditorProxy textEditorProxy)
        {
            reportManager = new ReportManager(textEditorProxy);
            this.restClient = restClient;
        }

        public void ProcessQuery(string jql, BaseCycleTimeRule cycleTimeRule, List<IIssueFilter> filters, int chunkSizeInDays)
        {
            var issues = GetJiraIssues(jql);

            var reportItemModels = IssueReportModelFactory.Create(issues, cycleTimeRule);

            filters.ForEach(f => reportItemModels = reportItemModels.Where(
                ri =>
                    {
                        var isOk = f.IsOk(ri);
                        return isOk;
                    })
                    .ToList());

            reportManager.GenerateReport(
                reportItemModels, 
                cycleTimeRule, 
                jql, 
                filters);
        }

        private IList<Issue> GetJiraIssues(string jql)
        {
            var result = new List<Issue>();
            var jsonChunks = restClient.GetJsonChunks(jql);
            
            jsonChunks.ForEach(
                chunk =>
                    {
                        var chunkOfIssues = JsonConvert.DeserializeObject<Issues>(chunk);
                        result.AddRange(chunkOfIssues.IssueList);
                    });

            return result;
        }
    }
}
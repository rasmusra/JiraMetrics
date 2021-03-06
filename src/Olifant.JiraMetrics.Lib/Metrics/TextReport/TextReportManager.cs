﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

using Olifant.JiraMetrics.Lib.Metrics.Filters;
using Olifant.JiraMetrics.Lib.Metrics.Model;

namespace Olifant.JiraMetrics.Lib.Metrics.TextReport
{
    public class TextReportManager
    {
        private readonly ITextEditorProxy _textEditorProxy;

        public TextReportManager(ITextEditorProxy textEditorProxy)
        {
            _textEditorProxy = textEditorProxy;
        }

        public void GenerateCycleTimeReport(IEnumerable<IIssueReportModel> issues, CycleTimeRule cycleTimeRule, string jql, IEnumerable<IIssueFilter> filters)
        {
            var output = new StringBuilder();

            var header = CreateHeader(cycleTimeRule, jql, filters);
            output.AppendLine(header);

            issues.ToList().ForEach(issueReport => output.AppendLine(issueReport.ToString()));
            _textEditorProxy.ShowInEditor(output.ToString());
        }

        private static string CreateHeader(CycleTimeRule cycleTimeRule, string jql, IEnumerable<IIssueFilter> filters)
        {
            var filterDescriptions = string.Join(", ", filters.Select(f => f.Description));
            var trimmedJql = jql.Replace('\n', ' ').Replace('\r', ' ');
            var header = string.Format("Cycle Time Report, cycle: {0}, jql : {1}, filters : [{2}]", cycleTimeRule, trimmedJql, filterDescriptions);
            return header;
        }
    }
}

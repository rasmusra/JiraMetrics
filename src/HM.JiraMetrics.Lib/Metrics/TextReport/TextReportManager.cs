using System.Collections.Generic;
using System.Linq;
using System.Text;

using HM.JiraMetrics.Lib.Metrics.Filters;
using HM.JiraMetrics.Lib.Metrics.Model;

namespace HM.JiraMetrics.Lib.Metrics.TextReport
{
    public class TextReportManager
    {
        private readonly ITextEditorProxy textEditorProxy;

        public TextReportManager(ITextEditorProxy textEditorProxy)
        {
            this.textEditorProxy = textEditorProxy;
        }

        public void GenerateCycleTimeReport(IEnumerable<IIssueReportModel> issues, CycleTimeRule cycleTimeRule, string jql, IEnumerable<IIssueFilter> filters)
        {
            var output = new StringBuilder();

            var header = CreateHeader(cycleTimeRule, jql, filters);
            output.AppendLine(header);

            issues.ToList().ForEach(issueReport => output.AppendLine(issueReport.ToString()));
            this.textEditorProxy.ShowInEditor(output.ToString());
        }

        public void GenerateValueAddedTimeReport(IEnumerable<IIssueReportModel> issues, CycleTimeRule cycleTimeRule, string jql, IEnumerable<IIssueFilter> filters)
        {
            // TODO: DRY
            var output = new StringBuilder();

            var header = CreateValueAddedTimeReportHeader(cycleTimeRule, jql, filters);
            output.AppendLine(header);

            issues.ToList().ForEach(issueReport => output.AppendLine(issueReport.ToString()));
            this.textEditorProxy.ShowInEditor(output.ToString());
        }

        private static string CreateHeader(CycleTimeRule cycleTimeRule, string jql, IEnumerable<IIssueFilter> filters)
        {
            var filterDescriptions = string.Join(", ", filters.Select(f => f.Description));
            var trimmedJql = jql.Replace('\n', ' ').Replace('\r', ' ');
            var header = string.Format("Cycle Time Report, cycle: {0}, jql : {1}, filters : [{2}]", cycleTimeRule, trimmedJql, filterDescriptions);
            return header;
        }

        // TODO: DRY
        private static string CreateValueAddedTimeReportHeader(CycleTimeRule cycleTimeRule, string jql, IEnumerable<IIssueFilter> filters)
        {
            var filterDescriptions = string.Join(", ", filters.Select(f => f.Description));
            var trimmedJql = jql.Replace('\n', ' ').Replace('\r', ' ');
            var header = string.Format("Value Added Time Report, cycle : {0}, jql : {1}, filters : [{2}]", cycleTimeRule, trimmedJql, filterDescriptions);
            return header;
        }
    }
}

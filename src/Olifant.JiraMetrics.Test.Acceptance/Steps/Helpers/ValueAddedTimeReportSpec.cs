using Olifant.JiraMetrics.Lib.Metrics.Model;

namespace Olifant.JiraMetrics.Test.Acceptance.Steps.Helpers
{
    public class ValueAddedTimeReportSpec
    {
        public string Key
        {
            get;
            set;
        }

        public string Summary
        {
            get;
            set;
        }

        public string Cycle
        {
            get;
            set;
        }
        
        public string Value
        {
            get;
            set;
        }

        public string CycleDescription
        {
            get;
            set;
        }

        public string StartDate
        {
            get;
            set;
        }

        public override string ToString()
        {
            var result = string.Format("{0}{4}{1}{4}{2}{4}{3}{4}{5}{4}{6}", this.Key, this.Summary, this.Cycle, this.Value, IssueReportModel.Separator, this.CycleDescription, this.StartDate);

            return result;
        }
    }
}
using Olifant.JiraMetrics.Lib.Metrics.Model;

namespace Olifant.JiraMetrics.Test.Acceptance.Steps.Specs
{
    public class CycleTimeReportEntrySpec
    {
        public string ClosedDate
        {
            get;
            set;
        }

        public string CycleTime
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }

        public string IssueType
        {
            get;
            set;
        }

        public string ScenarioDescription
        {
            get;
            set;
        }

        public string StartedDate
        {
            get;
            set;
        }

        public string Summary
        {
            get;
            set;
        }

        public string StoryPoints
        {
            get;
            set;
        }

        public string OriginalEstimate
        {
            get;
            set;
        }

        public override string ToString()
        {
            var result = string.Format("{0}{4}{8}{4}{1}{4}{2}{4}{3}{4}{5}{4}{6}{4}{7}", Key, IssueType, Summary, StartedDate, ClosedDate, IssueReportModel.Separator, CycleTime, StoryPoints, OriginalEstimate);

            return result;
        }
    }
}
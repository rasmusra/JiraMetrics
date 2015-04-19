namespace Olifant.JiraMetrics.Lib.Metrics.Model
{
    public interface IIssueReportModel
    {
        string StartDateTime { get; }

        string DoneDateTime { get; }

        decimal StoryPoints { get; }
        string IssueType { get; }
    }
}
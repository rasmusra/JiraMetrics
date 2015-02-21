namespace Olifant.JiraMetrics.Lib.Metrics.Model
{
    public interface IIssueReportModel
    {
        string StartDateTime { get; }

        string DoneDateTime { get; }

        string StoryPoints { get; }
    }
}
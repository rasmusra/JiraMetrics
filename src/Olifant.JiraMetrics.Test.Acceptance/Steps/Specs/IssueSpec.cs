using Olifant.JiraMetrics.Lib.Metrics.Model;

namespace Olifant.JiraMetrics.Test.Acceptance.Steps.Specs
{
    public class IssueSpec
    {
        public string Project
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }

        public int StoryPoints
        {
            get;
            set;
        }
    }
}
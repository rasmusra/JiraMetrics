namespace Olifant.JiraMetrics.Lib.Jira
{
    public class JiraProjectQuery
    {
        public JiraProjectQuery(string projectName)
        {
            ProjectName = projectName;
        }

        public string ProjectName { get; private set; }

        public string JqlQuery { get { return string.Format("project='{0}'", ProjectName); } }
    }
}